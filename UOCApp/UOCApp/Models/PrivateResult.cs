using System;
using System.Collections.Generic;
using System.Text;

namespace UOCApp.Models
{
    class PrivateResult
    {


        public int result_id { get; set; }
        public string student_name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string school_name { get; set; }
        public string sortableDate { get; set; }
        public double sortableTime { get; set; }
    }
}
