using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PielievinGerasApiVoter.lib;
using PielievinGerasApiVoter.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using PielievinGerasApiVoter.Services;

namespace PielievinGerasApiVoter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService _votesService;
        private readonly ICandidatesService _candidatesService;
        public VotesController(IVotesService votesService, ICandidatesService candidatesService)
        {
            _votesService = votesService;
            _candidatesService = candidatesService;
        }

        public static List<Vote> votes = new List<Vote>()
        {
        };

        //// GET api/votes
        //[HttpGet()]
        //[EnableCors("AllowAllOrigins")]
        ////[Route("api/votes")]
        //[ProducesResponseType(typeof(IEnumerable<Result>), 200)]
        //public IEnumerable<Result> Get()
        //{
        //    List<Result> r = new List<Result>();
        //    var result = (votes.GroupBy(v => v.Candidate_id,
        //        (key, group) => new { candidate_id = key, count = group.ToList().Count }));
        //    foreach (var item in result)
        //    {
        //        r.Add(new Result(item.candidate_id, item.count));
        //    }
        //    return r;
        //}

        // GET api/votes
        [HttpGet()]
        [EnableCors("AllowAllOrigins")]
        //[Route("api/votes")]
        [ProducesResponseType(typeof(IEnumerable<Result>), 200)]
        public async Task<IEnumerable<Result>> Get()
        {
            List<Vote> votess = await _votesService.GetVotesList();
            List<Result> r = new List<Result>();
            var result = (votess.GroupBy(v => v.Candidate_id,
                (key, group) => new { candidate_id = key, count = group.ToList().Count }));
            foreach (var item in result)
            {
                r.Add(new Result(item.candidate_id, item.count));
            }
            return  r;

        }

        //// GET api/votes/5
        //[HttpGet("{id}")]
        //public Vote Get(int id)
        //{
        //    return votes.Where(c => c.Id == id).FirstOrDefault();
        //}

        //// POST api/votes
        //[HttpPost]
        //[EnableCors("AllowAllOrigins")]
        ////[ProducesResponseType(typeof(string), 201)]
        ////[ProducesResponseType(404)]

        //public void Post([FromBody] String json)
        //{
        //    // Vote vote = Newtonsoft.Json.JsonConvert.DeserializeObject<Vote>(json);
        //    //Vote vote = Newtonsoft.Json.JsonConvert.DeserializeObject<Vote>(json);
        //    //Vote value  = JsonConvert.DeserializeObject<Vote>(jsonString);
        //    //string json = @"{""user"":{""name"":""asdf"",""teamname"":""b"",""email"":""c"",""players"":[""1"",""2""]}}";
        //    Vote vote = new Vote(json);
        //    Vote val;
        //    if (VotesController.votes.Count > 0)
        //    {
        //        val = new Vote
        //        {
        //            Id = votes.Max(c => c.Id) + 1,
        //            Candidate_id = vote.
        //            Candidate_id,
        //            User_id = vote.User_id
        //        };
        //    }
        //    else
        //    {
        //        val = new Vote
        //        {
        //            Id = 1,
        //            Candidate_id = vote.
        //            Candidate_id,
        //            User_id = vote.User_id
        //        };
        //    }
        //    votes.Add(val);
        //    //return GetResult();
        //}

        // POST api/votes
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        public async Task<string> Post([FromBody]Vote value)
        {
            Vote val = new Vote();
            int last = await _votesService.GetLastKey();
            if (last > 0)
            {
                val = new Vote
                {
                    Id = last + 1,
                    Candidate_id = value.
                    Candidate_id,
                    User_id = value.User_id
                };
            }
            else
            {
                val = new Vote
                {
                    Id = 1,
                    Candidate_id = value.
                    Candidate_id,
                    User_id = value.User_id
                };
            }

            await _votesService.AddAsync(val);
            
            return await GetResultAsync();


        }






        //// PUT api/votes/5
        //[HttpPut("{id}")]
        //public IEnumerable<Vote> Put(int id, [FromBody]Vote value)
        //{
        //    Vote val = votes.Where(c => c.Id == id).FirstOrDefault();
        //    if (val != null)
        //    {
        //        val.Candidate_id = value.Candidate_id;
        //        val.User_id = value.User_id;
        //    }
        //    return votes;
        //}

        // DELETE api/votes/5
        [HttpDelete("{id}")]
        public IEnumerable<Vote> Delete(int id)
        {
            Vote val = votes.Where(c => c.Id == id).FirstOrDefault();
            votes.Remove(val);
            return votes;
        }
        [NonAction]
        public async Task<string> GetResultAsync()
        {
            BarChart chart = new BarChart();
            List<Vote> votess = await _votesService.GetVotesList();
            List<Candidate> candidatess = await _candidatesService.GetCandidatesList();
            var result = votess.GroupBy(v => v.Candidate_id, (key, group) => new { candidate_id = key, count = group.ToList().Count });

            chart.addColumn("string", "Candidate");
            chart.addColumn("number", "Ratings");

            foreach (var item in result)
            {
                var candidate = candidatess.
                    Where(c => c.Id == item.candidate_id).FirstOrDefault();
                if (candidate != null)
                {
                    chart.addRowJson(candidate.Name, item.count.ToString());
                }
            }
            return chart.JSon();
        }
    }
}
