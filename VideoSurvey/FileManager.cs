using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace VideoSurvey
{
    public class FileManager
    {
        DirectoryInfo DirectoryInfo { get; set; }
        Record Record { get; set; }
        public string ParentPath { get; private set; }
        public string CurrentPath { get; private set; }
        public string RecordsPath { get; private set; }
        public string VideosPath { get; private set; }
        public string FileName { get; set; }
        public string CurrentVideo { get; set; }
        public string NextVideo { get; set; }
        public int cont = 0;

        public FileManager()
        {
            //Get the Parent Path C:\Users\user\source\repos\VideoSurvey
            DirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent;
            ParentPath = DirectoryInfo.FullName;
            VideosPath = ParentPath + "\\SampleSource";
        }

        public void CreateRecordsFolder(string folderName = "\\Records")
        {
            //Create the folder "Records" if it does not exist. There's no need to do an explicit check first
            //This folder will record all user data
            RecordsPath = ParentPath + folderName;
            Directory.CreateDirectory(RecordsPath);
        }

        public void CreateSampleFolder()
        {
            int current = 0;
            int next = 0;
            try
            {
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(RecordsPath));
                foreach (var dir in dirs)
                {
                    var subs = dir.Split('_');
                    current = Int32.Parse(subs[1]);

                    if (next < current)
                    {
                        next = current;
                    }
                }
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }

            string date = DateTime.Now.ToString("dd-MM-yyyy_HH-mm");
            //Console.WriteLine(date);
            next += 1;
            string folder = @"Record_" + next + "_@" + date;
            CurrentPath = RecordsPath + "\\" + folder;
            Directory.CreateDirectory(CurrentPath);
        }

        public List<string> VideoRandomList()
        {
            //DirectoryInfo root = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent;
            //string path = root.FullName + @"\VideoSurvey\public\";
            //Console.WriteLine("path {0}",path);

            List<string> fileList = new List<string>(Directory.GetFiles(VideosPath));
            List<string> randomList = new List<string>();
            Random rand = new Random();
            int randomIndex = 0;

            while (fileList.Count > 0)
            {
                randomIndex = rand.Next(0, fileList.Count);//Choose a random object in the list
                randomList.Add(fileList[randomIndex]);//add it to the new, random list
                fileList.RemoveAt(randomIndex);//remove to avoid duplicates
            }
            return randomList; //return the new random list
        }

        public void WriteJson(string file, Record record)
        {
            File.WriteAllText(System.IO.Path.Combine(CurrentPath, file),
                JsonConvert.SerializeObject(record,Formatting.Indented));
        }

        public Record LoadJson(string filename)
        {
            Record record = JsonConvert.DeserializeObject<Record>(File.ReadAllText(filename));
            return record;
        }

        public string GetNextVideo()
        {
            if (Record == null)
            {
                Record = LoadJson(System.IO.Path.Combine(CurrentPath, FileName));
                CurrentVideo = Record.Videos[cont];
                return CurrentVideo;
            }
            else
            {
                NextVideo = Record.Videos[cont++];
                return NextVideo;
            }          
        }

    }
}
