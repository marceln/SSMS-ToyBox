using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace ToyBox.Services.Services
{
    internal class BasicQueryExecutorServiceImpl : IBasicQueryExecutorService 
    {
        #region IBasicQueryExecutorService implementation

        public async Task<IEnumerable<T>> ReadAsync<T>(string connString, string query) where T : class
        {
            return await Task.Factory.StartNew(() =>
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    return conn.Query<T>(query);
                }
            });
        }

        public async Task<IEnumerable<T>> ReadAsync<T>(string connString, string query, dynamic args) where T : class
        {
            return await Task.Factory.StartNew(() =>
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    return SqlMapper.Query<T>(conn, query, args);
                }
            });
        }

        #endregion
    }
}
