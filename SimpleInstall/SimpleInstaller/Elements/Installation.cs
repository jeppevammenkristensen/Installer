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

        /// <summary>
        /// Call this method when all methods has been added
        /// </summary>
        public void FinishedAddingElements()
        {
            
        }
    }
}