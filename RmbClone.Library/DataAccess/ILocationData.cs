using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface ILocationData
    {
        Task AddLocationAsync(LocationDBModel model);
        Task DeleteLocationAsync(string id);
        Task<LocationDBModel> FindAsync(string id);
        Task<List<LocationDBModel>> GetAllLocationsAsync();
        Task UpdateLocationAsync(LocationDBModel model);
    }
}