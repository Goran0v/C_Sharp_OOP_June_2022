using System;
using System.Linq;
using System.Collections.Generic;

namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                string[] arr = Console.ReadLine().Split(' ').ToArray();
                string pizzaName = arr[1];
                Pizza pizza = new Pizza(pizzaName);
                string[] arr2 = Console.ReadLine().Split(' ').ToArray();
                Dough dough = new Dough(arr2[1], arr2[2], double.Parse(arr2[3]));
                pizza.AddDough(dough);
                string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    string[] cmd = command.Split(' ').ToArray();
                    Topping topping = new Topping(cmd[1], double.Parse(cmd[2]));
                    pizza.AddTopping(topping);
                }
                Console.WriteLine(pizza);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}
