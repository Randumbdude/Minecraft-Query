
using System;
using System.Threading;

namespace Console_App
{
    class Program
    {
        private static bool threadComplete = false;

        public static void Main(string[] args)
        {
            Console.Clear();

            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            Console.Title = strWorkPath + @"\MC Server Utils";
            while (true)
            {
                threadComplete = false;
                Console.ForegroundColor = ConsoleColor.Red;

                //drawMenuPic();

                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Enter Server Address");
                Console.Write("> ");
                string address = Console.ReadLine();

                Console.WriteLine("\nEnter Server Port");
                Console.Write("> ");

                int port = 25565;

                try
                {
                    port = Int32.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There was an error with your port, port default set to 25565");
                    port = 25565;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press Any Key To Continue...");
                    Console.ReadKey();
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;

                //drawMenuPic();

                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Working");

                new Thread(() =>
                {
                    while (threadComplete == false)
                    {
                        Console.Write(".");
                        Thread.Sleep(100);
                    }
                }).Start();

                getServerStatus(address, port);

                ConsoleKeyInfo ckilast = default(ConsoleKeyInfo);
                ConsoleKeyInfo ckicurrent = default(ConsoleKeyInfo);

                TimeSpan tsignorerepeatedkeypresstimeperiod = new TimeSpan(0, 0, 0, 0, 250);

                do
                {
                    while (Console.KeyAvailable == false)
                        Thread.Sleep(250);

                    ckilast = default(ConsoleKeyInfo);

                    DateTime eatingendtime = DateTime.UtcNow.Add(tsignorerepeatedkeypresstimeperiod);

                    do
                    {
                        while (Console.KeyAvailable == true)
                        {
                            ckicurrent = Console.ReadKey(true);

                            if (ckicurrent.Key == ConsoleKey.X)
                                break;

                            if (ckicurrent != ckilast)
                            {
                                eatingendtime = DateTime.UtcNow.Add(tsignorerepeatedkeypresstimeperiod);

                                //Console.WriteLine("You pressed the '{0}' key.", ckicurrent.Key);

                                ckilast = ckicurrent;

                                continue;
                            }
                        }

                        if (Console.KeyAvailable == false)
                        {
                            Thread.Sleep(50);
                        }

                    } while (DateTime.UtcNow < eatingendtime);

                } while (ckicurrent.Key != ConsoleKey.Enter);

                Console.ReadLine();
                Console.Clear();

            }

        }

        private static void getServerStatus(string address, int port)
        {
            threadComplete = false;
            Thread.CurrentThread.IsBackground = true;
            try
            {
                MCServer server = new MCServer(address, port);
                ServerStatus status = server.Status();
                double ping = server.Ping();

                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("COMPLETE");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("]");

                Console.WriteLine("\nServer Found!");
                Console.WriteLine($"\nServer:   {server.Address}:{server.Port}");
                Console.WriteLine($"Version:  {status.Version.Name}");
                Console.WriteLine($"Ping:     {ping}ms");
                Console.WriteLine($"Players:  {status.Players.Online} / {status.Players.Max}");
                Console.WriteLine($"MOTD:     {parseMOTD(status.Description.Text)}");
                //Console.WriteLine($"Sample:   {status.Players.Sample.Name}");
                //Console.WriteLine($"Icon Base64: {status.Icon}");
                Console.Write("\nPress Enter To Continue...");
                threadComplete = true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: " + e.Message);
                var stList = e.StackTrace.ToString().Split('\\');
                threadComplete = true;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press Any Key To Continue...");
            }
        }

        private static string parseMOTD(string motd)
        {
            string sentence = motd;

            string finalOutput = "";

            string[] seperator = {"§1", "§2", "§3", "§4", "§5", "§6", "§7", "§8", "§9", "§a", "§b", "§c", "§d", "§e", "§f", "§k", "§l", "§m", "§n", "§o", "§r"};

            string[] words = sentence.Split(seperator, StringSplitOptions.None);

            foreach (string word in words)
            {
                finalOutput += word;
            }

            finalOutput = finalOutput.Replace("\n", " ").Replace("\r", "");

            return finalOutput;
        }
    }
}