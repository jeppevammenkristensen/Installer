using System;
using System.IO;
using System.Threading.Tasks;
using IWshRuntimeLibrary;
using SimpleInstaller.Annotations;

namespace SimpleInstaller.Elements
{
    public class CreateShortcutElement : InstallerElement
    {
        public override string Name
        {
            get { return string.Format("Create shortcut for {0} in {1}", SourcePath, DestinationFolder); }
            protected set { }
        }


        public string SourcePath { get; private set; }

        public string DestinationFolder { get; private set; }
        public string LinkName { get; private set; }

        public CreateShortcutElement([NotNull] string sourcePath, [NotNull] string destinationFolder,
            [NotNull] string linkName)
        {
            if (sourcePath == null) throw new ArgumentNullException("sourcePath");
            if (destinationFolder == null) throw new ArgumentNullException("destinationFolder");
            if (linkName == null) throw new ArgumentNullException("linkName");
            SourcePath = sourcePath;
            DestinationFolder = destinationFolder;
            LinkName = linkName;
        }


        public override async Task InstallAsync()
        {
            string deskDir = DestinationFolder;
            var shell = new WshShell();
            var shortcutLinkFilePath = Path.Combine(deskDir, string.Format(@"{0}.lnk", LinkName));
            var windowsApplicationShortcut = (IWshShortcut) shell.CreateShortcut(shortcutLinkFilePath);
            windowsApplicationShortcut.Description = LinkName;
            windowsApplicationShortcut.WorkingDirectory = new FileInfo(SourcePath).Directory.FullName;
            windowsApplicationShortcut.TargetPath = SourcePath;
            windowsApplicationShortcut.Save();
        }
    }
}