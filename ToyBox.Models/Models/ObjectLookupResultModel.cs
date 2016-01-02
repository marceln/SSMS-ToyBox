using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyBox.Models.Models
{
    public class ObjectLookupResultModel
    {
        public ObjectLookupResultModel()
        {
            MatchElements = new List<string>();
        }

        public DatabaseObjectModel DatabaseObject { get; set; }
        public int MatchWeight { get; set; }
        public List<string> MatchElements { get; private set; }
        public int MatchIndex { get; set; }
    }
}
