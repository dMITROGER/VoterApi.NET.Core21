using PielievinGerasApiVoter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PielievinGerasApiVoter.Services
{
    public interface IVotesService
    {
        Task AddAsync(Vote vote);
        
        Task<IEnumerable<Vote>> GetAllAsync();
        Task<int> GetLastKey();
        Task<List<Vote>> GetVotesList();
        Task DeleteVote(int k);
        Task RemoveAllAsync();


    }
}
