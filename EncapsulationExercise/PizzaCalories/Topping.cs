using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Topping
    {
        private string type;
        private double grams;
        private const double Meat = 1.2;
        private const double Veggies = 0.8;
        private const double Cheese = 1.1;
        private const double Sauce = 0.9;

        public Topping(string type, double grams)
        {
            this.Type = type;
            this.Grams = grams;
        }

        private string Type
        {
            get { return this.type; }
            set
            {
                if (value.ToLower() != "meat" && value.ToLower() != "veggies" && value.ToLower() != "cheese" && value.ToLower() != "sauce")
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                this.type = value.ToLower();
            }
        }
        private double Grams
        {
            get { return this.grams; }
            set
            {
                if (value < 1 || value > 50)
                {
                    char first = this.Type.ToUpper()[0];
                    string substitue = this.Type.Substring(1);

                    throw new ArgumentException($"{first + substitue} weight should be in the range [1..50].");
                }
                this.grams = value;
            }
        }
        public double Calories
        {
            get
            {
                if (this.Type == "meat")
                {
                    return 2 * this.Grams * Meat;
                }
                else if (this.Type == "veggies")
                {
                    return 2 * this.Grams * Veggies;
                }
                else if (this.Type == "cheese")
                {
                    return 2 * this.Grams * Cheese;
                }
                else if (this.Type == "sauce")
                {
                    return 2 * this.Grams * Sauce;
                }

                return 2 * this.Grams;
            }
        }
        public override string ToString()
        {
            return $"{this.Calories:f2}";
        }
    }
}
