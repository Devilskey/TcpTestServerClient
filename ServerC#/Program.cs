using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace ServerC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
          TcpListernerHandler tcp = new TcpListernerHandler(80);

            Console.WriteLine($"Accept tcp Connection {tcp.ListenerAcceptThreadActive()}: Continue Connection {tcp.ListenerContinueActive()}");
            while (tcp.ListenerContinueActive())
            {
                tcp.TcpListernerCMD(Console.ReadLine());
            }
            Console.ReadLine();
        }
    }
}