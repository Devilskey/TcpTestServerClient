using Clientc_.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServerC_;

public partial class TcpListernerHandler
{
    private int PORT;
    public IPAddress IpAdress;
    TcpListener tcpListener;
    Thread ListenerThreadAccept;
    Thread ListernerContinue;
    int buffer;
    List<UserObject> ClientList = new();
    int MaxUser = 10;
    int UserCount = 0;


    public TcpListernerHandler(int port = 80, string ip = "127.0.0.1", int databuffer = 1024) { 
        PORT = port;
        IpAdress = IPAddress.Parse(ip);
        buffer = databuffer;
        ListenerThreadAccept = new Thread(ListenerAccept);
        ListenerThreadAccept.Start();

        ListernerContinue = new Thread(ConnectionContinue);
        ListernerContinue.Start();
    }
    public bool ListenerAcceptThreadActive()
    {
        return ListenerThreadAccept.IsAlive;
    }

    public bool ListenerContinueActive()
    {
        return ListernerContinue.IsAlive;
    }

    public void ConnectionContinue()
    {
        bool started = true;

        while (started = true)
        {
            try
            {
                if(ClientList.Count != 0)
                    foreach (UserObject user in ClientList)
                    {
                        HandleConnections(user);
                    }
            } 
            catch (Exception ex)
            {
                ColorConsoleWrite.WriteError($"Error Happend At {ex}");
            }
            finally { 
                 started = false;
            }
        }
    }
     public void ListenerAccept()
     {


         tcpListener = new TcpListener(IpAdress, PORT);

         ColorConsoleWrite.WriteConnection($"Started Server on PORT: {PORT} and on IP: {IpAdress}");

         tcpListener.Start();

         bool started = true;

         while (started)
         {
            if (MaxUser == UserCount)
            {
                started = false;
            }
            else
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                if (client != null)
                {
                    _ = JoinConnectionHandler(client);
                    UserCount += 1;
                }
            }
         }
     }

    private void SendMessageToAllClients(UserObject SendedUser ,string message)
    {
        foreach (UserObject User in ClientList)
        {
                Console.WriteLine(message);
                try
                {
                    NetworkStream stream = User.tcp.GetStream();

                    string MessageTemplate = message;

                    byte[] data = Encoding.UTF8.GetBytes(MessageTemplate);

                    stream.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    ClientList.Remove(User);
                    UserCount -= 1;
                }
            }
    }

    private async Task JoinConnectionHandler(TcpClient client)
    {

        IPEndPoint iPEnd = (IPEndPoint)client.Client.RemoteEndPoint!;

        ColorConsoleWrite.WriteConnection($"client connected to {iPEnd.Address}:{iPEnd.Port}");

        NetworkStream stream = client.GetStream();

        while (!stream.DataAvailable)
        {
            await Task.Delay(200);
        }
            

        byte[] NewBuffer = new byte[buffer];
        int ReadByte = stream.Read(NewBuffer, 0, NewBuffer.Length);

        string Username = "";

        if (ReadByte > 0)
        {
            Username = Encoding.UTF8.GetString(NewBuffer);
            Console.WriteLine($"{iPEnd.Address}:{iPEnd.Port} User joined Named: {Username}");
        }

        if (Username != "")
        {
            UserObject NewUser = new UserObject()
            {
                tcp = client,
                userName = Username,
            };

            ClientList.Add(NewUser);
            SendMessageToAllClients(NewUser, $"User joined named: {Username}");
        }

        return;
    }

    private Task HandleConnections(UserObject user)
    {
        NetworkStream stream = user.tcp.GetStream();

        if (!stream.DataAvailable)
            return Task.CompletedTask;

        byte[] NewBuffer = new byte[buffer];
        int ReadByte = stream.Read( NewBuffer, 0, NewBuffer.Length );

        if (ReadByte > 0)
        {
            string dataRecieved = Encoding.UTF8.GetString( NewBuffer );
            byte[] usernameByt = Encoding.UTF8.GetBytes(user.userName);
            dataRecieved += Encoding.UTF8.GetString(usernameByt);

            Console.WriteLine(dataRecieved);
            string message = $"{dataRecieved}";
            SendMessageToAllClients(user, message);
        }

        return Task.CompletedTask;
    }
}
