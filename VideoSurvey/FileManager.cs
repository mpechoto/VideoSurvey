using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace VideoSurvey
{
    public class FileManager
    {
        DirectoryInfo DirectoryInfo { get; set; }
        public Record Record { get; set; }
        public Question Question { get; set; }
        public Survey Survey { get; set; }
        public List<Question> ListQuestion { get; set; }

        public VideosCollection VideosCollection { get; set; }
        public Answers Answers { get; set; }

        public string ParentPath { get; private set; }
        public string CurrentPath { get; private set; }
        public string RecordsPath { get; private set; }
        public string VideosPath { get; private set; }
        public string FileName { get; set; }
        //public string CurrentVideo { get; set; }
        public string NextVideo { get; set; }
        public int Cont = 0;
        public int Qtde = 0;

        public FileManager()
        {
            //Get the Parent Path C:\Users\user\source\repos\VideoSurvey
            DirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent;
            ParentPath = DirectoryInfo.FullName;
            VideosPath = ParentPath + "\\SampleSource";
            
            Answers = new Answers();
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

            //bypass to debug
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

        /*public void WriteSurveyJson(string file, Survey survey)
        {
            File.WriteAllText(System.IO.Path.Combine(CurrentPath, file),
                JsonConvert.SerializeObject(survey, Formatting.Indented));
        }

        public Survey LoadSurveyJson(string filename)
        {
            Survey survey = JsonConvert.DeserializeObject<Survey>(File.ReadAllText(filename));
            return survey;
        }*/

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
        
        /*public void UpdateSurvey(int idQuestion, string answer)
        {
            if (Survey == null)
            {

                Question = new Question();
                ListQuestion = new List<Question>();
                
                Question.Id = idQuestion;
                Question.Answer = answer;
                ListQuestion.Add(Question);

                Survey = new Survey
                {
                    Record = Record,

                    Question = ListQuestion
                };

                WriteSurveyJson("Survey.txt",Survey);
            }
            else
            {
                Survey = LoadSurveyJson(System.IO.Path.Combine(CurrentPath, "Survey.txt"));

                Question.Id = idQuestion;
                Question.Answer = answer;
                ListQuestion = Survey.Question;

                ListQuestion.Add(Question);
                Survey.Question = ListQuestion;
                WriteSurveyJson("Survey.txt",Survey);
            }
            
        }*/

    }
}
