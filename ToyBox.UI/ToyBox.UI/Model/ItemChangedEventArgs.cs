using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyBox.UI.Model
{
    public class ItemChangedEventArgs<T> : EventArgs where T : class
    {
        public T ChangedItem { get; set; }
    }
}
