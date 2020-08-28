﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TimeServer
{
    class Server
    {
        private static byte[] _buffer = new byte[1024];
        private static List<Socket> _clientSockets = new List<Socket>();
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static int count = 1;
        private static string name = "";
        static void Main(string[] args)
        {
            Console.Title = "Server";
            Console.WriteLine(IPAddress.Any);
            SetupServer();
            Console.ReadKey();
        }

        private static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 100);
            _serverSocket.Bind(endPoint);
            _serverSocket.Listen(1);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            Console.WriteLine("Server Setup complete!");

        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = _serverSocket.EndAccept(AR);
            _clientSockets.Add(socket);
            Console.WriteLine("Client Connected!");
            Console.WriteLine(_clientSockets[0].ToString());
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
            Console.WriteLine((IPEndPoint)socket.RemoteEndPoint);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallBack(IAsyncResult AR)
        {
            try
            {
                Socket socket = (Socket)AR.AsyncState;
                int received = socket.EndReceive(AR);
                byte[] dataBuf = new byte[received];
                Array.Copy(_buffer, dataBuf,received);


                string text = Encoding.ASCII.GetString(dataBuf);
                Console.WriteLine("Count Before If: " + count);
                string response = string.Empty;
                if (count == 1)
                {
                    Console.WriteLine("Text Received: " + text);
                    name = text;
                    Console.WriteLine("Name is :" + name);
                    Console.WriteLine("Count before increment" + count);
                    count++;
                    Console.WriteLine("Count after increment" + count);
                    response = name;
                } else
                {
                    Console.WriteLine("Text Received: " + text);
                    Console.WriteLine("Name is :" + name);

                    response = GenerateLetter.GenerateLetters(text);
                    //byte[] data = Encoding.ASCII.GetBytes(response);
                    //socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
                    //socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
                }

                //Console.WriteLine("Text Received: " + text);
                //Console.WriteLine("Name is :" + name);

                //bool found = CheckWord.CheckExistingWord(text);
                
                //string response = string.Empty;
                //response = GenerateLetter.GenerateLetters(text);
                //if (found == true)
                //{
                //    CountWords.CheckWordLenght(text);
                //    //response = Winner.score.ToString();
                //    response = GenerateLetter.GenerateLetters(text);

                //}
                //else
                //{
                //    response = "Word does not exist";
                //}
                


                byte[] data = Encoding.ASCII.GetBytes(response);
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);

            } catch(SocketException)
            {
                Console.WriteLine("Client Disconnected");
            }
            
        }

        private static void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }
}
