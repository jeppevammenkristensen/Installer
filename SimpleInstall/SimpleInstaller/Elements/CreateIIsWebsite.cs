using System.Threading.Tasks;
using Microsoft.Web.Administration;

namespace SimpleInstaller.Elements
{
    public class CreateIIsWebsite : InstallerElement
    {
        public override string Name
        {
            get { return string.Format("Install website {0}", SiteName); }
            set {  }
        }

        public CreateIIsWebsite(string siteName, string sitePath, string hostName, int port = 8080)
        {
            Port = port;
            SitePath = sitePath;
            SiteName = siteName;
            HostName = hostName;
        }

        // This is far from done
        public override async Task InstallAsync()
        {
            var serverManager = new ServerManager();
            Site mySite = serverManager.Sites.Add(SiteName,"http:", HostName, SitePath);
            
            var appPool = serverManager.ApplicationPools.Add(HostName);
            appPool.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
            appPool.ProcessModel.UserName = @"jvk@somedomain.dk";
            appPool.ProcessModel.Password = "blablabla";
            mySite.ApplicationDefaults.ApplicationPoolName = appPool.Name;
            serverManager.CommitChanges();
            Logger("Created site");
        }

        public int Port { get; set; }

        public string SitePath { get; set; }

        public string SiteName { get; set; }

        public string HostName { get; set; }
    }
}