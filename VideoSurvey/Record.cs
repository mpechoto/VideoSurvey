using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoSurvey
{
    public class Record
    {
        public string Name { get; set; }
        public decimal Age { get; set; }
        public string Gender { get; set; }
        public List<string> Videos { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Answer { get; set; }
    }

    public class Survey
    {
        public Record Record { get; set; }
        public string Video { get; set; }
        public List<Question> Question { get; set; }
    }

    public class Answers
    {
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Q5 { get; set; }
        public string Q6 { get; set; }
    }

    public class Video
    {
        public string VideoName { get; set; }
        public Answers Answers { get; set; }
    }

    public class VideosCollection
    {
        public ICollection<Video> Videos { get; set; }

        /*public VideosCollection()
        {
            this.Videos = new List<Video>
            {
                new Video { Answers = new Answers() }
            };
        }*/
    }
}

//Pensar no Survey, faz sentido deixar o Record?
//Pensar no loop com Lista https://stackoverflow.com/questions/33081102/json-add-new-object-to-existing-json-file-c-sharp