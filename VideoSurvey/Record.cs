using System.Collections.Generic;


namespace VideoSurvey
{
    public class Record
    {
        public string Name { get; set; }
        public decimal Age { get; set; }
        public string Gender { get; set; }
        public List<string> Videos { get; set; }
    }
   
    public class Answers_OLD
    {
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Q5 { get; set; }
        public string Q6 { get; set; }
    }

    public class VideosCollection
    {
        public ICollection<Video> Videos { get; set; }
    }

    public class Video
    {        
        public string VideoName { get; set; }        
        public ICollection<Answers> Answers { get; set; }
    }

    public class Answers
    {        
        public int Id { get; set; }        
        public string Answer { get; set; }
    }
}