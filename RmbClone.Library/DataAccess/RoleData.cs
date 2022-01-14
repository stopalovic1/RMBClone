using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class RoleData : IRoleData
    {
        private readonly ISqlDataAccess _sql;

        public RoleData(ISqlDataAccess sql)
        {
            _sql = sql;
        }



        public async Task AddRoleAsync(RoleDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spRole_Insert", model, "RmBCloneDb");
        }

        public async Task<List<string>> GetRolesForUserAsync(string userId)
        {
            var result = await _sql.LoadDataAsync<string, dynamic>("dbo.spUserRole_LookupByUserId", new { UserId = userId }, "RmBCloneDb");
            return result;
        }




    }
}
