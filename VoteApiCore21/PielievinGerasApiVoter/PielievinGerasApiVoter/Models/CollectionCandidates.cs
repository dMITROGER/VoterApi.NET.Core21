using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PielievinGerasApiVoter.Models
{
    public class CollectionCandidates
    {
        private List<Candidate> candidates;

        public List<Candidate> Candidates { get => candidates; set => candidates = value; }
    }
}
