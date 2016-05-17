using System;
using System.Collections.Generic;
using System.Text;
using UOCApp.Helpers;

namespace UOCApp.Models
{
    public class LeaderboardResult
    {
        
        public LeaderboardResult(RawResult input)
        {
            result_id = Convert.ToInt32(input.result_id);
            student_name = input.student_name;
            time = GetResultsHelper.FormatTime(Convert.ToDecimal(input.time));
            school_name = input.school_name;
            ranked = Convert.ToBoolean(Convert.ToInt32(input.ranked)); //input can be 0 or 1
        }
        

        public int result_id { get; set; }
        public string student_name { get; set; }
        public bool ranked { get; set; }
        public string time { get; set; }
        public string school_name { get; set; }
    }
}
