using Clientc_.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientc_.Scenes
{
    public class MainScene : Scene
    {
        public List<UIElement> UI { get; set; } = new List<UIElement>();

        public MainScene (int ScreenWidth, int screenHeight)
        {
            TextInput inputUsername = new TextInput((ScreenWidth / 2) - 100, (screenHeight / 2) - 10, 200, 20);

            inputUsername.ActionOnComplete = () =>
            {
                TcpClientHandler.SendMessage(inputUsername.text);
                Game.Username = inputUsername.text;
                Console.WriteLine(Game.Username + "InputText Action");

                Game.SwitchScene(1);
            };

            UI.Add(inputUsername);
            UI.Add(new Text((ScreenWidth / 2) - 100, (screenHeight / 2) - 40, 20, "Enter UserName:"));
        }

        public void RenderScene()
        {
            foreach (UIElement element in UI)
            {
                element.OnRender();
            }
        }
    }
}
