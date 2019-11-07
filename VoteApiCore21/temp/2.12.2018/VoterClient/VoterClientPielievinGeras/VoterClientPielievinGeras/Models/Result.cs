using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoterClientPielievinGeras.Models
{
    public class Result
    {
        //public Result(int candidateId, int count)
        //{
        //    this.candidateId = candidateId;
        //    this.count = count;
        //}

        //public int candidateId { get; set; }

        public int candidate_id { get; set; }
        public int count { get; set; }
    }
}