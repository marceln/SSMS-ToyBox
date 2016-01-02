using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyBox.Models.Query
{
    public static class GeneralQueriesProvider
    {
        public static string Databases { get { return Properties.Settings.Default.Query_Databases; } }
    }
}
