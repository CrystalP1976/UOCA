﻿using System;
using System.Collections.Generic;
using System.Text;
using UOCApp.Helpers;

namespace UOCApp.Models
{
    public class AdminResult
    {

        public AdminResult(RawResult input)
        {
            //TODO: display formatting
            result_id = Convert.ToInt32(input.result_id);
            student_name = input.student_name;
            date = input.date;
            time = GetResultsHelper.FormatTime(Convert.ToDecimal(input.time));
            school_name = input.school_name;
            sortableDate = input.date; //this may change
            sortableTime = Convert.ToDouble(input.time);
        }

        public int result_id { get; set; }
        public string student_name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string school_name { get; set; }
        public string sortableDate { get; set; }
        public double sortableTime { get; set; }

        //TODO move the get operation here

    }
}
