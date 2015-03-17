<Query Kind="Program">
  <Reference Relative="..\..\SimpleInstaller\bin\Debug\SimpleInstaller.exe">&lt;MyDocuments&gt;\Visual Studio 2013\Projects\Upload\Installer\SimpleInstall\SimpleInstaller\bin\Debug\SimpleInstaller.exe</Reference>
  <Namespace>SimpleInstaller.Utilities</Namespace>
</Query>

void Main()
{
	InstallationBuilder
            .Init("debug", "Upload.exe",Folder.ProgramFilesFolder.CombinePathWith("Jaykid Enterprises"), "Ftp Upload", "Ftp upload")                
                .WithCopyToProgramsFiles("Ftp Upload")
                .WithContextFolderMenuItem("Ftp Upload", "Upload folder til ftp")
                .WithShortcutElement(Folder.Desktop, "Upload til ftp")
                .WithShortcutElement(Folder.AllProgramsFolder.CombinePathWith("Simple Installer"), "Simple Installer")
            .Complete(true).Dump(); 
}

// Define other methods and classes here