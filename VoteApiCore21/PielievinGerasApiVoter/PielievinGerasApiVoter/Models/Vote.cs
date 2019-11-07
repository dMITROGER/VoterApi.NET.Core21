using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PielievinGerasApiVoter.Models
{
    public class Vote
    {
        public Vote()
        {
        }

        public Vote(int id, int candidate_id, int user_id)
        {
            Id = id;
            Candidate_id = candidate_id;
            User_id = user_id;
        }

        public int Id { get; set; }
        public int Candidate_id { get; set; }
        public int User_id { get; set; }
    }
}
