using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UOCApp.Helpers
{
    class DeleteResultsException : Exception
    {
        public DeleteResultsException(string message)
        {

        }

    }

    class DeleteResultsHelper
    {
        private HttpClient client;
        private string url;

        public DeleteResultsHelper(HttpClient client, string url)
        {
            this.client = client;
            this.url = url;

        }

        public async Task<bool> DeleteResult(int result_id, string password)
        {
            string url = this.url + "result?result_id=" + result_id + "&password=" + password;
            var uri = new Uri(url);

            //Console.WriteLine("delete result " + result_id);
            //Console.WriteLine("delete result " + url);

            var response = await client.DeleteAsync(uri);
            //Console.WriteLine("Response code: " + response.StatusCode.ToString());
            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                //explicitly throw an exception if the status cocde is other than successful
                throw new DeleteResultsException(response.StatusCode.ToString());
            }
        }
    }
}
