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

        // GET api/votes
        [HttpGet()]
        [EnableCors("AllowAllOrigins")]
        //[Route("api/votes")]
        [ProducesResponseType(typeof(CollectionResults), 200)]
        public async Task<CollectionResults> Get()
        {
            List<Vote> votess = await _votesService.GetVotesList();
            List<Result> r = new List<Result>();
            CollectionResults collectionResults = new CollectionResults();

            var result = (votess.GroupBy(v => v.Candidate_id,
                (key, group) => new { candidate_id = key, count = group.ToList().Count }));
            foreach (var item in result)
            {
                r.Add(new Result(item.candidate_id, item.count));
            }
            collectionResults.Resuts = r;
            return collectionResults;
        }

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
