using System;
using System.Linq;
using Microsoft.Web.Administration;
using Roslyn.Compilers;

namespace SimpleInstaller.Elements
{
    public enum InstallationMode
    {
        Install
    }

    public class InstallationArguments
    {
        private static InstallationArguments _installationArguments;
        
        public static InstallationArguments Initialize(string sourceFolder, InstallationMode mode = InstallationMode.Install)
        {
            _installationArguments = new InstallationArguments(sourceFolder, mode);
            return _installationArguments;
        }

        public static InstallationArguments Instance
        {
            get
            {
                if (_installationArguments == null)
                    throw new InvalidOperationException("You are trying to call Instance before it has been initialized.");

                return _installationArguments;
            }
        }

        public InstallationMode Mode { get; private set; }
        public string SourceFolder { get; private set; }

        private InstallationArguments(string sourceFolder, InstallationMode mode)
        {
            SourceFolder = sourceFolder;
            Mode = mode;
        }

        public static InstallationArguments Parse(string[] arguments)
        {
            if (arguments == null || arguments.Length == 0)
                return Initialize(AppDomain.CurrentDomain.BaseDirectory);
            else
            {
                return Initialize(arguments[0]);
            }
        }
    }
}