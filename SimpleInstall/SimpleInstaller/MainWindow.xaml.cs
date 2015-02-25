using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;
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
            installation.Elements.Add(new FolderInstallerElement("Grund installation"){ DestinationFolder = installation.Path, SourceFolder = "."});
            
            installation.Elements.Add(new RegistryInstallerElement("Installer \\directory\\shell\\Custom installation",() => Registry.ClassesRoot, new []{"Directory","shell","Custom installation"}, "Custom oprettet"));
            installation.Elements.Add(new RegistryInstallerElement("Installer \\directory\\shell\\Custom installation\\command",() => Registry.ClassesRoot, new []{"Directory","shell","Custom installation","command"}, Path.Combine(installation.Path,"SimpleInstaller.exe") + " \"%L%\""));
            installation.Elements.Add(new CreateShortcutElement(Path.Combine(installation.Path, "SimpleInstaller.exe"), Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SimpleInstaller"));


            Scope = new InstallationViewModel();
            Scope.SetInstaller(installation);
            DataContext = Scope;
        }

        private async void Install_Clicked(object sender, RoutedEventArgs e)
        {
            await Scope.Install();
        }
    }
}
