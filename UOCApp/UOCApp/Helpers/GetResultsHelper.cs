using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UOCApp.Models;
using Newtonsoft.Json;

namespace UOCApp.Helpers
{
    public class GetResultsException : Exception
    {
        public GetResultsException(string message)
        {

        }

    }

    public class GetResultsHelper
    {
        private HttpClient client;
        private string url;

        public GetResultsHelper(HttpClient client, string url)
        {
            this.client = client;
            this.url = url;

        }

        public async Task<int> GetCount(string query)
        {
            string url = this.url + "count" + query;

            //Console.WriteLine(url);

            var uri = new Uri(url);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                return Convert.ToInt32(content);
            }
            else
            {
                //explicitly throw an exception if the status cocde is other than successful
                throw new GetResultsException(response.StatusCode.ToString());
            }
        }

        public async Task<List<RawResult>> GetRawResults(string query)
        {
            string url = this.url + "results" + query;

            //Console.WriteLine(url);

            var uri = new Uri(url);

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(content);
                List<RawResult> rawresults = JsonConvert.DeserializeObject<List<RawResult>>(content);

                //return results
                return rawresults;
            }
            else
            {
                //explicitly throw an exception if the status cocde is other than successful
                throw new GetResultsException(response.StatusCode.ToString());
            }
        }

        public static List<AdminResult> ConvertAdminResults(List<RawResult> rawresults)
        {
            List<AdminResult> results = new List<AdminResult>();

            foreach (RawResult result in rawresults)
            {
                //Console.WriteLine(result.ToString());
                results.Add(new AdminResult(result));
            }

            return results;
        }

        public static List<LeaderboardResult> ConvertLeaderboardResults(List<RawResult> rawresults)
        {
            List<LeaderboardResult> results = new List<LeaderboardResult>();

            foreach (RawResult result in rawresults)
            {
                //Console.WriteLine(result.ToString());
                results.Add(new LeaderboardResult(result));
            }

            return results;
        }

        public string CreateQueryString(string selectedPeriod, string selectedGrade, string selectedGender, string school, bool official)
        {
            string output = String.Empty;

            //check if a period is specified and deal with it
            if(!String.IsNullOrEmpty(selectedPeriod))
            {
                //TODO switch on string
                output += "/";

                switch(selectedPeriod)
                {
                    case "Daily":
                        output += "daily";
                        break;
                    case "Weekly":
                        output += "weekly";
                        break;
                    case "Monthly":
                        output += "monthly";
                        break;
                    default:
                        output += "alltime";
                        break;
                }
            }

            output += "?";

            //map grade strings to grade
            int grade = 0;
            switch (selectedGrade)
            {
                case "Grade 4":
                    grade = 4;
                    break;
                case "Grade 5":
                    grade = 5;
                    break;
                case "Grade 6":
                    grade = 6;
                    break;
                case "Grade 7":
                    grade = 7;
                    break;
                case "Teenager":
                    grade = -1;
                    break;
                case "Adult Under 35":
                    grade = -2;
                    break;
                case "Adult Over 35":
                    grade = -3;
                    break;
            }
            output += "student_grade=" + grade;

            //pop the first letter (Male or Female) off and use that
            string gender = Convert.ToString(selectedGender[0]).ToUpper();
            output += "&student_gender=" + gender;

            //get school and append if it's not null
            if (!String.IsNullOrEmpty(school))
            {
                output += "&school_name=" + school;
            }

            //filtering for official times if relevant
            if(official)
            {
                output += "&ranked=true";
            }

            return output;
        }

        public string CreateQueryString(string selectedGrade, string selectedGender, string school)
        {
            return CreateQueryString(null, selectedGrade, selectedGender, school, false);
        }

        public static void SortResults(List<AdminResult> baseResults, string selectedItem)
        {
            switch (selectedItem)
            {
                case "Name":
                    baseResults.Sort((o1, o2) => o1.student_name.CompareTo(o2.student_name));
                    break;
                case "Date":
                    baseResults.Sort((o1, o2) => o2.sortableDate.CompareTo(o1.sortableDate)); //we want most recent
                    break;
                default:
                    //default is to sort by time
                    baseResults.Sort((o1, o2) => o1.sortableTime.CompareTo(o2.sortableTime));
                    break;
            }
        }

        //TODO format time
        public static string FormatTime(decimal time)
        {
            throw new NotImplementedException();
        }

    }
}
