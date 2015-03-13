using System;
using System.Threading.Tasks;
using Microsoft.Win32;
using SimpleInstaller.Annotations;
using SimpleInstaller.Utilities;

namespace SimpleInstaller.Elements
{
    public enum RegistryRoot
    {
        HKEY_Classes_Root,
        HKEY_Current_User,
        HKEY_Local_Machine,
        HKEY_Users,
        HKEY_Current_Config,
    }

    public class RegistryValue
    {
        public RegistryValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class RegistryInstallerElement : InstallerElement
    {
        public RegistryRoot _root { get; set; }
        public string Path { get; set; }
        public RegistryValue[] Value { get; set; }

        public RegistryInstallerElement([NotNull] string name, RegistryRoot root, [NotNull] string path,
            params RegistryValue[] value)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (path == null) throw new ArgumentNullException("path");

            _root = root;
            Path = path;
            Value = value;
            Name = name;
        }

        public override string Name
        {
            get; set; 
        }

        public override async Task InstallAsync()
        {
            await Task.Run(() =>
            {
                
                Logger("Beginning registry creation");

                var path = RegistryHelper.WriteRegistryPath(_root, Path);
                Logger(string.Format("Wrote path {0}", Path));

                foreach (var registryValue in Value)
                {
                    path.WriteRegistryValue(registryValue);
                }
                Logger("Ended registrycreation");
            });
        }
    }
}