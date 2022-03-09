using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface ITransactionData
    {
        Task<List<TransactionDBModel>> GetAllReceivedTransactionsAsync(string id);
        Task<List<TransactionDBModel>> GetAllSentTransactionsAsync(string id);
        Task InsertTransactionAsync(TransactionDBModel model);
    }
}