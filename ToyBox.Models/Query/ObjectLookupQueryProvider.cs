using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyBox.Models.Query
{
    public static class ObjectLookupQueryProvider
    {
        public static string FindObject { get { return Properties.Settings.Default.Query_ObjectLookup; } }
    }
}
