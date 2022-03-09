using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class TransactionData : ITransactionData
    {
        private readonly ISqlDataAccess _sql;

        public TransactionData(ISqlDataAccess sql)
        {
            _sql = sql;
        }


        public async Task<List<TransactionDBModel>> GetAllSentTransactionsAsync(string transactionNumber)
        {
            var result = await _sql.LoadDataAsync<TransactionDBModel, dynamic>("dbo.spTransaction_LookupBySender", new { FromAccount = transactionNumber }, "RmbCloneDb");
            return result;
        }
        public async Task<List<TransactionDBModel>> GetAllReceivedTransactionsAsync(string transactionNumber)
        {
            var result = await _sql.LoadDataAsync<TransactionDBModel, dynamic>("dbo.spTransaction_LookupByReceiver", new { ToAccount = transactionNumber }, "RmbCloneDb");
            return result;
        }

        public async Task InsertTransactionAsync(TransactionDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spTransaction_Insert", model, "RmbCloneDb");
        }


    }
}
