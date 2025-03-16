using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Pizza
    {
        private string name;

        public Pizza(string name)
        {
            this.Name = name;
            this.Toppings = new List<Topping>();
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                this.name = value;
            }
        }
        private Dough @Dough { get; set; }
        public List<Topping> Toppings;
        public double TotalCalories
        {
            get
            {
                double totalCalories = 0;
                totalCalories += this.@Dough.Calories;
                foreach (var topping in this.Toppings)
                {
                    totalCalories += topping.Calories;
                }
                return totalCalories;
            }
        }

        public void AddTopping(Topping topping)
        {
            if (this.Toppings.Count + 1 > 10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }

            this.Toppings.Add(topping);
        }
        public void AddDough(Dough dough)
        {
            this.@Dough = dough;
        }
        public override string ToString()
        {
            return $"{this.Name} - {this.TotalCalories:f2} Calories.";
        }
    }
}
