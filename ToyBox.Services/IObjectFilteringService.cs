using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ToyBox.Models.Enums;
using ToyBox.Models.Models;

namespace ToyBox.Services
{
    public interface IObjectFilteringService
    {
        Task<IEnumerable<ObjectLookupResultModel>> FilterObjectsAsync(string lookupTerm, IEnumerable<DatabaseObjectModel> results);
        Task<IEnumerable<ObjectLookupResultModel>> FilterObjectsAsync(string lookupTerm, IEnumerable<DatabaseObjectModel> results, StringSearchType searchType);
        Task<IEnumerable<ObjectLookupResultModel>> FilterObjectsAsync(string lookupTerm, IEnumerable<DatabaseObjectModel> results, StringSearchType searchType, DbObjectType objectType);
    }
}
