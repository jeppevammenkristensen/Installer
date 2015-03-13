using System.IO;
using System.Threading.Tasks;
using SimpleInstaller.Utilities;

namespace SimpleInstaller.Elements
{
    public class FolderInstallerElement : InstallerElement
    {
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


        public override string Name { get; set; }

        public override async Task InstallAsync()
        {
            await Task.Run(() => DirectoryHelper.DirectoryCopy(SourceFolder, DestinationFolder, CopySubDirs, FileSearchPattern,Logger));
        }
    }
}