using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;

namespace Client
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IPAddress _IPAddress = IPAddress.Loopback;
            IPEndPoint _Endpoint = new IPEndPoint(_IPAddress, 4200);
            Socket _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _Socket.Connect(_Endpoint);

            while (true)
            {
                Console.WriteLine("Spieler1 geben Sie eine Zahl ein amk: ");
                string P1Zahl1 = Console.ReadLine();
                byte[] buffer1 = new byte[1024];
                buffer1 = Encoding.ASCII.GetBytes(P1Zahl1);
                _Socket.Send(buffer1);

                // byte[] buffer2 = new byte[1024];
                //_Socket.Receive(buffer2);
                //string ausgabe = Encoding.ASCII.GetString(buffer2);
                //Console.WriteLine(ausgabe);
            }
        }
    }
}
