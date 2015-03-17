using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using SimpleInstaller.Elements;
using SimpleInstaller.Elements.Loaders;
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

            var installationDetector = new InstallationResolver();
            var installation = installationDetector.GetInstallation();

            // the commented out below is to be used by a configr file

            Scope = new InstallationViewModel();
            Scope.SetInstaller(installation);
            DataContext = Scope;
        }

        private async void Install_Clicked(object sender, RoutedEventArgs e)
        {
            await Scope.Install();
        }

        private async void Uninstall_Clicked(object sender, RoutedEventArgs e)
        {
            await Scope.Uninstall();
        }
    }
}
