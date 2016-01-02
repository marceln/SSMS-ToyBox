using System.Collections.Generic;
using System.Threading.Tasks;
using ToyBox.Models.Enums;
using ToyBox.Models.Models;

namespace ToyBox.Services
{
    public interface IObjectFinderService
    {
        Task<IEnumerable<DatabaseObjectModel>> FindObjectAsync(DatabaseModel db, ServerModel server);
    }
}
