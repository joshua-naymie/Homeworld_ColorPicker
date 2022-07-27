using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker
{
    /// <summary>
    /// Class of miscellaneous utility methods that needed throughout the application
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Checks whether a path exists within the file system.
        /// Can be a directory or a file.
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>True if the path exists, false if not</returns>
        public static bool CheckPathExists(string path)
        {
            return Directory.Exists(path) || File.Exists(path);
        }

        /// <summary>
        /// Deletes all files and subdirectories in a directory.
        /// </summary>
        /// <param name="directory">The path to directory to clear</param>
        public static void ClearDirectory(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            directory.EnumerateFiles().ToList().ForEach(file => file.Delete());
            directory.EnumerateDirectories().ToList().ForEach(d => d.Delete(true));
        }
    }
}
