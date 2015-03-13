using System.Collections.Generic;

namespace SimpleInstaller.Elements
{
    public class Installation
    {
        public Installation()
        {
            Elements = new List<InstallerElement>();
        }

        public string Name { get; set; }
        
        public string Path { get; set; }

        public List<InstallerElement> Elements { get; set; }
        
        public string SourceFolder { get; set; }
        public string PrimaryExecutablePath{ get; set; }
        public string DestinationFolder { get; set; }
        public string UniqueName { get; set; }
        public string DisplayName { get; set; }
    }
}