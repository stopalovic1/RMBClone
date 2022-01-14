using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface IRoleData
    {
        Task AddRoleAsync(RoleDBModel model);
        Task<List<string>> GetRolesForUserAsync(string userId);
    }
}