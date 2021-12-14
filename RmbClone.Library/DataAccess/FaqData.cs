using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class FaqData : IFaqData
    {
        private readonly ISqlDataAccess _sql;

        public FaqData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task<List<FaqDBModel>> GetAllFaq()
        {
            var result = await _sql.LoadDataAsync<FaqDBModel, dynamic>("dbo.spFaq_Lookup", new { }, "RmBCloneDb");

            return result;
        }

        public async Task AddFaq(FaqDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spFaq_Insert", model, "RmbCloneDb");
        }

        public async Task UpdateFaq(FaqDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spFaq_Update", model, "RmbCloneDb");
        }

        public async Task<FaqDBModel> FindAsync(string id)
        {
            var result = await _sql.LoadDataAsync<FaqDBModel, dynamic>("dbo.spFaq_LookupById", new { Id = id }, "RmBCloneDb");

            return result.FirstOrDefault();
        }

        public async Task DeleteFaq(string id)
        {
            await _sql.SaveDataAsync("dbo.spFaq_Delete", new { Id = id }, "RmbCloneDb");
        }


    }
}
