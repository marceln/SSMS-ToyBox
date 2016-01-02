namespace ToyBox.Models.SSMS
{
    public class ObjectExplorerContext
    {
        #region Properties

        public string Server { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }
        public string CurrentNodeName { get; set; }

        #endregion
    }
}
