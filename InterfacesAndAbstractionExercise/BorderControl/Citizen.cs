using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Citizen : ICitizen, IBirthable, IBuyer
    {
        public Citizen(string name, int age, string id, string birthdate)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
            this.Birthdate = birthdate;
            this.Food = 0;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Id { get; private set; }
        public string Birthdate { get; private set; }
        public int Food { get; private set; }

        public void Detaining(string lastDigits)
        {
            if (this.Id.EndsWith(lastDigits))
            {
                Console.WriteLine(this.Id);
            }
        }

        public void Birthday(string year)
        {
            if (this.Birthdate.EndsWith(year))
            {
                Console.WriteLine(this.Birthdate);
            }
        }

        public void BuyFood()
        {
            this.Food += 10;
        }
    }
}
