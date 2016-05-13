using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace UOCApp.Models
{
	[Table("result")]
	public class LocalResult
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int result_id { get; set; }
		public DateTime date { get; set; }
		public Decimal time { get; set; }
		public Boolean ranked { get; set; }
		public string student_name { get; set; }
		public string student_gender { get; set; }
		public int student_grade { get; set; }
		public string school_name { get; set; }

		public override string ToString()
		{
			return result_id + " " + date + " " + time + " " + ranked + " " + student_name + " " + student_gender + " " + student_grade + " " + school_name;

			//return base.ToString();
		}

	}
}