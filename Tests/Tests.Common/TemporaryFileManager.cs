using System;
using System.Collections.Generic;
using System.IO;

namespace Tests.Common
{
    /// <summary>
    /// Manages the use of temporary files for the duration of a test
    /// </summary>
    public class TemporaryFileManager : IDisposable
    {
        private readonly List<string> _temporaryFiles;
        private readonly List<string> _temporaryFolders;

        /// <summary>
        /// Creates a new instance of <see cref="TemporaryFileManager"/>
        /// </summary>
        public TemporaryFileManager()
        {
            this._temporaryFiles = new List<string>();
            this._temporaryFolders = new List<string>();
        }

        /// <summary>
        /// Cleans up temporary files
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            this.Cleanup();
        }

        public void Cleanup()
        {
            this.CleanupTemporaryFiles();
            this.CleanupTemporaryFolders();
        }

        /// <summary>
        /// Deletes all the temporary files
        /// </summary>
        private void CleanupTemporaryFiles()
        {
            foreach (string fileName in this._temporaryFiles)
            {
                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch { }
                }
            }
        }

        private void CleanupTemporaryFolders()
        {
            foreach (var folder in this._temporaryFolders)
            {
                if (Directory.Exists(folder))
                {
                    try
                    {
                        Directory.Delete(folder, true);
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Returns the name of a temporary file name.  The file will be cleaned up at the end of the test
        /// </summary>
        /// <returns>The path to a temporary file.</returns>
        public string GetTemporaryFile()
        {
            string fileName = Path.GetTempFileName();
            RegisterTemporaryFile(fileName);
            return fileName;
        }

        public string GetTemporaryFolder()
        {
            string fileName = Path.GetTempFileName();
            File.Delete(fileName);
            RegisterTemporaryFolder(fileName);
            Directory.CreateDirectory(fileName);
            return fileName;
        }

        /// <summary>
        /// Registers a file as a temporary file with the test framework.
        /// Temporary files are removed as part of the test cleanup.
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        public void RegisterTemporaryFile(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                this._temporaryFiles.Add(fileName);
            }
        }

        /// <summary>
        /// Registers a folder as a temporary folder with the test framework.
        /// Temporary folders are removed as part of the dispose.
        /// </summary>
        /// <param name="folderName">The name of the folder</param>
        public void RegisterTemporaryFolder(string folderName)
        {
            if (!string.IsNullOrWhiteSpace(folderName))
            {
                this._temporaryFolders.Add(folderName);
            }
        }
    }
}