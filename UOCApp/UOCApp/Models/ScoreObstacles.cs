using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace UOCApp.Models
{

	[Table("score_obstacles")]
	class ScoreObstacles
	{
		[PrimaryKey,AutoIncrement]
		public int id { get; set; }
		public int obstacle_id { get; set; }
		public int result_id{ get; set; }

	}
}

