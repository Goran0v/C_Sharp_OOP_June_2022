using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBuyer> buyers = new List<IBuyer>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] arr = Console.ReadLine().Split(' ').ToArray();
                if (arr.Length == 4)
                {
                    IBuyer citizen = new Citizen(arr[0], int.Parse(arr[1]), arr[2], arr[3]);
                    buyers.Add(citizen);
                }
                else if (arr.Length == 3)
                {
                    IBuyer rebel = new Rebel(arr[0], int.Parse(arr[1]), arr[2]);
                    buyers.Add(rebel);
                }
            }

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                IBuyer buyer = buyers.FirstOrDefault(x => x.Name == command);
                if (buyer != null)
                {
                    buyer.BuyFood();
                }
            }

            int totalFood = 0;
            foreach (var buyer in buyers)
            {
                totalFood += buyer.Food;
            }
            Console.WriteLine(totalFood);
        }
    }
}
