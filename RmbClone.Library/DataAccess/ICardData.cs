using RmbClone.Library.Models;
using RmbClone.Library.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface ICardData
    {
        Task AddCardAsync(CardDBModel model);
        Task DeleteCardAsync(string id);
        Task<CardDBModel> FindAsync(string id);
        Task<CardDBModel> FindByTransactionNumberAsync(string transactionNumber);
        Task<List<CardResponseModel>> GetAllCardsForUserAsync(string userId);
        Task UpdateCardAsync(CardDBModel model);
    }
}