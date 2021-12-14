using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace RmbClone.Library.Internal.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }


        public string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);

        }

        public async Task<List<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {

            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task SaveDataAsync<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
