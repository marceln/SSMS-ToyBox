using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyBox.Models.SSMS;

namespace ToyBox.Models.Models
{
    public static class ModelExtensions
    {
        public static DatabaseModel ToDatabaseModel(this ObjectExplorerContext context)
        {
            return new DatabaseModel()
            {
                Name = context.Database
            };
        }

        public static ServerModel ToServerModel(this ObjectExplorerContext context)
        {
            return new ServerModel()
            {
                Name = context.Server,
                ConnectionString = context.ConnectionString
            };
        }
    }
}
