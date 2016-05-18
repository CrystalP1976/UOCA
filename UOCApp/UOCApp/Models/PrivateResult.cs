using System;
using System.Collections.Generic;
using System.Text;
using UOCApp.Helpers;

namespace UOCApp.Models
{
    public class PrivateResult
    {
        public PrivateResult(Result input, List<string> missedObstacles)
        {
            result_id = Convert.ToInt32(input.result_id);
            student_name = input.student_name;
            date = input.date;
            time = GetResultsHelper.FormatTime(input.time);
            sortableDate = input.date;
            sortableTime = Convert.ToDouble(input.time);
            if (missedObstacles == null || missedObstacles.Count == 0)
            {
                missedObstacle = false;
            }
            else
            {
                missedObstacle = true;
                this.missedObstacles = missedObstacles;
            }
        }

        public int result_id { get; set; }
        public string student_name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string sortableDate { get; set; }
        public double sortableTime { get; set; }
        //TODO obstacle handling
        public bool missedObstacle { get; set; }
        public List<string> missedObstacles { get; set; }
    }
}
