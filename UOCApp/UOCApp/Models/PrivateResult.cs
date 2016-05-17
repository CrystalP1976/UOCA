using System;
using System.Collections.Generic;
using System.Text;

namespace UOCApp.Models
{
    public class PrivateResult
    {
        public PrivateResult(Result input)
        {
            result_id = Convert.ToInt32(input.result_id);
            student_name = input.student_name;
            date = input.date;
            time = input.time.ToString(); //TODO use helper to convert nicely
            sortableDate = input.date;
            sortableTime = Convert.ToDouble(input.time);
        }

        public int result_id { get; set; }
        public string student_name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string sortableDate { get; set; }
        public double sortableTime { get; set; }
        //TODO obstacle handling
        public bool missedObstacle { get; set; }
    }
}
