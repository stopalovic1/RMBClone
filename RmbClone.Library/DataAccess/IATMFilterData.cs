using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface IATMFilterData
    {
        Task<List<ATMFilterDBModel>> GetAllAtmFiltersAsync();
        Task<ATMFilterDBModel> GetAtmFilterByIdAsync(string id);
        Task InsertAtmFilterAsync(ATMFilterDBModel model);
    }
}