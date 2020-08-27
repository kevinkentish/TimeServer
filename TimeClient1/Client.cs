using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TimeClient1
{
    class Client
    {
        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            Console.Title = "Client";
            Console.WriteLine("Enter Ip address: ");
            string ip = Console.ReadLine();
            IPAddress address = IPAddress.Parse(ip);
            Console.WriteLine(address);
            LoopConnect(address);
            SendLoop();
            Console.ReadKey();
        }

        private static void SendLoop()
        {
            while (true)
            {
                Console.Write("Enter a request: ");
                string req = Console.ReadLine();

                byte[] buffer = Encoding.ASCII.GetBytes(req);
                _clientSocket.Send(buffer);

                byte[] receivedBuf = new byte[1024];
                int rec = _clientSocket.Receive(receivedBuf);

                byte[] data = new byte[rec];
                Array.Copy(receivedBuf, data, rec);
                Console.WriteLine("Received " + Encoding.ASCII.GetString(data));
            }
        }

        private static void LoopConnect(IPAddress ipadd)
        {
            int attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect(ipadd, 100);
                }
                catch (SocketException)
                {
                    Console.Clear();
                    Console.WriteLine("Connection attempts : " + attempts.ToString());

                }
            }

            Console.WriteLine("Connected");


        }
    }
}
