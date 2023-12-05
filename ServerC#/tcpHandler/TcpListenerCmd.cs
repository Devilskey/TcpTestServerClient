using Clientc_.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerC_
{
    public partial class TcpListernerHandler
    {
        public void TcpListernerCMD(string CMD)
        {
            switch (CMD)
            {
                case "Connected":
                    Console.WriteLine($"{ClientList.Count}");
                    foreach (UserObject User in ClientList)
                    {
                        IPEndPoint iPEnd = (IPEndPoint)User.tcp.Client.RemoteEndPoint!;
                        Console.WriteLine($"{User.tcp.Available} client: {iPEnd.Address}");
                    }
                    break;
                case "Status":
                    Console.WriteLine($"Listener thread status: {ListenerAcceptThreadActive()}");
                    Console.WriteLine($"Connection thread status: {ListenerContinueActive()}");
                    break;
                default:
                    Console.WriteLine($"Welkom to the server");
                    Console.WriteLine($"running on {IpAdress}");
                    Console.WriteLine($"Commands Are: Status And Connected");
                    break;
            }
        }
    }
}
