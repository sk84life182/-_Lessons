using System;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();

            Event MyEvent = new Event();
            MyEvent.TimeEvent += counter.Handler1;

            MyEvent.OnTimeEvent();
        }
    }

    delegate void CounterHandler();

    class Event
    {
        public event CounterHandler TimeEvent;

        public void OnTimeEvent()
        {
            TimeEvent();
        }
    }

    public class Counter
    {
        public void Handler1()
        {
            Random rnd = new Random();
            int stopPoint = rnd.Next(0, 100);

            for (int i = 0; i < 100; i++)
            {
                if (i == stopPoint)
                {

                    Console.WriteLine($"Пора действовать, ведь уже {stopPoint}!");
                }
            }
        }
    }
}
