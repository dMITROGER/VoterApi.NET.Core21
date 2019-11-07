using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PielievinGerasApiVoter.Models;
using PielievinGerasApiVoter.Services;

namespace PielievinGerasApiVoter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class CandidatesController : Controller
    {
        private readonly IVotesService _votesService;
        private readonly ICandidatesService _candidatesService;
        public CandidatesController(IVotesService votesService, ICandidatesService candidatesService)
        {
            _votesService = votesService;
            _candidatesService = candidatesService;
        }

        // GET api/candidates
        [HttpGet()]
        [EnableCors("AllowAllOrigins")]
        [ProducesResponseType(typeof(IEnumerable<Candidate>), 200)]
        public Task<IEnumerable<Candidate>> Get()
        {
            return _candidatesService.GetAllAsync();

        }

        // GET api/candidates/getall
        [HttpGet()]
        [Route("getall")]
        [EnableCors("AllowAllOrigins")]
        [ProducesResponseType(typeof(CollectionCandidates), 200)]
        public async Task<CollectionCandidates> GetAsync()
        {
            CollectionCandidates collectionCandidates = new CollectionCandidates();
            List<Candidate> candidatess = await _candidatesService.GetCandidatesList();
            collectionCandidates.Candidates = candidatess;
            return collectionCandidates;

        }

        // POST api/candidates 
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public async Task<IEnumerable<Candidate>> Post([FromBody] Candidate value)
        {
            if (value.Name != null && value.Name.ToString() != "")
            {
                Candidate val;
                int last = await _candidatesService.GetLastKey();
                if (last > 0)
                {
                    val = new Candidate { Id = last + 1, Name = value.Name };
                }
                else
                {
                    val = new Candidate { Id = 1, Name = value.Name };
                }
                await _candidatesService.AddAsync(val);
            }
            return await _candidatesService.GetAllAsync();
        }

        // PUT api/candidates 
        [HttpPut("{newName}")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> Put([FromBody] Candidate value, string newName)
        {
            if (value.Name != null && value.Name.ToString() != "")
            {
                List<Candidate> candidatess = await _candidatesService.GetCandidatesList();

                Candidate val = candidatess.Find(c => c.Name == value.Name);

                val.Name = newName;

                await _candidatesService.UpdateAsync(val);

                return new ObjectResult(_candidatesService.GetAllAsync());
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/candidates/GetDeleteLast
        [HttpDelete]
        [Route("GetDeleteLast")]
        [EnableCors("AllowAllOrigins")]
        public async Task GetDeleteLast()
        {
            List<Candidate> candidatess = await _candidatesService.GetCandidatesList();
            List<Vote> votess = await _votesService.GetVotesList();
            if (candidatess.Count > 0)
            {
                Candidate cand = await _candidatesService.RemoveLastAsync();
                foreach (Vote v in votess)
                {
                    if (v.Candidate_id == cand.Id)
                    {
                        await _votesService.DeleteVote(v.Id);
                    }
                    
                }
            }
        }

        // DELETE api/candidates/GetDeleteAll
        [HttpDelete]
        [Route("GetDeleteAll")]
        [EnableCors("AllowAllOrigins")]
        public async Task GetDeleteAll()
        {
            await _candidatesService.RemoveAllAsync();
            await _votesService.RemoveAllAsync();
        }
    }
}