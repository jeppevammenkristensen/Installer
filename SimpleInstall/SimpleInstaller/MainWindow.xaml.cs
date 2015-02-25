using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using SimpleInstaller.Elements;
using SimpleInstaller.ViewModels;

namespace SimpleInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public InstallationViewModel Scope { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            var installation = new Installation();
            installation.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "CustomInstallation");
            installation.Name = "Custom installation";
            installation.Elements.Add(new FolderInstallerElement(){ DestinationFolder = installation.Path, Name = "Grund installation", SourceFolder = "."});

            Scope = new InstallationViewModel();
            Scope.SetInstaller(installation);
            this.DataContext = Scope;
        }

        private async void Install_Clicked(object sender, RoutedEventArgs e)
        {
            await Scope.Install();
        }
    }
}
