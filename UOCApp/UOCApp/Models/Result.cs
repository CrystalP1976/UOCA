using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace UOCApp.Models
{
    [Table("result")]
    public class Result
    {
        [PrimaryKey,AutoIncrement]
        public int? result_id { get; set; } 
        public string date { get; set; }
        public decimal time { get; set; }
        public int shared { get; set; }
        public string student_name { get; set; }
        public string student_gender { get; set; }
        public int student_grade { get; set; }



        public override string ToString()
        {
            return (base.ToString() + " [" + result_id + "," + date + "," + time + "," + shared + "," + student_name + "," + student_gender + "," + student_grade + "]");
        }
    }
}
