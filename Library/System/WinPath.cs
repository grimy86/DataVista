using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVista.System
{
    public class WinPath
    {
        public static string Desktop
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.Desktop); } }
        public static string Programs
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.Programs); } }
        public static string MyDocuments
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); } }
        public static string Favorites
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.Favorites); } }
        public static string Startup
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.Startup); } }
        public static string Recent
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.Recent); } }
        public static string MyMusic
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic); } }
        public static string MyVideos
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyVideos); } }
        public static string DesktopDirectory
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); } }
        public static string MyComputer
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer); } }
        public static string ApplicationData
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); } }
        public static string LocalApplicationData
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); } }
        public static string InternetCache
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.InternetCache); } }
        public static string Cookies
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.Cookies); } }
        public static string History
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.History); } }
        public static string Windows
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.Windows); } }
        public static string System
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.System); } }
        public static string ProgramFiles
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles); } }
        public static string MyPictures
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); } }
        public static string UserProfile
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); } }
        public static string SystemX86
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.SystemX86); } }
        public static string ProgramFilesX86
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86); } }
        public static string AdminTools
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.AdminTools); } }
        public static string Resources
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.Resources); } }
        public static string LocalizedResources
        { get { return Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources); } }
    }
}
