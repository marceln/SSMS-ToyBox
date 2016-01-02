using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightInject;
using ToyBox.UI.ObjectLookup;

namespace ToyBox.UI
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ObjectLookupForm>();
        }
    }
}
