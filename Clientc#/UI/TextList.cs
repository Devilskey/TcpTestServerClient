using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientc_.UI
{
    public class TextList : UIElement
    {
        private int posX;
        private int posY;
        private int fontsize;
        private List<string> textList;

        public TextList(int x, int y, int sizeFont, List<string> textList)
        {
            fontsize = sizeFont;
            posX = x;
            posY = y;
            this.textList = textList;

        }
        public void OnRender()
        {
            try
            {
                List<string> ListSoFar = textList.ToList();
                int Down = 0;
                foreach (string str in ListSoFar)
                {
                    Console.WriteLine(str);
                    Raylib.DrawText($"> {str}", posX, posY + Down, fontsize, Color.BLACK);
                    Down += fontsize + 20;
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public void OnUpdate() { }
    }
}
