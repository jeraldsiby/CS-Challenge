using System;

namespace ConsoleApp1
{
    class Program
    {
        static string[] results = new string[50];
        static char key;
        static Tuple<string, string> names;
        static ConsolePrinter printer = new ConsolePrinter();

        static void Main(string[] args)
        {
            printer.Value("Press ? to get instructions.").ToString();
            if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    printer.Value("Press c to get categories \nPress r to get random jokes").ToString();
                    GetEnteredKey(Console.ReadKey());
                    if (key == 'c')
                    {
                        GetCategories();
                        PrintResults();
                    }
                    if (key == 'r')
                    {
                        printer.Value("Want to use a random name? y/n").ToString();
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                            {
                                GetNames();
                            }

                        printer.Value("Want to specify a category? y/n").ToString();
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                            {
                                printer.Value("Enter a category;").ToString();
                                string category = Console.ReadLine();
                                printer.Value("How many jokes do you want? (1-9)").ToString();
                                GetEnteredKey(Console.ReadKey());
                                int n = (int)char.GetNumericValue(key);
                                for (int i = 0; i < n; i++)
                                {
                                    GetRandomJokes(category);
                                    PrintResults();
                                }
                            }
                        else
                            {
                                printer.Value("How many jokes do you want? (1-9)").ToString();
                                GetEnteredKey(Console.ReadKey());
                                int n = (int)char.GetNumericValue(key);
                                for (int i = 0; i < n; i++)
                                {
                                    GetRandomJokes(null);
                                    PrintResults();
                                }
                            }
                    }
                    names = null;
                }
            }
        }

        private static void PrintResults()
        {
            printer.Value("[" + string.Join(",", results) + "]").ToString();
        }

        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D2:
                    key = '2';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                case ConsoleKey.N:
                    key = 'n';
                    break;
            }
        }

        private static void GetRandomJokes(string category)
        {
            new JsonFeed("https://api.chucknorris.io");
            results = JsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category);
        }

        private static void GetCategories()
        {
            new JsonFeed("https://api.chucknorris.io");
            results = JsonFeed.GetCategories();
        }

        private static void GetNames()
        {
            new JsonFeed("https://names.privserv.com/api");
            dynamic result = JsonFeed.Getnames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
