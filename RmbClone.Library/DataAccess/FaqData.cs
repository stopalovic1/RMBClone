using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
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

        public async Task<List<FaqDBModel>> getAllFaq()
        {
            var result = await _sql.LoadData<FaqDBModel, dynamic>("dbo.spFaq_Lookup", new { }, "RmBCloneDb");

            return result;
        }


        public async Task AddFaq(FaqRequestModel model)
        {
            var faqModel = new FaqDBModel();
            faqModel.Id = Guid.NewGuid().ToString();
            faqModel.Answer = model.Answer;
            faqModel.Question = model.Question;
            await _sql.SaveData("dbo.spFaq_Insert", faqModel, "RmbCloneDb");
        }
    }
}
