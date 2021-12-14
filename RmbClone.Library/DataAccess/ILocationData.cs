using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface ILocationData
    {
        Task AddLocationAsync(LocationDBModel model);
        Task<List<LocationDBModel>> GetAllLocationsAsync();
    }
}