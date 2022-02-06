using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface IBranchTypeData
    {
        Task<BranchTypeDBModel> GetBranchTypeByIdAsync(string id);
        Task<List<BranchTypeDBModel>> GetBranchTypesAsync();
        Task InsertBranchTypeAsync(BranchTypeDBModel model);
    }
}