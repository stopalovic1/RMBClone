using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface IUserData
    {
        Task AddUserAsync(UserDBModel model);
        Task<bool> CheckPasswordAsync(UserDBModel user, string password);
        Task<UserDBModel> FindByEmailAsync(string email);
        Task<UserDBModel> FindByIdAsync(string id);
        Task<List<UserDBModel>> GetAllUsersAsync();
    }
}