using System.Collections.Generic;
using ToyBox.Models.Enums;

namespace ToyBox.Models.Helpers
{
    public static class DbObjectTypeMap
    {

        #region Private data

        private static readonly IDictionary<string, DbObjectType> Map;

        #endregion

        #region Static constructor

        static DbObjectTypeMap()
        {
            Map = new Dictionary<string, DbObjectType>();
            CreateMappings();
        }

        #endregion

        public static DbObjectType GetMapping(string dbType)
        {
            return Map.ContainsKey(dbType) ? Map[dbType] : DbObjectType.Generic;
        }

        #region Initializer

        private static void CreateMappings()
        {
            Map.Add("*", DbObjectType.Assembly);
            Map.Add("ASS", DbObjectType.Assembly);
            Map.Add("AK", DbObjectType.AssymetricKey);
            Map.Add("CERT", DbObjectType.Certificate);
            Map.Add("C", DbObjectType.CheckConstraint);
            Map.Add("FS", DbObjectType.ClrScalarFunction);
            Map.Add("PC", DbObjectType.ClrStoredProcedure);
            Map.Add("PT", DbObjectType.ClrTableValuedFunction);
            Map.Add("CT", DbObjectType.Contract);
            Map.Add("DDLT", DbObjectType.DdlTrigger);
            Map.Add("D", DbObjectType.DefaultConstraint);
            Map.Add("EN", DbObjectType.EventNotification);
            Map.Add("EP", DbObjectType.ExtendedProperty);
            Map.Add("F", DbObjectType.ForeignKey);
            Map.Add("FTC", DbObjectType.FullTextCatalog);
            Map.Add("FTS", DbObjectType.FullTextStoplist);
            Map.Add("MT", DbObjectType.MessageType);
            Map.Add("PF", DbObjectType.PartitionFunction);
            Map.Add("PS", DbObjectType.PartitionScheme);
            Map.Add("PK", DbObjectType.PrimaryKey);
            Map.Add("SQ", DbObjectType.Queue);
            Map.Add("RLE", DbObjectType.Role);
            Map.Add("RTE", DbObjectType.Route);
            Map.Add("R", DbObjectType.Rule);
            Map.Add("FN", DbObjectType.ScalarFunction);
            Map.Add("SCH", DbObjectType.Schema);
            Map.Add("SVC", DbObjectType.Service);
            Map.Add("SB", DbObjectType.ServiceBinding);
            Map.Add("P", DbObjectType.StoredProcedure);
            Map.Add("SK", DbObjectType.SymmetricKey);
            Map.Add("SN", DbObjectType.Synonym);
            Map.Add("U", DbObjectType.Table);
            Map.Add("TF", DbObjectType.TableValuedFunction);
            Map.Add("UQ", DbObjectType.UniqueConstraint);
            Map.Add("USR", DbObjectType.User);
            Map.Add("TT", DbObjectType.UserDefinedType);
            Map.Add("V", DbObjectType.View);
            Map.Add("XSC", DbObjectType.XmlSchemaCollection);
            Map.Add("TR", DbObjectType.DmlTrigger);
        }

        #endregion
    }
}
