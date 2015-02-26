using System.Windows;
using System.Windows.Threading;
using SimpleInstaller.Elements;

namespace SimpleInstaller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            InstallationArguments.Parse(e.Args);
        }

        private void Virk(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "No soup for you");
            
        }
    }
}
