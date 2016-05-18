using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace UOCApp.Models
{

	[Table("obstacles")]
	class Obstacle
	{
		public int obstacle_id { get; set; }
		public string obstacle_name { get; set; }

        public override string ToString()
        {
            return ("Obstacle: " + obstacle_id + " - " + obstacle_name);
        }



	}

   

}

