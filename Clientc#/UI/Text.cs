using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientc_.UI
{
    public class Text : UIElement
    {
        private int posX;
        private int posY;
        private int fontsize;
        private string text;

        public Text(int x, int y, int sizeFont, string text)
        {
            fontsize = sizeFont;
            posX = x;
            posY = y;
            this.text = text;

        }
        public void OnRender() {
            Raylib.DrawText(text, posX, posY, fontsize, Color.BLACK);
        }

        public void OnUpdate() { }
    }
}
