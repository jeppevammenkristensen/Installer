using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using SimpleInstaller.Elements;

namespace SimpleInstaller.Utilities
{
    public static class Folder
    {
        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner,
           [Out] StringBuilder lpszPath, int nFolder, bool fCreate);
        const int CSIDL_COMMON_STARTMENU = 0x16;  // All Users\Start Menu



        public static string ProgramFilesFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles); }
        }

        public static string Desktop
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); }
        }

        public static string AllProgramsFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu); }
        }

        public static string CurrentExecutionFolder
        {
            get
            {
                return Directory.GetCurrentDirectory();
            }
        }

        public static string FullPrimaryPath { get; internal set; }

        public static string CombinePathWith(this string source, string path)
        {
            return Path.Combine(source, path);
        }
    }

    public static class FolderElementExtensions
    {
        public static InstallationBuilder WithCopyToProgramsFiles(this InstallationBuilder source,
            string pathInProgramFiles, string sourceFolder = null, bool copySubfolders = true, string filter = null)
        {
            return WithCopyFilesToDestination(source, Folder.ProgramFilesFolder.CombinePathWith(pathInProgramFiles),
                sourceFolder, copySubfolders, filter);
        }

        public static InstallationBuilder WithCopyFilesToDestination(this InstallationBuilder source,
            string destinationFolder, string sourceFolder, bool copySubfolders, string filter)
        {
            sourceFolder = sourceFolder ?? source.Result.SourceFolder;
            return source.WithElement(
                new FolderInstallerElement(
                    string.Format("Copy files from {0} to {1} folder", sourceFolder, destinationFolder), sourceFolder,
                    destinationFolder,
                    copySubfolders, filter));
        }
    }

    public static class RegistryPath
    {
        public static string ContextFolderMenuPath(string destinationName)
        {
            return "Directory\\Shell".CombinePathWith(destinationName);
        }
    }
}