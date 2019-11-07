using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoterClientPielievinGeras.Models
{
    public class CollectionResults
    {
        private List<Result> resuts;

        public List<Result> Resuts { get => resuts; set => resuts = value; }
    }
   
}