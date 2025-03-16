using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        //private double militaryPower;
        private readonly ICollection<IMilitaryUnit> army;
        private readonly ICollection<IWeapon> weapons;

        private Planet()
        {
            this.army = new List<IMilitaryUnit>();
            this.weapons = new List<IWeapon>();
        }

        public Planet(string name, double budget)
            : this()
        {
            this.Name = name;
            this.Budget = budget;
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }

                this.name = value;
            }
        }

        public double Budget
        {
            get { return this.budget; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }

                this.budget = value;
            }
        }

        public double MilitaryPower => this.GetMilitaryPower();

        public IReadOnlyCollection<IMilitaryUnit> Army => (IReadOnlyCollection<IMilitaryUnit>)this.army;

        public IReadOnlyCollection<IWeapon> Weapons => (IReadOnlyCollection<IWeapon>)this.weapons;

        public void AddUnit(IMilitaryUnit unit)
        {
            this.army.Add(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public void TrainArmy()
        {
            foreach (var unit in this.army)
            {
                unit.IncreaseEndurance();
            }
        }

        public void Spend(double amount)
        {
            if (this.Budget - amount < 0)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }

            this.Budget -= amount;
        }

        public void Profit(double amount)
        {
            this.Budget += amount;
        }

        public string PlanetInfo()
        {
            string armyMessage = string.Empty;
            if (this.army.Count > 0)
            {
                armyMessage = string.Join(", ", this.army.Select(a => a.GetType().Name));
            }
            else
            {
                armyMessage = "No units";
            }

            string weaponMessage = string.Empty;
            if (this.weapons.Count > 0)
            {
                weaponMessage = string.Join(", ", this.weapons.Select(a => a.GetType().Name));
            }
            else
            {
                weaponMessage = "No weapons";
            }

            return $"Planet: {this.Name}" + Environment.NewLine + $"--Budget: {this.Budget} billion QUID" + Environment.NewLine + $"--Forces: {armyMessage}" + Environment.NewLine + $"--Combat equipment: {weaponMessage}" + Environment.NewLine + $"--Military Power: {this.MilitaryPower}";
        }

        private double GetMilitaryPower()
        {
            double total = 0;
            foreach (var arm in this.Army)
            {
                total += arm.EnduranceLevel;
            }
            foreach (var w in this.Weapons)
            {
                total += w.DestructionLevel;
            }

            if (this.army.Count > 0)
            {
                IMilitaryUnit militaryUnit = this.army.FirstOrDefault(a => a.GetType().Name == "AnonymousImpactUnit");

                if (militaryUnit != null)
                {
                    total *= 1.3;
                }
            }

            if (this.weapons.Count > 0)
            {
                IWeapon weapon = this.weapons.FirstOrDefault(w => w.GetType().Name == "NuclearWeapon");

                if (weapon != null)
                {
                    total *= 1.45;
                }
            }

            return Math.Round(total, 3);
        }
    }
}
