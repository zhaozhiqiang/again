using System.IO;
using System.Collections.Generic;

namespace again
{
    class PlayList
    {
        private static List<string> List = new List<string>();

        public PlayList(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            FileManager.getFileList(dir, List);

            FileManager.savePlayList(List);

            FileManager.getPlayList(List);
        }
    }
}
