using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientc_.UI
{
    public interface UIElement
    {
        public void OnRender() { }

        public void OnUpdate() { }
    }
}
