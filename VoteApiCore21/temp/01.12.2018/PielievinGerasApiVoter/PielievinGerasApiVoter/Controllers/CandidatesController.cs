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
        public  static List<Candidate> candidates = new List<Candidate>()
        {
        };

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
        //public Task<IEnumerable<Candidate>> GetAsync() => _candidatesService.GetAllAsync();
        public Task<IEnumerable<Candidate>> Get()
        {
           return _candidatesService.GetAllAsync();

        }   
           

        // GET api/candidates/5
        [HttpGet("{id}")]
        public Candidate Get(int id)
        {
            return candidates.Where(c => c.Id == id).FirstOrDefault();
           
        }


        // POST api/candidates 
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public async Task <IEnumerable<Candidate>> Post([FromBody] Candidate value)
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
                votess.RemoveAll(c => c.Candidate_id == cand.Id);

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