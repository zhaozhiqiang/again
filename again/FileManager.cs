using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace again
{
    public static class FileManager
    {
        public const string CONFIG_PATH = "Config";

        public const string PLAY_LIST_PATH = "PlayList";

        public static bool getPlayList(List<string> List)
        {
            if (File.Exists(PLAY_LIST_PATH))
            {
                List = File.ReadAllText(PLAY_LIST_PATH).Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                return true;
            }

            return false;
        }

        public static void savePlayList(List<string> List)
        {
            if (!File.Exists(PLAY_LIST_PATH))
            {
                FileStream file = File.Create(PLAY_LIST_PATH);
                file.Close();
            }

            File.WriteAllLines(PLAY_LIST_PATH, List);
        }

        public static string getMusicFilePath(string path)
        {
            if (File.Exists(CONFIG_PATH))
            {
                return File.ReadAllText(CONFIG_PATH);
            }

            else
            {
                return "";
            }
        }

        public static void getFileList(DirectoryInfo dir, List<string> List)
        {
            foreach (FileInfo fileName in dir.GetFiles())
            {
                List.Add(fileName.FullName);
            }

            foreach (DirectoryInfo dirSub in dir.GetDirectories())
            {
                getFileList(dirSub, List);
            }
        }
    }
}
