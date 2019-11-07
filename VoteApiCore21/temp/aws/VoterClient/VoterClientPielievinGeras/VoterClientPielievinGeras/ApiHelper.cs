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
        
        private static string resVotes = "";
        private static string resCand = "";

        public Task<IEnumerable<Result>> getResult()
        {
            HttpClient client = new HttpClient();

            //client.BaseAddress = new Uri("https://localhost:44388");
            client.BaseAddress = new Uri("http://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com");
            //client.BaseAddress = new Uri("http://dmitrogeras-eval-test.apigee.net/votesystem");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;
                response = client.GetAsync("/api/Votes").Result;
                using (HttpContent content1 = response.Content)
                {
                    // ... Read the string.//
                    Task<string> result = content1.ReadAsStringAsync();
                    resVotes = result.Result;
                    results = JsonConvert.DeserializeObject<IEnumerable<Result>>(resVotes);
                }
                client.Dispose();
                return Task.FromResult(results);
        }

        public Task <IEnumerable<Candidate>> getCandidates()
        {
             HttpClient client = new HttpClient();

            //client.BaseAddress = new Uri("https://localhost:44388");
            client.BaseAddress = new Uri("http://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com");
            //client.BaseAddress = new Uri("http://dmitrogeras-eval-test.apigee.net/votesystem");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response2 = client.GetAsync("/api/candidates").Result;
            using (HttpContent content2 = response2.Content)
            {
                // ... Read the string.//
                Task<string> result = content2.ReadAsStringAsync();
                resCand = result.Result;
                listCandidates = JsonConvert.DeserializeObject<IEnumerable<Candidate>>(resCand);
            }
            client.Dispose();
            return Task.FromResult(listCandidates);
        }
    }
}