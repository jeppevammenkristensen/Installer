using System;
using System.IO;
using System.Linq;

namespace SimpleInstaller.Utilities
{
    public class DirectoryHelper
    {
        public static string EnsureDirectoryExists(string directory)
        {
            if (directory == "." || directory == string.Empty)
                return directory;

            string currentPath = null;
            var paths = directory.Split(new []{Path.DirectorySeparatorChar}, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var path in paths)
            {
                if (currentPath == null)
                {
                    if (!Directory.Exists(path))
                        throw new InvalidOperationException(string.Format("Could not resolve path {0}", path));
                    
                    currentPath =
                        DriveInfo.GetDrives().Where(x => x.Name.StartsWith(path)).Select(x => x.Name).FirstOrDefault();
                    
                    // If we don't match a drive use the value instead
                    if (currentPath == null)
                        currentPath = path;
                    if (directory.StartsWith("\\\\"))
                        currentPath = "\\\\" + currentPath;
                }
                else
                {
                    currentPath = currentPath.CombinePathWith(path);

                    if (!Directory.Exists(currentPath))
                        Directory.CreateDirectory(currentPath);
                }
            }

            return directory;
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, string searchPattern = null, Action<string> logger = null)
        {
            logger = logger ?? delegate { };

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName == string.Empty ? Folder.CurrentExecutionFolder : sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);

                file.CopyTo(temppath, true);
                logger(temppath);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, true);
                }
            }
        }
    }
}