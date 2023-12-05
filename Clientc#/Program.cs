namespace Clientc_
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            TcpClientHandler.InitTCP();
            Game.Init(800, 800, "Hi");

            while (true)
            {
                Game.GameLoop();
            }
        }
    }
}