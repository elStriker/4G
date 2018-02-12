using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TCPListener listener = new TCPListener();
            listener.Listen();
        }
    }
}
