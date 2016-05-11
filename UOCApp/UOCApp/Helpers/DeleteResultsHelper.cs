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
            Console.WriteLine("delete result " + result_id);

            return false;
        }
    }
}
