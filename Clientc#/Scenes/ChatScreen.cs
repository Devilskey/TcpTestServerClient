using Clientc_.UI;
using Raylib_cs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortice.Vulkan;

namespace Clientc_.Scenes
{
    public class ChatScreen : Scene
    {
        public List<UIElement> UI { get; set; } = new List<UIElement>();

        List<string> MessageList = new List<string>();


        int ScreenHeight { get; set; }
        int ScreenWidth { get; set; }

        public ChatScreen(int ScreenWidth, int screenHeight) {
            this.ScreenWidth = ScreenWidth;
            this.ScreenHeight = ScreenHeight;

            TextInput InputMessage = new TextInput((ScreenWidth / 2) - 200, screenHeight - 30, 400, 20);

            InputMessage.ActionOnComplete = () => {
                TcpClientHandler.SendMessage(InputMessage.text);
                InputMessage.text = "";
            };

            UI.Add(InputMessage);

            TcpClientHandler.RecievedMessage = (string Message) =>
            {
                MessageList.Add(Message);
                Console.WriteLine(Message);
            };
        }

        public void RenderScene()
        {
            foreach (UIElement element in UI)
            {
                element.OnRender();
            }
            Raylib.DrawText($"Welkom: {Game.Username}", (ScreenWidth / 2) - 100, 0, 20, Color.BLACK);
            int down = 0;
            string[] Messages = MessageList.ToArray();

            foreach (string message in Messages)
            {
                Raylib.DrawText(message, 10, 30 + down, 30, Color.BLACK);
                Console.WriteLine(message);
                down += 30;

            }
        }
    }
}
