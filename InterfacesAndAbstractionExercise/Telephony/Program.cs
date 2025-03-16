using System;
using System.Linq;

namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] numbers = Console.ReadLine().Split(' ').ToArray();
            string[] urls = Console.ReadLine().Split(' ').ToArray();

            Smartphone smartphone = new Smartphone();
            StationaryPhone stationaryPhone = new StationaryPhone();

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].Length == 10)
                {
                    smartphone.Call(numbers[i]);
                }
                else if (numbers[i].Length == 7)
                {
                    stationaryPhone.Dial(numbers[i]);
                }
            }

            for (int i = 0; i < urls.Length; i++)
            {
                smartphone.Browse(urls[i]);
            }
        }
    }
}
