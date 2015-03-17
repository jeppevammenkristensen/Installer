using System.IO;
using System.Threading.Tasks;
using SimpleInstaller.Utilities;

namespace SimpleInstaller.Elements
{
    public class FolderInstallerElement : InstallerElement
    {
        private string _name;

        public FolderInstallerElement(string name, string sourceFolder, string destinationFolder, bool copySubDirs = true, string fileSearchPattern = null)
        {
            Name = name;
            SourceFolder = sourceFolder;
            DestinationFolder = destinationFolder;
            CopySubDirs = copySubDirs;
            FileSearchPattern = fileSearchPattern;
        }

        public string SourceFolder { get; set; }
        public string DestinationFolder { get; set; }
        public bool CopySubDirs { get; set; }
        public string FileSearchPattern { get; set; }


        public override string Name
        {
            get { return _name ?? (_name = string.Format("Copy from {0} to {1}", SourceFolder, DestinationFolder)); }
            set { _name = value; }
        }

        public override async Task InstallAsync()
        {
            await Task.Run(() => DirectoryHelper.DirectoryCopy(SourceFolder, DestinationFolder, CopySubDirs, FileSearchPattern,Logger));
        }

        public async override Task UninstallAsync()
        {
            await Task.Run(() => DirectoryHelper.Delete(DestinationFolder, CopySubDirs, FileSearchPattern, Logger));
        }
    }
}