using System.Text.RegularExpressions;

namespace ToyBox.Models.Helpers
{
    public class ObjectExplorerNodeRegex
    {
        public static readonly Regex DbRegex = new Regex(@"^Server\[@Name='(?<Server>[^']*)'\]/Database\[@Name='(?<Database>[^']*)'\]$");
        public static readonly Regex DbRegexStart = new Regex(@"^Server\[@Name='(?<Server>[^']*)'\]/Database\[@Name='(?<Database>[^']*)'\]");
        public static readonly Regex SpFolderRegex = new Regex(@"^Server\[@Name='(?<Server>[^']*)'\]/Database\[@Name='(?<Database>[^']*)'\]/Folder\[@Name='StoredProcedures' and @Type='StoredProcedure'\]");
        public static readonly Regex SpRegex = new Regex(@"Server\[[^\]]*\]/Database\[@Name='(?<Database>[^']*)'\]/StoredProcedure\[@Name='(?<StoredProcedure>[^']*)' and @Schema='(?<Schema>[^']*)'\]$");
        public static readonly Regex TableFolderRegex = new Regex(@"^Server\[@Name='(?<Server>[^']*)'\]/Database\[@Name='(?<Database>[^']*)'\]/Folder\[@Name='UserTables' and @Type='Table'\]");
        public static readonly Regex TableRegex = new Regex(@"^Server\[@Name='(?<Server>[^\]]*)'\]/Database\[@Name='(?<Database>[^']*)'\]/Table\[@Name='(?<Table>[^']*)' and @Schema='(?<Schema>[^']*)'\]$");
        public static readonly Regex ViewFolderRegex = new Regex(@"^Server\[@Name='(?<Server>[^']*)'\]/Database\[@Name='(?<Database>[^']*)'\]/Folder\[@Name='Views' and @Type='View'\]");
        public static readonly Regex ViewRegex = new Regex(@"^Server\[@Name='(?<Server>[^']*)'\]/Database\[@Name='(?<Database>[^']*)'\]/View\[@Name='(?<View>[^']*)' and @Schema='(?<Schema>[^']*)'\]$");
    }
}
