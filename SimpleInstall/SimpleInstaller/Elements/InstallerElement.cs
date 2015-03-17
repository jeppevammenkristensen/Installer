using System;
using System.Threading.Tasks;

namespace SimpleInstaller.Elements
{
    public abstract class InstallerElement
    {
        public virtual string Name { get; set; }

        public Action<string> Logger = delegate { };

        public abstract Task InstallAsync();

        public abstract Task UninstallAsync();
    }
}
