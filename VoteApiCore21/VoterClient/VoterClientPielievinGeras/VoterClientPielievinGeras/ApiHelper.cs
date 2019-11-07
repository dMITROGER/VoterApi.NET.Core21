using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using VoterClientPielievinGeras.Models;

namespace VoterClientPielievinGeras
{
    
    public class ApiHelper
    {
        public Task<IEnumerable<Result>> getResult()
        {
            using (var webClient = new System.Net.WebClient())
            {
               string rawJSON =  webClient.DownloadString("https://localhost:44388/api/votes");
                //string rawJSON = webClient.DownloadString("http://dmitrogeras-eval-test.apigee.net/votesystem/api/votes");
                CollectionResults collectionResults = JsonConvert.DeserializeObject<CollectionResults>(rawJSON);
                IEnumerable<Result> res = collectionResults.Resuts;
                return Task.FromResult(res);
            }
        }

        public Task <IEnumerable<Candidate>> getCandidates()
        {
            using (var webClient = new System.Net.WebClient())
            {
                string rawJSON = webClient.DownloadString("https://localhost:44388/api/candidates/getall");
                //string rawJSON = webClient.DownloadString("http://dmitrogeras-eval-test.apigee.net/votesystem/api/candidates/getall");
                CollectionCandidates collectionCandidates = JsonConvert.DeserializeObject<CollectionCandidates>(rawJSON);
                IEnumerable<Candidate> res = collectionCandidates.Candidates;
                return Task.FromResult(res);
            }
        }
    }
}