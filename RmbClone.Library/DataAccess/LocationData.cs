using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task DeleteLocationAsync(string id)
        {
            await _sql.SaveDataAsync("dbo.spLocation_Delete", new { Id = id }, "RmbCloneDb");
        }

        public async Task<LocationDBModel> FindAsync(string id)
        {
            var result = await _sql.LoadDataAsync<LocationDBModel, dynamic>("dbo.spLocation_LookupById", new { Id = id }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

        public async Task UpdateLocationAsync(LocationDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spLocation_Update", model, "RmbCloneDb");
        }


        public async Task<LocationDBModel> FindByBranchIdAsync(string branchId)
        {
            var result = await _sql.LoadDataAsync<LocationDBModel, dynamic>("dbo.spLocation_LookupByBranchId", new { BranchId = branchId }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

        public async Task DeleteLocationByBranchIdAsync(string branchId)
        {
            await _sql.SaveDataAsync("dbo.spLocation_DeleteByBranchId", new { BranchId = branchId }, "RmbCloneDb");
        }


    }
}
