using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyBox.Models.SSMS
{
    public enum ObjectExplorerNodeType
    {
        Server = 0,
        DatabasesCollection,
        Database,
        TablesCollection,
        Table,
        ViewsCollection,
        View
    }
}
