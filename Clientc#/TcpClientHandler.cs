using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Clientc_
{
    public static class TcpClientHandler
    {
        private static int PORT;
        public static string IpAdress;
        static TcpClient tcpClient;
        static Thread ListenerThread;
        static int buffer;
        public static Action<string> RecievedMessage;


        public static void InitTCP(int port = 80, string ip = "127.0.0.1", int databuffer = 4048)
        {
            PORT = port;
            IpAdress = ip;
            buffer = databuffer;
            tcpClient = new TcpClient(IpAdress, PORT);

            ListenerThread = new Thread(CheckIfRecievedMessage);
            ListenerThread.Start();
        }

        public static void CheckIfRecievedMessage ()
        {
            bool started = true;
            while (started)
            {
                if (tcpClient != null)
                  HandleMessage(tcpClient);
            }
        }


        public static Task HandleMessage(TcpClient client)
        {
            NetworkStream stream = tcpClient.GetStream();

            if (!stream.DataAvailable)
                return Task.CompletedTask;

            byte[] NewBuffer = new byte[buffer];
            int ReadByte = stream.Read(NewBuffer, 0, NewBuffer.Length);

            if (ReadByte > 0)
            {
                string dataRecieved = Encoding.UTF8.GetString(NewBuffer);

                if (RecievedMessage != null)
                {
                    Console.WriteLine(dataRecieved);
                    RecievedMessage.Invoke(dataRecieved);
                }
                else
                {
                    Console.WriteLine(dataRecieved);
                }
            }
            
            return Task.CompletedTask;
        }


        public static void SendMessage(string message)
        {
            try
            {
                //Note To self Connects to the server when given the parameters 

                NetworkStream stream = tcpClient.GetStream();

                byte[] data = Encoding.UTF8.GetBytes(message);

                stream.Write(data, 0, data.Length);

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
