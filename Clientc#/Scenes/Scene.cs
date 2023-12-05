using Clientc_.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientc_.Scenes
{
    interface Scene
    {
        public List<UIElement> UI { get; set; }

        public void RenderScene();
    }
}