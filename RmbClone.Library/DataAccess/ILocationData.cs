using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface ILocationData
    {
        Task AddLocationAsync(LocationDBModel model);
        Task DeleteLocationAsync(string id);
        Task DeleteLocationByBranchIdAsync(string branchId);
        Task<LocationDBModel> FindAsync(string id);
        Task<LocationDBModel> FindByBranchIdAsync(string branchId);
        Task<List<LocationDBModel>> GetAllLocationsAsync();
        Task UpdateLocationAsync(LocationDBModel model);

    }
}