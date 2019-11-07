using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using VoterClientPielievinGeras.Models;

namespace VoterClientPielievinGeras
{
    
    public class ApiHelper
    {
        static IEnumerable<Result> results;
        static IEnumerable<Candidate> listCandidates;
        static HttpClient client = new HttpClient();

        public static string resVotes = "";
        public static string resCand = "";

        public static IEnumerable<Result> getResult()
        {
            RunAsync().GetAwaiter();
            return results;
        }
        public static IEnumerable<Candidate> getCandidates()
        {
           // RunAsync().GetAwaiter();
            return listCandidates;
        }

        static async Task RunAsync()
        {
            //client.BaseAddress = new Uri("http://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com");
            client.BaseAddress = new Uri("https://localhost:44388");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
               
                HttpResponseMessage response;
                response =  client.GetAsync("/api/Votes").Result;
                using (HttpContent content1 = response.Content)
                {
                    // ... Read the string.//
                    Task<string> result = content1.ReadAsStringAsync();
                    resVotes = result.Result;
                    results = JsonConvert.DeserializeObject<IEnumerable<Result>>(resVotes);
                }

                HttpResponseMessage response2 = client.GetAsync("/api/candidates").Result;
                using (HttpContent content2 = response2.Content)
                {
                    // ... Read the string.//
                    Task<string> result = content2.ReadAsStringAsync();
                    resCand = result.Result;
                    listCandidates = JsonConvert.DeserializeObject<IEnumerable<Candidate>>(resCand);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

    }
}