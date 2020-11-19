using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solid.DataTypes
{
    public class MimeTypeMap
    {
        private static readonly Dictionary<MimeType, FileExtension[]> TypeToExtMap = null;
        private static readonly Dictionary<FileExtension, MimeType> ExtToTypeMap = null;

        static MimeTypeMap()
        {
            TypeToExtMap = new Dictionary<MimeType, FileExtension[]>();
            ExtToTypeMap = new Dictionary<FileExtension, MimeType>();

            var mapConfig = LoadMimeTypeMap();
            foreach (var rawMap in mapConfig)
            {
                var parts = Regex.Split(rawMap, @"\s+");
                if (parts.Length < 2) continue;
                var mimeType = new MimeType(parts[0]);
                var extensions = parts.Skip(1).Select(p => new FileExtension(p)).ToArray();

                if (!TypeToExtMap.ContainsKey(mimeType))
                {
                    TypeToExtMap.Add(mimeType, extensions);
                }

                foreach (var fileExtension in extensions)
                {
                    if (!ExtToTypeMap.ContainsKey(fileExtension))
                    {
                        ExtToTypeMap.Add(fileExtension, mimeType);
                    }
                }
            }
        }


        public MimeTypeMap() { }

        public FileExtension GetBestExtensionsFor(MimeType mimeType)
        {
            var fileExtensions = GetExtensionsFor(mimeType);
            if (fileExtensions.Length == 0)
            {
                return FileExtension.None;
            }
            return fileExtensions[0];
        }

        public FileExtension[] GetExtensionsFor(MimeType mimeType)
        {
            if (mimeType == MimeType.None || !TypeToExtMap.TryGetValue(mimeType, out var extensions))
            {
                return new FileExtension[0];
            }

            return extensions;
        }

        public MimeType GetMimeTypeFor(FileExtension fileExtension)
        {
            if (fileExtension == FileExtension.None || !ExtToTypeMap.TryGetValue(fileExtension, out var type))
            {
                return MimeType.Default;
            }

            return type;
        }

        private static string[] LoadMimeTypeMap()
        {
            var lines = new List<string>();
            // the mime type map is stored in an embedded resource
            using (var s = typeof(MimeTypeMap).Assembly.GetManifestResourceStream(typeof(MimeTypeMap), "Resources.mime.types"))
            {
                using (var reader = new StreamReader(s))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.StartsWith("#"))
                        {
                            // commented lines begin with '#'
                            continue;
                        }
                        lines.Add(line);
                    }
                }
            }

            return lines.ToArray();
        }
    }
}
