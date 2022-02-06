using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface IBranchServiceTypeData
    {
        Task<List<BranchServiceTypeDBModel>> GetAllBranchServiceTypesAsync();
        Task InsertBranchServiceTypeAsync(BranchServiceTypeDBModel model);
    }
}