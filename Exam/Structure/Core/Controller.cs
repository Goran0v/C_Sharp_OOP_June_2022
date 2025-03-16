using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;

        public Controller()
        {
            this.planets = new PlanetRepository();
        }

        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = this.planets.FindByName(name);

            if (planet != null)
            {
                return String.Format(OutputMessages.ExistingPlanet, name);
            }

            planet = new Planet(name, budget);
            this.planets.AddItem(planet);

            return String.Format(OutputMessages.NewPlanet, name);
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = this.planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            IMilitaryUnit military;
            if (unitTypeName == "AnonymousImpactUnit")
            {
                military = new AnonymousImpactUnit();
            }
            else if (unitTypeName == "SpaceForces")
            {
                military = new SpaceForces();
            }
            else if (unitTypeName == "StormTroopers")
            {
                military = new StormTroopers();
            }
            else
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            foreach (var arm in planet.Army)
            {
                if (arm.GetType().Name == military.GetType().Name)
                {
                    throw new InvalidOperationException(String.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
                }
            }

            planet.Spend(military.Cost);
            planet.AddUnit(military);
            return String.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = this.planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            IWeapon weapon;
            if (weaponTypeName == "BioChemicalWeapon")
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == "NuclearWeapon")
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == "SpaceMissiles")
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            else
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            foreach (var w in planet.Weapons)
            {
                if (w.GetType().Name == weapon.GetType().Name)
                {
                    throw new InvalidOperationException(String.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
                }
            }

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);
            return String.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = this.planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);
            }

            planet.Spend(1.25);
            foreach (var arm in planet.Army)
            {
                arm.IncreaseEndurance();
            }

            return String.Format(OutputMessages.ForcesUpgraded, planetName);
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = this.planets.FindByName(planetOne);
            IPlanet secondPlanet = this.planets.FindByName(planetTwo);
            string message = string.Empty;

            if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                firstPlanet.Spend(firstPlanet.Budget / 2);
                firstPlanet.Profit(secondPlanet.Budget / 2);

                foreach (var arm in secondPlanet.Army)
                {
                    firstPlanet.Profit(arm.Cost);
                }

                foreach (var w in secondPlanet.Weapons)
                {
                    firstPlanet.Profit(w.Price);
                }

                this.planets.RemoveItem(planetTwo);
                message = String.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
            }
            else if (firstPlanet.MilitaryPower < secondPlanet.MilitaryPower)
            {
                secondPlanet.Spend(secondPlanet.Budget / 2);
                secondPlanet.Profit(firstPlanet.Budget / 2);

                foreach (var arm in firstPlanet.Army)
                {
                    secondPlanet.Profit(arm.Cost);
                }

                foreach (var w in firstPlanet.Weapons)
                {
                    secondPlanet.Profit(w.Price);
                }

                this.planets.RemoveItem(planetOne);
                message = String.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
            }
            else if (firstPlanet.MilitaryPower == secondPlanet.MilitaryPower)
            {
                bool firstHasNuclear = false;
                bool secondHasNuclear = false;

                foreach (var w in firstPlanet.Weapons)
                {
                    if (w.GetType().Name == "NuclearWeapon")
                    {
                        firstHasNuclear = true;
                    }
                }

                foreach (var w in secondPlanet.Weapons)
                {
                    if (w.GetType().Name == "NuclearWeapon")
                    {
                        secondHasNuclear = true;
                    }
                }

                if (firstHasNuclear && !secondHasNuclear)
                {
                    firstPlanet.Spend(firstPlanet.Budget / 2);
                    firstPlanet.Profit(secondPlanet.Budget / 2);

                    foreach (var arm in secondPlanet.Army)
                    {
                        firstPlanet.Profit(arm.Cost);
                    }

                    foreach (var w in secondPlanet.Weapons)
                    {
                        firstPlanet.Profit(w.Price);
                    }

                    this.planets.RemoveItem(planetTwo);
                    message = String.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
                }
                else if (!firstHasNuclear && secondHasNuclear)
                {
                    secondPlanet.Spend(secondPlanet.Budget / 2);
                    secondPlanet.Profit(firstPlanet.Budget / 2);

                    foreach (var arm in firstPlanet.Army)
                    {
                        secondPlanet.Profit(arm.Cost);
                    }

                    foreach (var w in firstPlanet.Weapons)
                    {
                        secondPlanet.Profit(w.Price);
                    }

                    this.planets.RemoveItem(planetOne);
                    message = String.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
                }
                else if ((firstHasNuclear && secondHasNuclear) || (!firstHasNuclear && !secondHasNuclear))
                {
                    firstPlanet.Spend(firstPlanet.Budget / 2);
                    secondPlanet.Spend(secondPlanet.Budget / 2);

                    message = OutputMessages.NoWinner;
                }
            }

            return message;
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var pl in this.planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name))
            {
                sb.AppendLine(pl.PlanetInfo());
            }

            return sb.ToString().Trim();
        }
    }
}
