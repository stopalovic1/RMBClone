using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class BranchServiceTypeData : IBranchServiceTypeData
    {
        private readonly ISqlDataAccess _sql;

        public BranchServiceTypeData(ISqlDataAccess sql)
        {
            _sql = sql;
        }


        public async Task<List<BranchServiceTypeDBModel>> GetAllBranchServiceTypesAsync()
        {
            var result = await _sql.LoadDataAsync<BranchServiceTypeDBModel, dynamic>("dbo.spBranchServiceType_Lookup", new { }, "RmbCloneDb");
            return result;
        }

        public async Task InsertBranchServiceTypeAsync(BranchServiceTypeDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spBranchServiceType_Insert", model, "RmbCloneDb");
        }

        public async Task<BranchServiceTypeDBModel> GetBranchServiceTypeByIdAsync(string id)
        {
            var result = await _sql.LoadDataAsync<BranchServiceTypeDBModel, dynamic>("dbo.spBranchServiceType_LookupById", new { Id = id }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

    }
}
