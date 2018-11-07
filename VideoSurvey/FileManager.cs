using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VideoSurvey
{
    class FileManager
    {
        public string RootPath { get; private set; }
        DirectoryInfo Root { get; set; }

        public FileManager()
        {
            //Get the Parent Path C:\Users\user\source\repos\VideoSurvey
            Root = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent;
            RootPath = Root.FullName;                        
        }


        public void CreateRecordsFolder(string folderName = "\\Records")
        {
            //Create the folder "Records" if it does not exist. There's no need to do an explicit check first
            //This folder will record all user data
            Directory.CreateDirectory(RootPath + folderName);
        }



    }
}
