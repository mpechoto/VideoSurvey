using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace VideoSurvey
{
    public class FileManager
    {        
        public Record Record { get; set; }
        public VideosCollection VideosCollection { get; set; }
        public ICollection<Answers> Answers { get; set; }

        public string User { get; set; }
        public string Participant { get; set; }
        public string ParentPath { get; private set; }
        public string CurrentPath { get; private set; }
        public string RecordsPath { get; private set; }
        public string VideosPath { get; private set; }
        public string FileName { get; set; }        
        public string NextVideo { get; set; }
        public int Cont = 0;
        public int Qtde = 0;
        public bool TestSession { get; set; }
        public bool CheckDir { get; set; }

        public FileManager()
        {
            //Get the Parent Path C:\..\..\..\..\VideoSurvey      
            ParentPath = GetMyRootPath(Directory.GetCurrentDirectory());

            if (Directory.Exists(ParentPath + @"\SampleSource"))
            {
                VideosPath = ParentPath + @"\SampleSource";
                CheckDir = true;
            }
            else
                CheckDir = false;

            Answers = new List<Answers>();
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
                        next = current;                    
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
            //fileList.RemoveRange(2, 27);

            while (fileList.Count > 0)
            {
                randomIndex = rand.Next(0, fileList.Count);//Choose a random object in the list
                randomList.Add(fileList[randomIndex]);//add it to the new, random list
                fileList.RemoveAt(randomIndex);//remove to avoid duplicates
            }
            randomList.Add(VideosPath + @"\init\teste.mp4"); //Insert the Test Video in the last position
            return randomList; //return the new random list            
        }

        public void WriteRecordJson(string file, Record record)
        {
            File.WriteAllText(System.IO.Path.Combine(CurrentPath, file),
                JsonConvert.SerializeObject(record,Formatting.Indented));
            Record = record;// LoadRecordJson(System.IO.Path.Combine(CurrentPath, FileName));
            Qtde = Record.Videos.Count - 1; //Verificar se dá erro
        }

        public Record LoadRecordJson(string filename)
        {
            Record record = JsonConvert.DeserializeObject<Record>(File.ReadAllText(filename));
            return record;
        }

        public void PrintRecord(string json)
        {
            Record record = JsonConvert.DeserializeObject<Record>(File.ReadAllText(json));
            Console.WriteLine("Name: {0}\nIdade: {1}\nSexo: {2}", record.Name, record.Age, record.Gender);
            record.Videos.ForEach(i => Console.WriteLine("{0}", i));
        }
        
        public void PrintSurvey(string json)
        {
            VideosCollection videosCollection = JsonConvert.DeserializeObject<VideosCollection>
                (File.ReadAllText(json));
            foreach (var video in videosCollection.Videos)
            {
                Console.WriteLine(video.VideoName);
                foreach (var answers in video.Answers)
                    Console.WriteLine("Q"+ answers.Id + ": " + answers.Answer);
            }
        }

        public string GetNextVideo()
        {   //if (Record == null)
            //{
                //Record = LoadRecordJson(System.IO.Path.Combine(CurrentPath, FileName));
            NextVideo = Record.Videos[Cont++];            
            return NextVideo;
            // return NextVideo;
            //}
            //else 
            //{
                //if (Cont < Qtde)
               // {
                //    NextVideo = Record.Videos[Cont++];
                //    return NextVideo;
                //}
                //else return "end";
           // }          
        }

        public void SaveSurvey()
        {
            Video video = new Video {
                VideoName = NextVideo,
                Answers = this.Answers
            };

            string filePath = System.IO.Path.Combine(CurrentPath, "Survey.txt");

            if (File.Exists(filePath))
            {   //Load Json File
                VideosCollection = JsonConvert.DeserializeObject<VideosCollection>
                (File.ReadAllText(filePath));
                
                VideosCollection.Videos.Add(video);
                //Save Jsn File
                File.WriteAllText(filePath,JsonConvert.SerializeObject
                    (VideosCollection, Formatting.Indented));
            }
            else
            {
                ICollection<Video> ts = new List<Video>();
                ts.Add(video);
                VideosCollection = new VideosCollection { Videos = ts};
                //Save Jsn File
                File.WriteAllText(filePath,
                    JsonConvert.SerializeObject(VideosCollection, Formatting.Indented));
            }
            this.Answers.Clear();
        }
    }
}