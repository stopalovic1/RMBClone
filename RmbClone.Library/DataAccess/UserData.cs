﻿using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var result = await _sql.LoadData<UserDBModel, dynamic>("dbo.spUser_Lookup", new { }, "RmbCloneDb");
            return result;
        }



        public async Task AddUserAsync(UserDBModel model)
        {
            await _sql.SaveData("dbo.spUser_Insert", model, "RmbCloneDb");
        }

        public async Task<UserDBModel> FindByEmailAsync(string email)
        {
            var result = await _sql.LoadData<UserDBModel, dynamic>("dbo.spUser_EmailLookup", new { Email = email }, "RmbCloneDb");
            return result.FirstOrDefault();

        }
    }
}