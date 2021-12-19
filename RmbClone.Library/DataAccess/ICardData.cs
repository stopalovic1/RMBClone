using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface ICardData
    {
        Task AddCardAsync(CardDBModel model);
        Task DeleteCardAsync(string id);
        Task<CardDBModel> FindAsync(string id);
        Task<List<CardDBModel>> GetAllCardsAsync();
        Task UpdateCardAsync(CardDBModel model);
    }
}