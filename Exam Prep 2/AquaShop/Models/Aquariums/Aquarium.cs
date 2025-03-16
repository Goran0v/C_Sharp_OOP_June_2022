using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;

        private Aquarium()
        {
            this.Decorations = new HashSet<IDecoration>();
            this.Fish = new HashSet<IFish>();
        }

        protected Aquarium(string name, int capacity)
            : this()
        {
            this.Name = name;
            this.Capacity = capacity;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                this.name = value;
            }
        }

        public int Capacity { get; }

        public int Comfort => this.Decorations.Sum(d => d.Comfort);

        public ICollection<IDecoration> Decorations { get; }

        public ICollection<IFish> Fish { get; }

        public void AddFish(IFish fish)
        {
            if (this.Fish.Count == this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.Fish.Add(fish);
        }

        public bool RemoveFish(IFish fish)
        {
            return this.Fish.Remove(fish);
        }

        public void AddDecoration(IDecoration decoration)
        {
            this.Decorations.Add(decoration);
        }

        public void Feed()
        {
            foreach (var f in this.Fish)
            {
                f.Eat();
            }
        }

        public string GetInfo()
        {
            return $"{this.Name} ({this.GetType().Name}):" + Environment.NewLine + $"Fish: {(this.Fish.Count > 0 ? string.Join(", ", this.Fish.Select(f => f.Name)) : "none")}" + Environment.NewLine + $"Decorations: {this.Decorations.Count}" + Environment.NewLine + $"Comfort: {this.Comfort}";
        }
    }
}
