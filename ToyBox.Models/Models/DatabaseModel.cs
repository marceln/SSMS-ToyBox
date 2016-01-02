using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyBox.Models.Models
{
    public class DatabaseModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string StateDescription { get; set; }

        public ServerModel Server { get; set; }
    }
}
