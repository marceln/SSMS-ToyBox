using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyBox.Models.Enums;
using ToyBox.Models.Helpers;

namespace ToyBox.Models.Models
{
    public class DatabaseObjectModel
    {
        #region Properties

        public string ObjectType { get; set; }
        public DbObjectType MappedObjectType { get { return DbObjectTypeMap.GetMapping(ObjectType); } }
        public string ObjectName { get; set; }
        public string SchemaName { get; set; }
        public string FullyQualifiedName { get; set; }

        #endregion

        #region Non-bound properties

        public DatabaseModel Database { get; set; }

        #endregion
    }
}
