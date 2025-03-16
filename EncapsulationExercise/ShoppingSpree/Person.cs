using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingSpree
{
    public class Person
    {
        private List<Product> products;
        private string name;
        private double money;

        public Person(string name, double money)
        {
            this.products = new List<Product>();
            this.Name = name;
            this.Money = money;
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                this.name = value;
            }
        }
        public double Money
        {
            get { return this.money; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                this.money = value;
            }
        }
        public IReadOnlyList<Product> Products
        {
            get
            {
                return this.products.AsReadOnly();
            }
        }
        public void BuyProduct(Product product)
        {
            if (product.Cost > this.Money)
            {
                throw new ArgumentException($"{this.Name} can't afford {product.Name}");
            }

            this.products.Add(product);
            this.Money -= product.Cost;
        }
        public override string ToString()
        {
            if (this.Products.Count > 0)
            {
                List<string> list = new List<string>();
                foreach (var pr in this.Products)
                {
                    list.Add(pr.Name);
                }
                return $"{this.Name} - {string.Join(", ", list)}";
            }

            return $"{this.Name} - Nothing bought";
        }
    }
}
