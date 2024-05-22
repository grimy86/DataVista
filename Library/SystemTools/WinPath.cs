using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVista.SystemTools
{
    public sealed class WinPath
    {
        private static SpecialFolderType _folderType;
        private string _path = _folderType.ToString();

        public WinPath(SpecialFolderType folderType)
        {
            _folderType = folderType;
        }

        public static SpecialFolderType FolderType
        {
            get { return _folderType; }
            set { _folderType = value; }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public static string GetSpecialFolderPath(SpecialFolderType folderType)
        {
            return Environment.GetFolderPath((Environment.SpecialFolder)folderType);
        }

        public enum SpecialFolderType
        {
            Desktop,
            Programs,
            MyDocuments,
            Favorites,
            Startup,
            Recent,
            MyMusic,
            MyVideos,
            DesktopDirectory,
            MyComputer,
            ApplicationData,
            LocalApplicationData,
            InternetCache,
            Cookies,
            History,
            Windows,
            System,
            ProgramFiles,
            MyPictures,
            UserProfile,
            SystemX86,
            AdminTools,
            Resources,
            LocalizedResources
        }
    }
}
