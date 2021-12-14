using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class LocationData : ILocationData
    {
        private readonly ISqlDataAccess _sql;

        public LocationData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task<List<LocationDBModel>> GetAllLocationsAsync()
        {
            var result = await _sql.LoadDataAsync<LocationDBModel, dynamic>("dbo.spLocation_Lookup", new { }, "RmbCloneDb");
            return result;
        }

        public async Task AddLocationAsync(LocationDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spLocation_Insert", model, "RmbCloneDb");
        }


    }
}
