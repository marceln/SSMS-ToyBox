using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToyBox.Services
{
    interface IBasicQueryExecutorService
    {
        Task<IEnumerable<T>> ReadAsync<T>(string connString, string query) where T : class;
        Task<IEnumerable<T>> ReadAsync<T>(string connString, string query, dynamic args) where T : class;
    }
}
