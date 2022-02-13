using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface ICityData
    {
        Task AddCityAsync(CityDBModel model);
        Task DeleteCityAsync(string id);
        Task<CityDBModel> FindCityByIdAsync(string id);
        Task<List<CityDBModel>> GetAllCitiesAsync();
        Task UpdateCityAsync(CityDBModel model);
    }
}