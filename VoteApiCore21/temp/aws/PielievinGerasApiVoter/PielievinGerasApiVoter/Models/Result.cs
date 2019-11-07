using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PielievinGerasApiVoter.Models
{
    public class Result
    {
        public Result()
        {
        }

        public Result(int _candidate_id, int _count)
        {
            this.candidate_id = _candidate_id;
            this.count = _count;
        }

        public int candidate_id { get; set; }
        public int count { get; set; }
    }
}
