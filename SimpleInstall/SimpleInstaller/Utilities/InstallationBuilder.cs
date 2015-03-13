using System;
using SimpleInstaller.Annotations;
using SimpleInstaller.Elements;

namespace SimpleInstaller.Utilities
{
    public class InstallationBuilder
    {
        // For the record this is not perfect to have such a hard dependency on sourceFolder, primaryExecutablePath and destinationFolder
        // But for the time being this is most common scenario for installation.
        public static InstallationBuilder Init([NotNull] string sourceFolder, [NotNull] string primaryExecutablePath,
            [NotNull] string destinationFolder, string uniqueName, string displayName)
        {
            if (sourceFolder == null) throw new ArgumentNullException("sourceFolder");
            if (primaryExecutablePath == null) throw new ArgumentNullException("primaryExecutablePath");
            if (destinationFolder == null) throw new ArgumentNullException("destinationFolder");

            return new InstallationBuilder(sourceFolder, primaryExecutablePath, destinationFolder, uniqueName, displayName);
        }

        internal Installation Result { get; set; }

        public string FullPrimaryExecutionPath
        {
            get { return Result.DestinationFolder.CombinePathWith(Result.PrimaryExecutablePath); }
        }

        private InstallationBuilder(string sourceFolder, string primaryExectablePath, string destinationFolder, string uniqueName, string displayName)
        {
            Result = new Installation {SourceFolder = sourceFolder, PrimaryExecutablePath = primaryExectablePath, DestinationFolder = destinationFolder, UniqueName = uniqueName, DisplayName = displayName};
            Folder.FullPrimaryPath = FullPrimaryExecutionPath;
        }

        public InstallationBuilder WithElement(InstallerElement installerElement)
        {
            Result.Elements.Add(installerElement);
            return this;
        }

        public Installation Complete(bool addUninstallLink)
        {
            if (addUninstallLink)
                Result.Elements.Add(new AddUninstallElement("All your base are belong to later implented",Result.UniqueName, Result.DisplayName));

            return Result;
        }
    }
}