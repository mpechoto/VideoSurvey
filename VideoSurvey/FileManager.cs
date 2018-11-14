using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using System.Text;

namespace VideoSurvey
{
    public class FileManager
    {
        //DirectoryInfo DirectoryInfo { get; set; }
        public Record Record { get; set; }
        public VideosCollection VideosCollection { get; set; }
        public Answers Answers { get; set; }

        public string ParentPath { get; private set; }
        public string CurrentPath { get; private set; }
        public string RecordsPath { get; private set; }
        public string VideosPath { get; private set; }
        public string FileName { get; set; }        
        public string NextVideo { get; set; }
        public int Cont = 0;
        public int Qtde = 0;

        public FileManager()
        {
            //Get the Parent Path C:\..\..\..\..\VideoSurvey      

            //DirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent;
            //DirectoryInfo = new DirectoryInfo(teste);
            //ParentPath = DirectoryInfo.FullName;
            ParentPath = GetMyRootPath(Directory.GetCurrentDirectory());
            VideosPath = ParentPath + @"\SampleSource";            
            Answers = new Answers();
        }

        public string GetMyRootPath(string filePath)
        {
            string directoryName;
            int i = 0;
            while (filePath.Contains(@"\VideoSurvey\VideoSurvey"))
            {
                directoryName = Path.GetDirectoryName(filePath);
                //Console.WriteLine("GetDirectoryName('{0}') returns '{1}'",
                    //filePath, directoryName);
                filePath = directoryName;
                if (i == 1)
                {
                    filePath = directoryName + @"\";  // this will preserve the previous path
                }
                i++;
            }
            return filePath;
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
            List<string> fileList = new List<string>(Directory.GetFiles(VideosPath));
            List<string> randomList = new List<string>();
            Random rand = new Random();
            int randomIndex = 0;

            //bypass to debug, remove 27 videos
            fileList.RemoveRange(2,27);

            while (fileList.Count > 0)
            {
                randomIndex = rand.Next(0, fileList.Count);//Choose a random object in the list
                randomList.Add(fileList[randomIndex]);//add it to the new, random list
                fileList.RemoveAt(randomIndex);//remove to avoid duplicates
            }
            return randomList; //return the new random list
        }

        public void WriteRecordJson(string file, Record record)
        {
            File.WriteAllText(System.IO.Path.Combine(CurrentPath, file),
                JsonConvert.SerializeObject(record,Formatting.Indented));
        }

        public Record LoadRecordJson(string filename)
        {
            Record record = JsonConvert.DeserializeObject<Record>(File.ReadAllText(filename));
            return record;
        }                

        public string GetNextVideo()
        {
            if (Record == null)
            {
                Record = LoadRecordJson(System.IO.Path.Combine(CurrentPath, FileName));
                NextVideo = Record.Videos[Cont++];
                Qtde = Record.Videos.Count;
                return NextVideo;
            }
            else 
            {
                if (Cont < Qtde)
                {
                    NextVideo = Record.Videos[Cont++];
                    return NextVideo;
                }
                else return "end";
            }          
        }

        public void SaveSurvey()
        {
            Video video = new Video {
                VideoName = NextVideo,
                Answers = this.Answers
            };            

            string filePath = System.IO.Path.Combine(CurrentPath, "Survey.txt");

            if (File.Exists(filePath))
            {
                VideosCollection = JsonConvert.DeserializeObject<VideosCollection>
                (File.ReadAllText(filePath));

                VideosCollection.Videos.Add(video);

                File.WriteAllText(filePath,JsonConvert.SerializeObject
                    (VideosCollection, Formatting.Indented));
            }
            else
            {
                ICollection<Video> ts = new List<Video>();
                ts.Add(video);
                VideosCollection = new VideosCollection { Videos = ts};

                File.WriteAllText(filePath,
                    JsonConvert.SerializeObject(VideosCollection, Formatting.Indented));
            } 
        }
    }
}
