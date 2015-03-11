using System.Threading.Tasks;
using Microsoft.Win32;
using SimpleInstaller.Utilities;

namespace SimpleInstaller.Elements
{
    public class AddUninstallElement : InstallerElement
    {
        public AddUninstallElement(string uninstallString, string applicationUniqueName, string displayName)
        {
            UninstallString = uninstallString;
            ApplicationUniqueName = applicationUniqueName;
            DisplayName = displayName;
        }

        public string UninstallString { get; set; }
        public string ApplicationUniqueName { get; set; }

        public string DisplayName { get; set; }

        public override string Name
        {
            get { return "Add uninstall information"; }
            protected set { throw new System.NotImplementedException(); }
        }

        public async override Task InstallAsync()
        {
            await Task.Run(() =>
            {
                RegistryHelper.WriteRegistryPath(RegistryRoot.HKEY_Local_Machine,
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + ApplicationUniqueName)
                    .WriteRegistryValue(new RegistryValue("UninstallString", UninstallString))
                    .WriteRegistryValue(new RegistryValue("DisplayName", DisplayName));
            });
        }

        public string Uninstaller { get; set; }

    }
}