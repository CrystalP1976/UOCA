using System;
using System.Collections.Generic;
using UOCApp.Models;

namespace UOCApp
{
	public class ObstacleList
	{

		public Boolean Switch_0 { get; set; }
		public Boolean Switch_1  { get; set; }
		public Boolean Switch_2  { get; set; }
		public Boolean Switch_3  { get; set; }
		public Boolean Switch_4  { get; set; }
		public Boolean Switch_5  { get; set; }
		public Boolean Switch_6  { get; set; }
		public Boolean Switch_7  { get; set; }
		public Boolean Switch_8  { get; set; }
		public Boolean Switch_9  { get; set; }
		public Boolean Switch_10  { get; set; }
		public Boolean Switch_11 { get; set; }




		public ObstacleList ()
		{
			Switch_0 = true;
			Switch_1 = true;
			Switch_2 = true;
			Switch_3 = true;
			Switch_4 = true;
			Switch_5 = true;
			Switch_6 = true;
			Switch_7 = true;
			Switch_8 = true;
			Switch_9 = true;
			Switch_10 = true;
			Switch_11 = true;
		}


		public Boolean allComplete()
		{
			return Switch_0 & Switch_1 & Switch_2 & Switch_3 & Switch_4 & Switch_5 & Switch_6 & Switch_7 & Switch_8 & Switch_9
			& Switch_10 & Switch_11;


		}

		public override String ToString()
		{
			return "0:" + Switch_0 + " 1:" + Switch_1 + " 2:" + Switch_2 + " 3:" + Switch_3
			+ " 4:" + Switch_4 + " 5:" + Switch_5 + " 6:" + Switch_6 + " 7:" + Switch_7
			+ " 8:" + Switch_8 + " 9:" + Switch_9 + " 10:" + Switch_10 + " 11:" + Switch_11;

		}




	}
}

