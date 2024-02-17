using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UpdateGit 
{
    class Program 
    {
        static void Error_Read(string user) 
        {
            Console.WriteLine("Error Read");
            clients.Remove(user);
        }
        static void Error_Write(string user) 
        {
            Console.WriteLine("Error Write");
            clients.Remove(user);
        }
        static bool Write(Stream stream, string text,string user) 
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
        static void Write(Stream stream, string text)
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
        static string Read(Stream stream) 
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
        static string Read(Stream stream, string user)
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
                Error_Read(user);
            }
            return requst;
        }
        static bool isconected = false;
        static bool CheckConnect(Stream stream) 
        {
            bool requst = false;
            string test = "test";
            Write(stream,test);
            string answer_test = Read(stream);
            if (answer_test == "test") 
            {
                requst = true;
            }
            isconected = requst;
            return requst;
        }
        static Dictionary<string,TcpClient> clients = new Dictionary<string,TcpClient>();
        static void ThreadingClient(object obj) 
        {
            TcpClient client = obj as TcpClient;
            Stream stream = client.GetStream();
                Write(stream,"username");
                string username = Read(stream);
                    clients.Add(username, client);
                    while (true)
                    {
                
                    }
                   
        }
        static string host = "127.0.0.1";
        static int port = 25565;
        static void ThreadingServer() 
        {
            using (TcpListener server = new TcpListener(IPAddress.Any, port))
            {
                server.Start();
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("client connected");
                    new Thread(ThreadingClient).Start(client);
                }
            }
        }
       
        static string ServerCommands(string command) 
        {
            string[] helper = { "ul", "help", "connect", "clear","hide"};
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

                        while (true) {
                            Console.WriteLine("Input command for client");
                            string _command = Console.ReadLine();
                            if (_command?.Trim().Length > 0 && command != null) 
                            {

                           
                            if (_command == "hide") 
                            {
                                break;
                            }
                            bool error = Write(stream, _command, username);
                            string answer = Read(stream, username);
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
                    foreach (var i in helper) { 
                        requst += $"help command - {i};\n";
                        
                    }
                   
                    break;
                    default:requst = "the command was not found"; break; 
            }
            return requst;
        }
        static void CheckConnectClient() 
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
                    }
                }

                Thread.Sleep(3000);
            }
        }
        static void Main()
        {
            Console.Title = "Server";
            /* using (WebClient client = new WebClient()) 
             {
                 string ouInside = @"C:\1\File.txt", urlInside = "ya.ry";
                 Console.WriteLine("input ouInside");
                 ouInside = Console.ReadLine();
                 Console.WriteLine("input urlInside");
                 urlInside = Console.ReadLine();
                 client.DownloadFile(urlInside, ouInside);



             }*/
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
                
                string answer ="null answer";
                Console.ForegroundColor = ConsoleColor.Green;
                if (command?.Trim().Length > 0 && command !=null) {
                    answer = ServerCommands(command);
                }
                Console.WriteLine(answer);
                Console.ResetColor();
            }
            t2.Join();
            t1.Join();
        }
    }
}