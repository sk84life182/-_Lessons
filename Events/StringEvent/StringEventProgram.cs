using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringEvent
{
    class StringEventProgram
    {
        static void Main(string[] args)
        {
            var list = new List<string> {"3.", "1", "4", "1", "5", "9", "2", "6", "5", "3", "5", "9" };

            StringSearcher test = new StringSearcher(list);

            test.OnActionArg += OnActionNotifyArg;

            test.Search(list);
        }

        private static void OnActionNotifyArg(object sender, StringEventArgs e)
        {
            if (e == null)
                return;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Строка \"{e.Found}\" найдена!\n");
            Console.ResetColor();
        }
    }

    class StringSearcher
    {
        public StringSearcher(List<string> list)
        {
            GeneralList =  list;
        }

        public List<string> GeneralList { get; }
        
        public event EventHandler<StringEventArgs> OnActionArg;

        public void Search(List<string> SomeList)
        {
            string pattern = @"3(.)?"; // шаблон поиска "3" или "3."

            Regex defaultRegex = new Regex(pattern);

            string decision = default;

            Console.WriteLine("Идёт поиск...\n");

            foreach (string s in SomeList)
            {
                Match match = Regex.Match(s, pattern);

                // если совпадение найдено
                if (match.Success)
                {
                    string output = match.Value;

                    OnActionArg?.Invoke(this, new StringEventArgs(output));

                    // цикл для возможности остановить или продолжить поиск совпадений
                    do
                    {
                        Console.WriteLine("Продолжить поиск? (Y/N)\n");

                        decision = Console.ReadLine();

                        Console.WriteLine();

                        if (decision != "Y" && decision != "y" && decision != "N" && decision != "n")
                            Console.WriteLine("Не понятно! Попробуем еще раз...\n");

                    } while (decision != "Y" && decision != "y" && decision != "N" && decision != "n");
                }

                if (decision == "Y" || decision == "y")
                    continue; // продолжить поиск

                if (decision == "N" || decision == "n")
                    break; // остановить поиск
            }

            Console.WriteLine("Поиск завершен!");
        }
    }

    public class StringEventArgs : EventArgs
    {
        public string Found { get; }

        public StringEventArgs(string found)
        {
            Found = found;
        }
    }
}
