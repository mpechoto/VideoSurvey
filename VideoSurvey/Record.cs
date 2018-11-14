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
    }
}