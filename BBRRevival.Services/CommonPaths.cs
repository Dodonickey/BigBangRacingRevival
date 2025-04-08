using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services
{
    internal class CommonPaths
    {
        public static readonly string BasePath = "ServerSavedData";
        public static readonly string MinigamesRootPath = Path.Combine(BasePath, "Minigames");
        public static readonly string Levels = "Assets\\Levels";

        public static void CreateRootDirectories()
        {
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }

            if (!Directory.Exists(MinigamesRootPath))
            {
                Directory.CreateDirectory(MinigamesRootPath);
            }

            if (!Directory.Exists(Levels))
            {
                Directory.CreateDirectory(MinigamesRootPath);
            }

            string[] levelFolders = Directory.GetDirectories(Levels);

            foreach (var folder in levelFolders)
            {
                string folderName = Path.GetFileName(folder);
                string targetPath = Path.Combine(MinigamesRootPath, folderName);

                CopyDirectory(folder, targetPath);
            }

        }
        private static void CopyDirectory(string sourceDir, string destinationDir)
        {
            Directory.CreateDirectory(destinationDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationDir, fileName);
                File.Copy(file, destFile, overwrite: true);
            }

            foreach (var subDir in Directory.GetDirectories(sourceDir))
            {
                string subDirName = Path.GetFileName(subDir);
                string destSubDir = Path.Combine(destinationDir, subDirName);
                CopyDirectory(subDir, destSubDir);
            }
        }
    }
}
