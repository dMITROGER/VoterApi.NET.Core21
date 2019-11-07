using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PielievinGerasApiVoter.Models
{
    public class CollectionResults
    {
        private List<Result> resuts;

        public List<Result> Resuts { get => resuts; set => resuts = value; }
    }
}
