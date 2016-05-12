using System;
using System.Collections.Generic;
using System.Text;

namespace UOCApp.Models
{
    class RawResult
    {
        public string result_id { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string ranked { get; set; }
        public string flagged { get; set; }
        public string student_name { get; set; }
        public string student_gender { get; set; }
        public string student_grade { get; set; }
        public string school_name { get; set; }

        public override string ToString()
        {
            return result_id + " " + date + " " + time + " " + ranked + " " + student_name + " " + student_gender + " " + student_grade + " " + school_name;

            //return base.ToString();
        }

    }
}
