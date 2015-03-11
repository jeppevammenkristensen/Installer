using System;
using System.Linq;
using System.Windows.Documents;
using Microsoft.Win32;
using SimpleInstaller.Annotations;
using SimpleInstaller.Elements;

namespace SimpleInstaller.Utilities
{
    public static class RegistryHelper
    {
        public static RegistryKey WriteRegistryPath(RegistryRoot registryRoot, string completePath)
        {
            var Paths = completePath.Split('\\');

            var currentKey = GetRegistryRoot(registryRoot);
            
            RegistryKey currentCandidate;
            if (currentKey == null)
                throw new InvalidOperationException("Failed to retrieve root key");

            foreach (var path in Paths)
            {
                currentCandidate = currentKey.OpenSubKey(path, true);
                if (currentCandidate == null)
                {
                    currentCandidate = currentKey.CreateSubKey(path);
                }

                currentKey = currentCandidate;
            }

            if (currentKey == null)
                throw new InvalidOperationException(string.Format("Unable to create the path {0}\\{1}", registryRoot, completePath));

            return currentKey;
        }

        public static RegistryKey WriteRegistryValue([NotNull] this RegistryKey currentKey,
            [NotNull] RegistryValue value)
        {
            if (currentKey == null) throw new ArgumentNullException("currentKey");
            if (value == null) throw new ArgumentNullException("value");
            
            currentKey.SetValue(value.Name, value.Value);
            return currentKey;
        }

        private static RegistryKey GetRegistryRoot(RegistryRoot registryRoot)
        {
            switch (registryRoot)
            {
                case RegistryRoot.HKEY_Classes_Root:
                    return Registry.ClassesRoot;
                case RegistryRoot.HKEY_Current_User:
                    return Registry.CurrentUser;
                case RegistryRoot.HKEY_Local_Machine:
                    return Registry.LocalMachine;
                case RegistryRoot.HKEY_Users:
                    return Registry.Users;
                case RegistryRoot.HKEY_Current_Config:
                    return Registry.CurrentConfig;
                default:
                    throw new ArgumentOutOfRangeException("registryRoot");
            }
        }
    }
}