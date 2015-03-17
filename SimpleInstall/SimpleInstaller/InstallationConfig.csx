using SimpleInstaller.Elements;
using SimpleInstaller.Utilities;

Add("installation", InstallationBuilder
            .Init("", "SimpleInstaller.exe",Folder.ProgramFilesFolder.CombinePathWith("SimpleInstaller"), "SimpleInstallerInstallation", "Simple installer")                
                .WithCopyToProgramsFiles("SimpleInstaller")
                .WithContextFolderMenuItem("customjvk", "Open installer here")
                .WithShortcutElement(Folder.Desktop, "Jeppes custom link")
                .WithShortcutElement(Folder.AllProgramsFolder.CombinePathWith("Simple Installer"), "Simple Installer")
            .Complete(true));           
