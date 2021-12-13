using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface IUserData
    {
        Task AddUserAsync(UserDBModel model);
        Task<UserDBModel> FindByEmailAsync(string email);
        Task<List<UserDBModel>> GetAllUsersAsync();
    }
}