using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class BranchTypeData : IBranchTypeData
    {
        private readonly ISqlDataAccess _sql;

        public BranchTypeData(ISqlDataAccess sql)
        {
            _sql = sql;
        }
        public async Task<List<BranchTypeDBModel>> GetBranchTypesAsync()
        {
            var result = await _sql.LoadDataAsync<BranchTypeDBModel, dynamic>("dbo.spBranchType_Lookup", new { }, "RmbCloneDb");
            return result;
        }
        public async Task InsertBranchTypeAsync(BranchTypeDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spBranchType_Insert", model, "RmbCloneDb");
        }
        public async Task<BranchTypeDBModel> GetBranchTypeByIdAsync(string id)
        {
            var result = await _sql.LoadDataAsync<BranchTypeDBModel, dynamic>("dbo.spBranchType_LookupById", new { Id = id }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

    }
}
