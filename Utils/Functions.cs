using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunterService
{
    public static class Functions
    {
        public static Dictionary<string, string> cacheDic = new Dictionary<string, string>();

        public  static string GetGifsListFromGiphyCom( string searchText)
        {
            string respData = string.Empty;
            RestResponse response = null;
            try
            {

                var options = new RestClientOptions("https://api.giphy.com")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v1/gifs/search?api_key=2bwcVkYS3ndztWiWLJmNVRCA7u9DwDPq&q=funny+cat&limit=5", Method.Get);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                response = client.Execute(request);
                
                if(response != null)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        respData = response.Content;
                    }
                }

                
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return respData;
        }

        //public static bool IsSqlInjection(string userInput)
        //{
        //    // Define regular expressions for common SQL injection patterns
        //    string[] patterns =
        //    {
        //        "';",
        //        "\";",
        //        "--",
        //        ";--",
        //        ";",
        //        "/*",
        //        "*/",
        //        "@@"
        //        // Add more patterns as needed
        //    };

        //    // Check if any of the patterns match the input string
        //    foreach (string pattern in patterns)
        //    {
        //        if (Regex.IsMatch(userInput, pattern, RegexOptions.IgnoreCase))
        //        {
        //            return true; // SQL injection pattern found
        //        }
        //    }

        //    return false; // No SQL injection pattern found
        //}
    }
}
