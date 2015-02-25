using System;
using System.IO;
using System.Threading.Tasks;

namespace SimpleInstaller.Elements
{
    public abstract class InstallerElement
    {
        public abstract string Name { get; protected set; }

        public Action<string> Logger = delegate { };

        public abstract Task InstallAsync();
    }

    public class FolderInstallerElement : InstallerElement
    {
        public FolderInstallerElement(string name)
        {
            Name = name;
        }

        public string SourceFolder { get; set; }
        public string DestinationFolder { get; set; }


        public override string Name { get; protected set; }

        public override async Task InstallAsync()
        {
            await Task.Run(() => DirectoryCopy(SourceFolder, DestinationFolder, true));
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
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

                file.CopyTo(temppath,true);
                Logger(string.Format("Copied file to {0}", temppath));
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
