using SimpleInstaller.Elements;
using Microsoft.Win32;

var installation = new Installation();
installation.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "CustomInstallation");
installation.Name = "Custom installation";
installation.Elements.Add(new FolderInstallerElement("Grund installation") { DestinationFolder = installation.Path, SourceFolder = "." });

installation.Elements.Add(new RegistryInstallerElement("Installer \\directory\\shell\\Custom installation", () => Registry.ClassesRoot, new[] { "Directory", "shell", "Custom installation" }, "Custom oprettet"));
installation.Elements.Add(new RegistryInstallerElement("Installer \\directory\\shell\\Custom installation\\command", () => Registry.ClassesRoot, new[] { "Directory", "shell", "Custom installation", "command" }, Path.Combine(installation.Path, "SimpleInstaller.exe") + " \"%L%\""));
installation.Elements.Add(new CreateShortcutElement(Path.Combine(installation.Path, "SimpleInstaller.exe"), Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SimpleInstaller"));
//installation.Elements.Add(new CreateIIsWebsite("supertest",installation.Path,"supertest.localtest.me"));

Add("installation", installation);
