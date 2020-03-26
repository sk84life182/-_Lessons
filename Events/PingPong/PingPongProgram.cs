using System;
using System.Threading;

namespace PingPong
{
    class PingPongProgram
    {
        static void Main(string[] args)
        {
            PingPong game = new PingPong();

            Ping ping = new Ping();
            Pong pong = new Pong();

            game.OnGame += ping.GameMessage;
            game.OnGame += pong.GameMessage;

            string decision;

            do
            {
                game.Play();

                do
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Хотите сыграть еще раз? (Y/N)\n");
                    Console.ResetColor();

                    decision = Console.ReadLine();

                    Console.WriteLine();

                    if (decision != "Y" && decision != "y" && decision != "N" && decision != "n")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Не понятно! Попробуем еще раз...\n");
                        Console.ResetColor();
                    }

                } while (decision != "Y" && decision != "y" && decision != "N" && decision != "n");

            } while (decision != "N" && decision != "n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Игра окончена!");
            Console.ResetColor();
        }
    }

    public delegate void PingPongHandler(bool turn, bool ball);

    public class PingPong
    {
        public event PingPongHandler OnGame;

        public void Play()
        {
            string decision;

            do
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Кто начнёт игру? (Ping/Pong)\n");
                Console.ResetColor();

                decision = Console.ReadLine();

                Console.WriteLine();

                if (decision != "Ping" && decision != "ping" && decision != "Pong" && decision != "pong")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не понятно! Попробуем еще раз...\n");
                    Console.ResetColor();
                }                   

            } while (decision != "Ping" && decision != "ping" && decision != "Pong" && decision != "pong");

            bool turn;

            if (decision == "Pong" || decision == "pong") turn = true;
            else turn = false;

            Random rnd = new Random();

            int probability;

            bool ball;

            do
            {
                // вероятность, что текущий игрок отобъет подачу 4:5
                probability = rnd.Next(1, 11);

                // определение отбита подача или нет
                if (probability > 2) ball = true;
                else ball = false;

                OnGame?.Invoke(turn, ball);

                // смена хода
                if (turn) turn = false;
                else turn = true;

            } while (probability > 2);
        }
    }

    public class Ping
    {
        public void GameMessage(bool turn, bool ball)
        {
            // определение чей был ход
            if (turn)
            {
                // определение результата подачи
                if (ball)
                {
                    Console.Beep(1000, 200);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ping получил Pong!\n");
                    Console.ResetColor();
                    Thread.Sleep(500);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Ping промахнулся! ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Победил Pong!\n");
                    Console.ResetColor();
                    Console.Beep(1000, 1000);
                }
            }
        }
    }

    public class Pong
    {
        public void GameMessage(bool turn, bool ball)
        {
            // определение чей был ход
            if (!turn)
            {
                // определение результата подачи
                if (ball)
                {
                    Console.Beep(500,200);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Pong получил Ping!\n");
                    Console.ResetColor();
                    Thread.Sleep(500);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Pong промахнулся! ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Победил Ping!\n");
                    Console.ResetColor();
                    Console.Beep(500, 1000);
                }
            }
        }
    }
}
