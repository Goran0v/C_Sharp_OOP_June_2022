using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public abstract class Animal
    {
        private string name;
        private int age;
        private string gender;

        protected Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.Name), "Invalid input!");
                }
                else
                {
                    this.name = value;
                }
            }
        }
        public int Age
        {
            get { return this.age; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(nameof(this.Age), "Invalid input!");
                }
                else
                {
                    this.age = value;
                }
            }
        }
        public string Gender
        {
            get { return this.gender; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.Gender), "Invalid input!");
                }
                else
                {
                    this.gender = value;
                }
            }
        }

        public abstract string ProduceSound();

        public override string ToString()
        {
            return $"{this.GetType().Name}" + Environment.NewLine + $"{this.Name} {this.Age} {this.Gender}" + Environment.NewLine + $"{this.ProduceSound()}";
        }
    }
}
