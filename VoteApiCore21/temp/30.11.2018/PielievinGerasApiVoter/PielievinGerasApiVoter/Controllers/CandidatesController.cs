using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PielievinGerasApiVoter.Models;


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
        
        // GET api/candidates
        [HttpGet()]
        [EnableCors("AllowAllOrigins")]
        [ProducesResponseType(typeof(IEnumerable<Candidate>), 200)]
        public IEnumerable<Candidate> Get()
        {
            return candidates;
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
        public IEnumerable<Candidate> Post([FromBody] Candidate value)
        {
            if (value.Name != null && value.Name.ToString() != "")
            {
                Candidate val = new Candidate();
                if (CandidatesController.candidates.Count > 0)
                {
                    val = new Candidate { Id = candidates.Max(c => c.Id) + 1, Name = value.Name };
                }
                else
                {
                    val = new Candidate { Id = 1, Name = value.Name };
                }
                candidates.Add(val);
            }
            return candidates;
        }

        // DELETE api/candidates/GetDeleteLast
        [HttpDelete]
        [Route("GetDeleteLast")]
        [EnableCors("AllowAllOrigins")]
        public void GetDeleteLast()
        {
            if (CandidatesController.candidates.Count > 0)
            {
                Candidate val = CandidatesController.candidates.Last();
                if (val != null)
                {
                    CandidatesController.candidates.Remove(val);
                    VotesController.votes.RemoveAll(c => c.Candidate_id == val.Id);
                }
            }
        }

        // DELETE api/candidates/GetDeleteAll
        [HttpDelete]
        [Route("GetDeleteAll")]
        [EnableCors("AllowAllOrigins")]
        public void GetDeleteAll()
        {
            if (CandidatesController.candidates.Count > 0)
            {
                CandidatesController.candidates.RemoveAll(c => c.Id > -1);
                VotesController.votes.RemoveAll(c => c.Id > -1);
            }
        }
    }
}