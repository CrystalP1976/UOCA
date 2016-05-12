using System;
using System.Collections.Generic;
using System.Text;

namespace UOCApp.Models
{
    class LeaderboardResult
    {
        
        public LeaderboardResult(RawResult input)
        {
            result_id = Convert.ToInt32(input.result_id);
            student_name = input.student_name;
            time = input.time;
            school_name = input.school_name;
            ranked = Convert.ToBoolean(input.ranked); //input can be 0 or 1
        }
        

        public int result_id { get; set; }
        public string student_name { get; set; }
        public bool ranked { get; set; }
        public string time { get; set; }
        public string school_name { get; set; }
    }
}
