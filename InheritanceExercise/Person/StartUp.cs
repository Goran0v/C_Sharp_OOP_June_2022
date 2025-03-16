using System;
using System.Linq;
using System.Collections.Generic;

namespace Person
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            Person person = new Person(name, age);
            Console.WriteLine(person);
        }
    }
}