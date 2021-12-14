using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class CityData
    {
        private readonly ISqlDataAccess _sql;

        public CityData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task<List<CityDBModel>> GetAllCitiesAsync()
        {
            var result = await _sql.LoadDataAsync<CityDBModel, dynamic>("dbo.spCity_Lookup", new { }, "RmbCloneDb");
            return result;
        }

        public async Task AddCityAsync(CityDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spCity_Insert",model, "RmbCloneDb");
        }

        public async Task<CityDBModel> FindAsync(string id)
        {
            var result = await _sql.LoadDataAsync<CityDBModel, dynamic>("dbo.spCity_LookupById", new { Id = id }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

        public async Task DeleteCityAsync(string id)
        {
            await _sql.SaveDataAsync("dbo.spCity_Delete", new { Id = id }, "RmbCloneDb");
        }
    }
}
