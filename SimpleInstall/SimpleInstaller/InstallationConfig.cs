using SimpleInstaller.Elements;
using SimpleInstaller.Utilities;


namespace SimpleInstaller
{
    class InstallationConfig
    {
        var installation = InstallationBuilder
            .Init("", "SimpleInstaller.exe",Folder.ProgramFilesFolder.CombinePathWith("SimpleInstaller"), "SimpleInstallerInstallation", "Simple installer")
                .WithRegistryElement(RegistryRoot.HKEY_Local_Machine,"Software\\Microsoft\\Windows\\CurrentVersion\\App Paths", new []{ new RegistryValue("", Folder.FullPrimaryPath)} )
                .WithCopyToProgramsFiles("SimpleInstaller")
                .WithContextFolderMenuItem("customjvk", "Open installer here")
                .WithShortcutElement(Folder.Desktop,"Jeppes custom link")
                .WithShortcutElement(Folder.AllProgramsFolder.CombinePathWith("Simple Installer"),"Simple Installer")
            .Complete(true);

    }
}
