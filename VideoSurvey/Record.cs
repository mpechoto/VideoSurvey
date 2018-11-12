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
}

//Pensar no Surver, faz sentido deixar o Record?
//Pensar no loop com Lista https://stackoverflow.com/questions/33081102/json-add-new-object-to-existing-json-file-c-sharp