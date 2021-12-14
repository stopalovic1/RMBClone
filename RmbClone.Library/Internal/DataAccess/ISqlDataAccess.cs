using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        string GetConnectionString(string name);
        Task<List<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionStringName);
        Task SaveDataAsync<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}