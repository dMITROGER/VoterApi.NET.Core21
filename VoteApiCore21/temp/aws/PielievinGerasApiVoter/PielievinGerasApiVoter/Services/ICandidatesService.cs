using PielievinGerasApiVoter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PielievinGerasApiVoter.Services
{
    public interface ICandidatesService
    {
        Task AddAsync(Candidate candidate);
        Task <Candidate> RemoveLastAsync();
        Task RemoveAllAsync();
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate> FindAsync(int id);
        Task UpdateAsync(Candidate candidate);
        Task <int> GetLastKey();
        Task<List<Candidate>> GetCandidatesList();
    }
}
