using Clientc_.Scenes;
using Clientc_.UI;
using Raylib_cs;


namespace Clientc_
{
    public static class Game 
    {
        static List<Scene> SceneList = new List<Scene>();
        static Scene LoadedScene;
        public static string Username = "";
        static List<UIElement> ElementsUi = new List<UIElement>();

        public static void SwitchScene(int SceneId)
        {
            if (SceneList.Count >= SceneId)
            {
                LoadedScene = SceneList[SceneId];
            }
            else
            {
                Console.WriteLine("Error Scene Id does not exist");
            }
        }

        public static void Init(int width, int height, string title) {
            Raylib.InitWindow(width, height, title );
            Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);



            SceneList.Add(new MainScene(width, height));
            SceneList.Add(new ChatScreen(width, height));
            LoadedScene = SceneList.First();
        }

        public static void GameLoop()
        {
            ElementsUi = LoadedScene.UI;
            Render();
            UpdateGame();
        }

        public static void Render()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            LoadedScene.RenderScene();
            Raylib.EndDrawing();
        }

        public static void UpdateGame()
        {
            foreach (UIElement element in ElementsUi)
            {
                element.OnUpdate();
            }
        }
    }
}
