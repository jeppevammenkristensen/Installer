using SimpleInstaller.Elements;

namespace SimpleInstaller.Utilities
{
    public static class ShortcutElementExtensions
    {
        public static InstallationBuilder WithShortcutElement(this InstallationBuilder installationBuilder, string destinationFolder, string linkName, string sourcePath = null)
        {
            sourcePath = sourcePath ?? installationBuilder.FullPrimaryExecutionPath;

            return installationBuilder.WithElement(new CreateShortcutElement(sourcePath, destinationFolder, linkName));
        }

    }
}