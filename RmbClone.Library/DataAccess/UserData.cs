using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
namespace RmbClone.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task<List<UserDBModel>> GetAllUsersAsync()
        {
            var result = await _sql.LoadDataAsync<UserDBModel, dynamic>("dbo.spUser_Lookup", new { }, "RmbCloneDb");
            return result;
        }

        public async Task AddUserAsync(UserDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spUser_Insert", model, "RmbCloneDb");
        }

        public async Task<UserDBModel> FindByEmailAsync(string email)
        {
            var result = await _sql.LoadDataAsync<UserDBModel, dynamic>("dbo.spUser_EmailLookup", new { Email = email }, "RmbCloneDb");
            return result.FirstOrDefault();

        }
        public async Task<UserDBModel> FindByIdAsync(string id)
        {
            var result = await _sql.LoadDataAsync<UserDBModel, dynamic>("dbo.spUser_LookupById", new { Id = id }, "RmbCloneDb");
            return result.FirstOrDefault();

        }

        public async Task<bool> CheckPasswordAsync(UserDBModel user, string password)
        {
            var task = Task.Run(() =>
            {
                if (user == null || password == null)
                {
                    return false;
                }
                var check = BCrypt.Net.BCrypt.EnhancedVerify(password, user.PasswordHash, HashType.SHA256);
                return check;
            });
            return await task;
        }


        private async Task<RoleDBModel> GetRoleByNameAsync(string name)
        {
            var result = await _sql.LoadDataAsync<RoleDBModel, dynamic>("dbo.spRole_Lookup", new { Name = name }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

        public async Task AssignRoleAsync(UserDBModel user, string roleName)
        {
            var role = await GetRoleByNameAsync(roleName);

            if (role == null || user == null)
            {
                throw new Exception("Rola ili korisnik ne postoji.");
            }
            var p = new { UserId = user.Id, RoleId = role.Id };
            await _sql.SaveDataAsync("dbo.spUserRole_Insert", p, "RmbCloneDb");
        }
        public async Task RemoveRoleAsync(UserDBModel user, string roleName)
        {
            var role = await GetRoleByNameAsync(roleName);
            if (role == null || user == null)
            {
                throw new Exception("Rola ili korisnik ne postoji.");
            }
            var p = new { UserId = user.Id, RoleId = role.Id };
            await _sql.SaveDataAsync("dbo.spUserRole_Delete", p, "RmbCloneDb");
        }





    }
}
