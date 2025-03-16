using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private ICollection<IEquipment> equipment;
        private ICollection<IAthlete> athletes;

        protected Gym(string name, int capacity)
        {
            this.equipment = new HashSet<IEquipment>();
            this.athletes = new HashSet<IAthlete>();
            this.Name = name;
            this.Capacity = capacity;
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }

                this.name = value;
            }
        }

        public int Capacity { get; private set; }

        public double EquipmentWeight => this.equipment.Sum(e => e.Weight);

        public ICollection<IEquipment> Equipment => this.equipment;

        public ICollection<IAthlete> Athletes => this.athletes;

        public void AddAthlete(IAthlete athlete)
        {
            if (this.athletes.Count == this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }

            this.athletes.Add(athlete);
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return this.athletes.Remove(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            this.equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in this.athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            string message = string.Empty;
            if (this.athletes.Count > 0)
            {
                message = string.Join(", ", this.athletes.Select(a => a.FullName));
            }
            else
            {
                message = "No athletes";
            }

            return $"{this.Name} is a {this.GetType().Name}:" + Environment.NewLine + $"Athletes: {message}" + Environment.NewLine + $"Equipment total count: {this.equipment.Count}" + Environment.NewLine + $"Equipment total weight: {this.EquipmentWeight:f2} grams";
        }
    }
}
