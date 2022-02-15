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
        Task<List<BranchResponseModel>> GetAllBranchesAsync(List<BranchDBModel> brs=null);
        Task<BranchDBModel> GetBranchByIdAsync(string id);
        Task<List<BranchResponseModel>> GetFilteredBranchesAsync(string city = null, string branchType = null, string branchServiceType = null);
        Task InsertBranchAsync(BranchRequestModel model);
        Task UpdateBranchAsync(string id, BranchRequestModel model);
    }
}