using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Clientc_.Objects
{  
    public class UserObject
    {
        public string userName { get; set; }
        Vector2 Position { get; set; } = new Vector2(0, 0);
        public TcpClient tcp { get; set; }

    }
}
