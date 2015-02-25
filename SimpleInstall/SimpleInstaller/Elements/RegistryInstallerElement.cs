using System;
using System.Threading.Tasks;
using Microsoft.Win32;
using SimpleInstaller.Annotations;

namespace SimpleInstaller.Elements
{
    public class RegistryInstallerElement : InstallerElement
    {
        public Func<RegistryKey> _rootRetriever { get; set; }
        public string[] Paths { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public RegistryInstallerElement([NotNull] string name, [NotNull] Func<RegistryKey> rootRetriever, [NotNull] string[] paths,
            [NotNull] string value, string type = null)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (rootRetriever == null) throw new ArgumentNullException("rootRetriever");
            if (paths == null) throw new ArgumentNullException("paths");
            if (value == null) throw new ArgumentNullException("value");

            _rootRetriever = rootRetriever;
            Paths = paths;
            Value = value;
            Type = type;
            Name = name;
        }

        public override string Name
        {
            get;protected set; 
        }

        public override async Task InstallAsync()
        {
            await Task.Run(() =>
            {
                var currentKey = _rootRetriever();
                RegistryKey currentCandidate;
                if (currentKey == null)
                    throw new InvalidOperationException("Failed to retrieve root key");

                foreach (var path in Paths)
                {
                    currentCandidate = currentKey.OpenSubKey(path, true);
                    if (currentCandidate == null)
                    {
                        currentCandidate = currentKey.CreateSubKey(path);
                        Logger(string.Format("Created subkey {0} for {1}", path, currentKey.ToString()));
                    }

                    currentKey = currentCandidate;
                }

                if (currentKey != null)
                    currentKey.SetValue("", Value);

                Logger("Ended registrycreation");
            });
        }
    }
}