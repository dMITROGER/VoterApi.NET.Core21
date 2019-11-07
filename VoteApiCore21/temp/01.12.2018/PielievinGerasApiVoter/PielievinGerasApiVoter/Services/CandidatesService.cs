using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PielievinGerasApiVoter.Models;

namespace PielievinGerasApiVoter.Services
{
    public class CandidatesService : ICandidatesService
    {
        private readonly ConcurrentDictionary<int, Candidate> _candidates = new ConcurrentDictionary<int, Candidate>();
        public Task AddAsync(Candidate candidate)
        {
           
            int Idd = candidate.Id;
            _candidates[Idd] = candidate;
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Candidate>> GetAllAsync() => Task.FromResult<IEnumerable<Candidate>>(_candidates.Values);
        

        public Task RemoveAllAsync()
        {
            _candidates.Clear();
            return Task.CompletedTask;
        }

        public Task<Candidate> RemoveLastAsync()
        {
            var key = _candidates.Keys.Last();
             _candidates.TryRemove(key, out Candidate removed);
             return Task.FromResult(removed);
            //return Task.CompletedTask;

        }

        public Task UpdateAsync(Candidate candidate)
        {
            throw new NotImplementedException();
        }
        
        Task<int> ICandidatesService.GetLastKey()
        {
            int last;
            if (_candidates.Count() > 0)
            {
                last = _candidates.Keys.Last();
            }
            else
            {
                last = 0;
            }
            return Task.FromResult(last);
        }


        Task<List<Candidate>> ICandidatesService.GetCandidatesList()
        {
            List<Candidate> fields = new List<Candidate>();
            if (_candidates.Count() > 0)
            {
                fields = _candidates.Values.ToList();
            }

            return Task.FromResult(fields);
        }
    }
}
