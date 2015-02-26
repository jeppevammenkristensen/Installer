using System;
using SimpleInstaller.Infrastructure;

namespace SimpleInstaller.Elements.Loaders
{
    public class InstallationResolver
    {
        public Installation GetInstallation()
        {
            var result = new ChainOfResponsibility<Installation>(
                     new ConfigRTester(InstallationArguments.Instance.SourceFolder),
                     new ConfigRTester(AppDomain.CurrentDomain.BaseDirectory)
                ).TryGetMatch();

            if (result.Item1)
                return result.Item2;
            throw new InvalidOperationException("Unable to run installation. No matching configuration was found see <tobefilledout> guide");
        }
    }
}