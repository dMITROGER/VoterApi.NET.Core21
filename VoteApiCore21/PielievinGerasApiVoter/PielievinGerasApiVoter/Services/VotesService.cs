using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PielievinGerasApiVoter.Models;

namespace PielievinGerasApiVoter.Services
{
    public class VotesService : IVotesService
    {
        private readonly ConcurrentDictionary<int, Vote> _votes = new ConcurrentDictionary<int, Vote>();

        public Task AddAsync(Vote vote)
        {
            int Idd = vote.Id;
            _votes[Idd] = vote;
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Vote>> GetAllAsync() => Task.FromResult<IEnumerable<Vote>>(_votes.Values);

        Task<int> IVotesService.GetLastKey()
        {
            int last;
            if (_votes.Count() > 0)
            {
                last = _votes.Keys.Last();
            }
            else
            {
                last = 0;
            }
            return Task.FromResult(last);
        }

        Task<List<Vote>> IVotesService.GetVotesList()
        {
            List<Vote> fields = new List<Vote>();
            if (_votes.Count() > 0)
            {
                fields = _votes.Values.ToList();
            }
           
            return Task.FromResult(fields);
        }

        Task IVotesService.DeleteVote(int k)
        {
            _votes.TryRemove(k, out Vote removed);

            return Task.CompletedTask;
        }

        public Task RemoveAllAsync()
        {
            _votes.Clear();
            return Task.CompletedTask;
        }
    }
}
