using SimpleInstaller.Elements;
using SimpleInstaller.Utilities;

var installation = new Installation();
installation.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "CustomInstallation");
installation.Name = "Custom installation";
installation.Elements.Add(new FolderInstallerElement("Grund installation",".", installation.Path));

installation.Elements.Add(new RegistryInstallerElement("Installer Context menu",RegistryRoot.HKEY_Classes_Root,"Directory\\Shell\\CustomJeppe",
                    new RegistryValue("MUIVerb", "Custom text her")));
installation.Elements.Add(new RegistryInstallerElement("Installer Context menu",RegistryRoot.HKEY_Classes_Root,"Directory\\Shell\\CustomJeppe\\command",
                    new RegistryValue("", installation.Path)));

installation.Elements.Add(new CreateShortcutElement(Path.Combine(installation.Path, "SimpleInstaller.exe"), Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SimpleInstaller"));
//installation.Elements.Add(new CreateIIsWebsite("supertest",installation.Path,"supertest.localtest.me"));
installation.Elements.Add(new AddUninstallElement("babies for pussies", "Babaganuch","Jeppe Ganuch"));


Add("installation", installation);
