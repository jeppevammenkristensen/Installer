using System;
using System.IO;
using ConfigR;
using SimpleInstaller.Infrastructure;

namespace SimpleInstaller.Elements.Loaders
{
    public class ConfigRTester : Chain<Installation>
    {
        private readonly string _folder;

        public ConfigRTester(string folder)
        {
            _folder = folder;
        }

        private string FullPath
        {
            get { return Path.Combine(_folder, "InstallationConfig.csx"); }
        }

        public override bool IsMatch
        {
            get { return File.Exists(FullPath); }
        }

        public override Installation GetValue()
        {
            Config.Global.LoadScriptFile(FullPath, typeof (ConfigRTester).Assembly);
            if (Config.Global.ContainsKey("installation"))
                return Config.Global.Get<Installation>("installation");
            
            throw new InvalidOperationException(
                    "Was able to load configuration, but expected element with key \"installation\" was not found");
        }
    }
}