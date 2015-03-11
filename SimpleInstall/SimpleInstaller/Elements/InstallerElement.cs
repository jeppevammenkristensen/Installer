using System;
using System.Threading.Tasks;

namespace SimpleInstaller.Elements
{
    public abstract class InstallerElement
    {
        public abstract string Name { get; protected set; }

        public Action<string> Logger = delegate { };

        public abstract Task InstallAsync();
    }
}
