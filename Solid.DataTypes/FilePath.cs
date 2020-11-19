using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using Solid.DataTypes.Exceptions;

namespace Solid.DataTypes
{
    public readonly struct FilePath : IEquatable<FilePath>
    {
        public static readonly FilePath Undefined = new FilePath();

        private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars().OrderBy(c => c).ToArray();
        private static readonly char[] InvalidFileChars = Path.GetInvalidFileNameChars().OrderBy(c => c).ToArray();
        private readonly string _filePath;

        #region Constructor

        public FilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));
            }
            _filePath = filePath.Trim();

            this.IsValid = IsValidPath(filePath);
        }

        #endregion

        [Pure]
        public bool Exists
        {
            get
            {
                if (this == FilePath.Undefined)
                {
                    return false;
                }

                if (this.IsValid == false)
                {
                    return false;
                }

                return File.Exists(this._filePath);
            }
        }

        [Pure]
        public FileExtension Extension
        {
            get
            {
                try
                {
                    var extension = Path.GetExtension(this._filePath);
                    if (string.IsNullOrWhiteSpace(extension))
                    {
                        return FileExtension.None;
                    }
                    return new FileExtension(extension);
                }
                catch (Exception e)
                {
                    throw new InvalidFilePathException($"the path \"{this._filePath}\" is not valid", e);
                }
            }
        }

        private void CheckIsValid()
        {
            if (!this.IsValid)
            {
                throw new InvalidFilePathException($"the path \"{this._filePath}\" is not valid");
            }
        }

        /// <summary>
        /// Indicates that the file path contains a valid value
        /// </summary>
        /// <remarks>
        /// determining validity is actually quite difficult as it can depend on the operating system, file system, and the API being used.
        /// 
        /// </remarks>
        [Pure]
        public bool IsValid { get; }

        [Pure]
        public FilePath AppendToFileName(string value)
        {
            var dir = this.GetDirectoryName();
            var filename = this.GetFileNameWithoutExtension();
            var extension = this.Extension;
            var newFilename = $"{filename}{value}{extension}";

            return new FilePath(Path.Combine(dir, newFilename));
        }

        [Pure]
        public FilePath ChangeExtension(FileExtension extension)
        {
            var newPath = Path.ChangeExtension(this._filePath, (string)extension);
            return new FilePath(newPath);
        }

        [Pure]
        public static explicit operator string(FilePath value)
        {
            return value._filePath;
        }

        [Pure]
        public static explicit operator FilePath(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            return new FilePath(value);
        }

        [Pure]
        public override string ToString()
        {
            return this._filePath;
        }

        public void Delete()
        {
            CheckIsValid();
            File.Delete(this._filePath);
        }

        /// <summary>
        /// Returns the directory information for the specified path string.
        /// </summary>
        /// <returns>The directory part of the filepath.  E.g. "C:\Users\Tom\Documents" for "C:\Users\Tom\Documents\projectReport.docx"</returns>
        [Pure]
        public string GetDirectoryName()
        {
            CheckIsValid();
            return Path.GetDirectoryName(this._filePath);
        }

        /// <summary>
        /// Gets the file name without the path
        /// <example>
        /// <code>
        ///     var f = new FilePath("C:\\Temp\\MyFile.txt");<br/>
        ///     var s = f.GetFileName();
        /// </code>
        /// In this example <c>s</c> is "MyFile.txt"
        /// </example>
        /// </summary>
        /// <returns></returns>
        [Pure]
        public FilePath GetFileName()
        {
            CheckIsValid();
            return (FilePath)Path.GetFileName(this._filePath);
        }

        [Pure]
        public FileStream OpenRead()
        {
            return File.OpenRead(this._filePath);
        }

        [Pure]
        public FileStream OpenWrite()
        {
            return File.OpenWrite(this._filePath);
        }

        /// <summary>
        /// Returns the name of the file without the extension
        /// </summary>
        /// <remarks>
        /// e.g.
        ///   MyFile.txt           =>   MyFile
        ///   C:\Temp\MyFile.txt   =>   MyFile
        /// </remarks>
        /// <returns></returns>
        [Pure]
        public string GetFileNameWithoutExtension()
        {
            return Path.GetFileNameWithoutExtension(this._filePath);
        }

        public void CopyTo(FilePath targetFilePath, bool overwrite = false)
        {
            if (!targetFilePath.IsValid) { throw new ArgumentNullException(nameof(targetFilePath), "The target path contains invalid chars"); }
            CheckIsValid();
            File.Copy(this.ToString(), targetFilePath.ToString(), overwrite);
        }

        public void MoveTo(in FilePath target)
        {
            if (!target.IsValid) { throw new ArgumentNullException(nameof(target), "The target path contains invalid chars"); }
            CheckIsValid();
            File.Move(this.ToString(), target.ToString());
        }

        [Pure]
        public FileInfo GetInfo()
        {
            return new FileInfo(this._filePath);
        }

        [Pure]
        public bool Equals(FilePath other)
        {
            return string.Equals(_filePath, other._filePath, StringComparison.InvariantCultureIgnoreCase);
        }

        [Pure]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FilePath other && Equals(other);
        }

        public override int GetHashCode()
        {
            return StringComparer.InvariantCultureIgnoreCase.GetHashCode(_filePath);
        }

        public static bool operator ==(FilePath left, FilePath right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FilePath left, FilePath right)
        {
            return !left.Equals(right);
        }

        public static FilePath Sanitise(string filePath, char substitutionChar = '_')
        {
            var safePath = new StringBuilder();

            var lastDirSeparator = filePath.LastIndexOfAny(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            for (int i = 0; i < lastDirSeparator; i++)
            {
                var pathChar = filePath[i];
                if (Array.BinarySearch(InvalidPathChars, pathChar) >= 0)
                {
                    pathChar = substitutionChar;
                }
                safePath.Append(pathChar);
            }

            if (lastDirSeparator >= 0)
            {
                safePath.Append(Path.DirectorySeparatorChar);
            }
            for (int i = lastDirSeparator + 1; i < filePath.Length; i++)
            {
                var pathChar = filePath[i];
                if (Array.BinarySearch(InvalidFileChars, pathChar) >= 0)
                {
                    pathChar = substitutionChar;
                }
                safePath.Append(pathChar);
            }

            return new FilePath(safePath.ToString());
        }

        /// <summary>
        /// Indicates that the given file path is a valid value
        /// </summary>
        /// <remarks>
        /// determining validity is actually quite difficult as it can depend on the operating system, file system, and the API being used.
        /// this gives a reasonable indication.
        /// The best option is to use the path and handle any exceptions
        /// </remarks>
        public static bool IsValidPath(string candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate)) return false;

            var path = Path.GetDirectoryName(candidate);
            if (path == null) return true; // e.g. "C:\"

            var fileName = Path.GetFileName(candidate);

            var isValid = (path.IndexOfAny(InvalidPathChars) == -1) && fileName.IndexOfAny(InvalidFileChars) == -1;

            if (!isValid) return false;

            try
            {
                // it's not a great idea to use exceptions here to determine validity, but it is, at least, somewhat reliable.
                // I've deliberately avoided Regex as there's a truism: If you solve one problem with Regex, you now have two problems.
                Path.Combine(candidate);
                var unused = new FileInfo(candidate);
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }
    }
}