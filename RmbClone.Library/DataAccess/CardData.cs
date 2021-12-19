using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class CardData : ICardData
    {
        private readonly ISqlDataAccess _sql;

        public CardData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task<List<CardDBModel>> GetAllCardsAsync()
        {
            var result = await _sql.LoadDataAsync<CardDBModel, dynamic>("dbo.spCard_Lookup", new { }, "RmbCloneDb");
            return result;
        }

        public async Task AddCardAsync(CardDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spCard_Insert", model, "RmbCloneDb");
        }

        public async Task DeleteCardAsync(string id)
        {
            await _sql.SaveDataAsync("dbo.spCard_Delete", new { Id = id }, "RmbCloneDb");
        }

        public async Task<CardDBModel> FindAsync(string id)
        {
            var result = await _sql.LoadDataAsync<CardDBModel, dynamic>("dbo.spCard_LookupById", new { Id = id }, "RmbCloneDb");
            return result.FirstOrDefault();
        }
        public async Task UpdateCardAsync(CardDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spCard_Update", model, "RmbCloneDb");
        }
    }
}
