using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Clientc_.UI
{
    public class TextInput : UIElement
    {
        Rectangle TextBox;
        public string text = "";
        public Action ActionOnComplete;

        public TextInput(int X, int Y, int width, int height)
        {
            TextBox = new Rectangle()
            {
                X = X,
                Y = Y,
                Width = width + 4,
                Height = height + 4

            };
        }
        public void OnRender() 
        {
            Raylib.DrawRectangleRec(TextBox, Color.BEIGE);
            Raylib.DrawText(text, (int)TextBox.X + 2, (int)TextBox.Y + 2, 20, Color.BLACK );
        }

        public void OnUpdate() {
            bool MouseOnText = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), TextBox) ? true : false;

            if (MouseOnText)
            {
                Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_IBEAM);

                int key = Raylib.GetCharPressed();

                while(key > 0)
                {
                    if ((key >= 32) && (key <= 125))
                    {
                        text += (char)key;
                    }

                    key = Raylib.GetCharPressed();
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                {
                    if(text.Length != 0)
                        text = text.Remove(text.Length - 1);
                    
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                    if(ActionOnComplete != null)
                    {
                        ActionOnComplete.Invoke();
                    }
                }
            }
            else
            {
                Raylib.SetMouseCursor(MouseCursor.MOUSE_CURSOR_DEFAULT);
            }
        }
    }
}
