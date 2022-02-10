using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class ATMFilterData : IATMFilterData
    {
        private readonly ISqlDataAccess _sql;

        public ATMFilterData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task<List<ATMFilterDBModel>> GetAllAtmFiltersAsync()
        {
            var result = await _sql.LoadDataAsync<ATMFilterDBModel, dynamic>("dbo.spAtmFilter_Lookup", new { }, "RmbCloneDb");
            return result;
        }

        public async Task InsertAtmFilterAsync(ATMFilterDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spAtmFilter_Insert", model, "RmbCloneDb");
        }

        public async Task<ATMFilterDBModel> GetAtmFilterByIdAsync(string id)
        {
            var result = await _sql.LoadDataAsync<ATMFilterDBModel, dynamic>("dbo.spAtmFilter_LookupById", new { Id = id }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

    }
}
