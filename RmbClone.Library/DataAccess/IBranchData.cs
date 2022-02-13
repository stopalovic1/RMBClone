using RmbClone.Library.Models;
using RmbClone.Library.Models.Requests;
using RmbClone.Library.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface IBranchData
    {
        Task DeleteBranchAsync(string id);
        Task<List<BranchResponseModel>> GetAllBranchesAsync();
        Task<BranchDBModel> GetBranchByIdAsync(string id);
        Task InsertBranchAsync(BranchRequestModel model);
        Task UpdateBranchAsync(string id, BranchRequestModel model);
    }
}