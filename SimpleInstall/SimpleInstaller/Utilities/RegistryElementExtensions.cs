using SimpleInstaller.Elements;

namespace SimpleInstaller.Utilities
{
    public static class RegistryElementExtensions
    {
        public static InstallationBuilder WithContextFolderMenuItem(this InstallationBuilder installationBuilder, string destination, string contextMenuText, string executionPath = null)
        {
            var rootPath = RegistryPath.ContextFolderMenuPath(destination);
            var commandPath = rootPath.CombinePathWith("command");

            executionPath = executionPath ?? ( installationBuilder.FullPrimaryExecutionPath + " \"%L%\"");


            return installationBuilder
                .WithRegistryElement(RegistryRoot.HKEY_Classes_Root, rootPath, new[]
                {
                    new RegistryValue("MUIVerb", contextMenuText)
                })
                .WithRegistryElement(RegistryRoot.HKEY_Classes_Root, commandPath, new[]
                {
                    new RegistryValue("", executionPath),
                });
        }

        public static InstallationBuilder WithRegistryElement(this InstallationBuilder installationBuilder, RegistryRoot root, string path, RegistryValue[] values)
        {
            return installationBuilder.WithElement(new RegistryInstallerElement("!!!TEST!!!", root, path, values));
        }
    }
}