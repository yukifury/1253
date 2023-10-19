using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using Newtonsoft.Json.Bson;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace practice11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu.WhileMenu();
        }
    }
    class Menu
    {
        public static void WhileMenu()
        {
            StartMenu();
            KeyScanner.Cursor();
            KeyScanner.Scanning();
        }
        public static void StartMenu()
        {
            Console.Clear();
            Console.WriteLine("Console typing speed test.");
            Console.WriteLine("  Play");
            Console.WriteLine("  ScoreList");
            Console.WriteLine("  Exit");
        }
    }
    class SpeedTest
    {
        public static void Game()
        {
            GetName();
            KeyGame.Gaming();
        }
        public static void ScoreList()
        {
            Console.Clear();
            List<User1> scorelist = SerializeDeserialize.DeserializeList();
            int x = 1;
            if (scorelist != null)
            {
                Console.WriteLine("      ScoreList");
                foreach (User1 user in scorelist)
                {
                    if (user.Name != "FirstUser")
                    {
                        Console.WriteLine($"{x}.");
                        Console.WriteLine($"  Name: {user.Name}");
                        Console.WriteLine($"  Clicks per second: {user.CPS}");
                        Console.WriteLine($"  Date: {user.Date}");
                        x++;
                    }
                    else
                    {
                    }
                }
                Console.WriteLine("\nPress any key to return");
                Console.ReadKey();
            }
            else if (scorelist == null)
            {
                Console.WriteLine("Scorelist is empty for now.");
                Thread.Sleep(1500);
                Console.WriteLine("\nPress any key to return");
                Console.ReadKey();
            }

        }
        private static void GetName()
        {
            Console.Clear();
            Console.WriteLine("Hello! There is a typing speed test.\nEnter your name.");
            string input = Console.ReadLine();
            ChangeUserMeta.NameAdding(input);
            return;
        }
        protected static string Text()
        {
            string text = "As the golden sun dipped below the horizon, casting long shadows across the Hogwarts grounds, Harry felt a sense of foreboding wash over him. " +
                           "The Forbidden Forest loomed in the distance, its ancient trees whispering secrets of centuries past. " +
"The air was thick with magic, and Harry knew that another adventure was about to begin!"; 


            return text;
        }
    }
    class KeyGame : SpeedTest
    {
        public static int WrongChars;
        public static int RightChars;
        public static void Gaming()
        {
            Prepare();
            Typing.Type();
        }
        private static void Prepare()
        {
            Console.Clear();
            Console.WriteLine("Prepare to TYPE!");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("3");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("2");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("1");
            Thread.Sleep(1000);
            Console.Clear();
            Threader.ThreadStart();
            User.date = DateTime.Now;
        }
    }
    class Typing : KeyGame
    {

        private static int[] ChangeLog;
        private static char ActualChar;
        private static int currentIndex = 0;
        private static char input;
        private static string Text = SpeedTest.Text();
        public static void Type()
        {;
            ChangeLog = new int[372];
            Console.SetCursorPosition(0, 0);
            Console.Write(Text);
            Console.WriteLine("");
            GetChar();
            TypeChar();
        }
        private static void GetChar()
        {
            if (currentIndex < 370)
            {
                ActualChar = Text[currentIndex];
            }
            else if (currentIndex > 370)
            {
                Console.Clear();
                Console.WriteLine("End!");
                Console.WriteLine($"Rights: {KeyGame.RightChars}\n     Wrongs: {KeyGame.WrongChars}");
                Console.ReadKey();
            }
        }
        private static void TypeChar()
        {
            if (currentIndex != 370)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Backspace & currentIndex != 0)
                {
                    currentIndex--;
                    GetChar();
                    Change();
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Elapsed();
                }
                else
                {
                    GetChar();
                    char keyChar = key.KeyChar;
                    input += keyChar;
                    Check();
                }

            }
            else if (currentIndex == 370)
            {
                Console.Clear();
                Console.WriteLine("End!");
                Console.WriteLine($"Rights: {KeyGame.RightChars}\n     Wrongs: {KeyGame.WrongChars}");
                Console.ReadKey();
            }
        }
        private static void Check()
        {
            if (ActualChar == '!')
            {
                if (input == '!')
                {
                    RightChars++;
                    Elapsed();
                }
                else if (input != '!')
                {
                    WrongChars++;
                    Elapsed();
                }
               
            }
            else if (ActualChar == input)
            {
                ChangeLog[currentIndex] = 1;
                if (currentIndex < 120)
                {
                    Console.SetCursorPosition(currentIndex, 0);
                }
                else if (currentIndex > 120 & currentIndex < 240)
                {
                    int x = currentIndex - 120;
                    Console.SetCursorPosition(x, 1);
                }
                else if (currentIndex > 240)
                {
                    int x = currentIndex - 240;
                    Console.SetCursorPosition(x, 2);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(ActualChar);
                currentIndex++;
                input = '\0';
                KeyGame.RightChars++;
                Console.SetCursorPosition(5, 5);
                Console.WriteLine($"Rights: {KeyGame.RightChars}\n     Wrongs: {KeyGame.WrongChars}");
                TypeChar();
            }
            else if (ActualChar != input)
            {
                ChangeLog[currentIndex] = 2;
                if (currentIndex < 120)
                {
                    Console.SetCursorPosition(currentIndex, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(ActualChar);
                }
                else if (currentIndex > 120 & currentIndex < 240)
                {
                    int x = currentIndex - 120;
                    Console.SetCursorPosition(x, 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(ActualChar);
                }
                else if (currentIndex > 240)
                {
                    int x = currentIndex - 240;
                    Console.SetCursorPosition(x, 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(ActualChar);
                }
                currentIndex++;
                input = '\0';
                KeyGame.WrongChars++;
                Console.SetCursorPosition(5, 5);
                Console.WriteLine($"Rights: {KeyGame.RightChars}\n     Wrongs: {KeyGame.WrongChars}");
                TypeChar();
            }
        }
        private static void Change()
        {
            int x = ChangeLog[currentIndex];
            if (x == 1)
            {
                KeyGame.RightChars--;
            }
            else if (x == 2)
            {
                KeyGame.WrongChars--;
            }
            if (currentIndex < 120)
            {
                Console.SetCursorPosition(currentIndex, 0);
            }
            else if (currentIndex > 120 & currentIndex < 240)
            {
                int y = currentIndex - 120;
               Console.SetCursorPosition(y, 1);
            }
            else if (currentIndex > 240)
            {
                int y = currentIndex - 240;
                Console.SetCursorPosition(y, 2);
            }
           
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(ActualChar);
            input = '\0';
            Console.SetCursorPosition(5, 5);
            Console.WriteLine($"Rights: {KeyGame.RightChars}\n     Wrongs: {KeyGame.WrongChars}");
            ChangeLog[currentIndex] = 0;
            TypeChar();
        }
        private static void Elapsed()
        {
            Console.ResetColor();
            Threader.ThreadElapsed();
            ElapseInfo();
        }
        private static void ElapseInfo()
        {
            double x = ElapseCPS();
            double WPM = (x * 60) / 6;
            int y = KeyGame.RightChars;
            int d = KeyGame.WrongChars;
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("Your CPS:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   {x}");
            Console.ResetColor();
            Console.WriteLine("Your WPM:");
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine($"   {WPM}");
            Console.ResetColor();
            Console.WriteLine($"Rights: {y}\nWrongs: {d}");
            ChangeUserMeta.AddList();
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            Menu.WhileMenu();
        }
        private static double ElapseCPS()
        {
            TimeSpan time = Act.elapsed;
            double x = time.TotalSeconds;
            double wrong = KeyGame.WrongChars;
            double right = KeyGame.RightChars;
            double result = right - wrong;
            double result1 = result / x;
            User.CPS = result1;
            return result1;
        }
    }

    class KeyScanner
    {
        public static void Scanning()
        {
            Scanner();
            AfterScanAct();
        }
        public static void Scanner()
        {
            Act.key = Console.ReadKey();
        }
        public static void AfterScanAct()
        {
            ConsoleKeyInfo key = Act.key;
            if (key.Key == ConsoleKey.DownArrow & Act.pos != 3)
            {
                Console.SetCursorPosition(0, Act.pos);
                Console.WriteLine(" ");
                Act.pos++;
                Console.SetCursorPosition(0, Act.pos);
                Console.WriteLine(">");
                Scanning();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Act.pos = 1;
                Menu.WhileMenu();
            }
            else if (key.Key == ConsoleKey.UpArrow & Act.pos != 1)
            {
                Console.SetCursorPosition(0, Act.pos);
                Console.WriteLine(" ");
                Act.pos--;
                Console.SetCursorPosition(0, Act.pos);
                Console.WriteLine(">");
                Scanning();
            }
            else if (key.Key == ConsoleKey.DownArrow & Act.pos == 3)
            {
                Scanning();
            }
            else if (key.Key == ConsoleKey.UpArrow & Act.pos == 1)
            {
                Scanning();
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (Act.pos == 1)
                {
                    Act.pos = 1;
                    SpeedTest.Game();
                }
                else if (Act.pos == 2)
                {
                    Act.pos = 1;
                    SpeedTest.ScoreList();
                    Menu.StartMenu();
                    Scanning();
                }
                else if (Act.pos == 3)
                {
                    return;
                }
            }
            
        }
        public static void Cursor()
        {
            int x = Act.pos;
            Console.SetCursorPosition(0, x);
            Console.Write(">");
        }
    }

    class ChangeUserMeta
    {
        public static List<User1> ScoreList { get; set; }
        protected static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        protected static string serListPath = $"{desktopPath}//ScoreList.json";
        private static bool ListCreated;
        private static bool BoolChecker()
        {
            bool result = false;
            if (File.Exists(serListPath))
            {

                result = true;
                return result;
            }
            else if (!File.Exists(serListPath))
            {
                List<User1> firstList = new List<User1>();
                User1 firstUser = new User1();
                firstUser.Name = "FirstUser";
                firstUser.CPS = 100;
                firstUser.Date = DateTime.Now;
                firstList.Add(firstUser);
                string json = JsonConvert.SerializeObject(firstList);
                File.WriteAllText(serListPath, json);
                result = true;
                return result;
            }
            return result;
        }
        protected static List<User1> ActualList()
        {
            ChangeUserMeta.BoolChecker();
            string JsonText = File.ReadAllText(serListPath);
            List<User1> ActualList = JsonConvert.DeserializeObject<List<User1>>(JsonText);
            return ActualList;
        }
        public static void NameAdding(string name)
        {
            User.Name = name;
            return;
        }
        public static void AddList()
        {
            if (ListCreated = true)
            {
                List<User1> list = ActualList();
                User1 user1 = new User1();
                user1.Name = User.Name;
                user1.CPS = User.CPS;
                user1.Date = User.date;
                list.Add(user1);
                ChangeUserMeta.ScoreList = list;
                SerializeDeserialize.SerializeList();
                Console.WriteLine("Test was scored in a scorelist.");
            }

        }
    }
    class SerializeDeserialize : ChangeUserMeta
    {
        public static void SerializeList()
        {
            string JsonText = JsonConvert.SerializeObject(ChangeUserMeta.ScoreList);
            File.WriteAllText(serListPath, JsonText);
        }
        public static List<User1> DeserializeList()
        {
            List<User1> result = ChangeUserMeta.ActualList();
            return result;
        }
    }
    class Threader
    {
        private static Thread thread = new Thread(_ =>
        {
            sw.Start();
            int time = 0;
            while (x != 1)
            {
                Console.SetCursorPosition(5, 4);
                Console.ResetColor();
                Console.WriteLine($"Time spend: {time}sec.");
                System.Threading.Thread.Sleep(1000);
                time++;
            }
        });
        private static Stopwatch sw = new Stopwatch();
        private static int x = 0;
        public static void ThreadStart()
        {
            thread.Start();
        }
        public static void ThreadElapsed()
        {
            x = 1;
            Act.elapsed = sw.Elapsed;
            thread.Abort();
        }
    }

    class User1
    {
        public string Name;
        public double CPS;
        public DateTime Date;
    }

    class User
    {
        public static string Name { get; set; }
        public static double CPS { get; set; }
        public static DateTime date { get; set; }
    }
    class Act
    {
        public static ConsoleKeyInfo key;
        public static int pos = 1;
        public static TimeSpan elapsed;
    }
}
