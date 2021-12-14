using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface IFaqData
    {
        Task AddFaq(FaqDBModel model);
        Task DeleteFaq(string id);
        Task<FaqDBModel> FindAsync(string id);
        Task<List<FaqDBModel>> getAllFaq();
        Task UpdateFaq(FaqDBModel model);
    }
}