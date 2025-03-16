using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly ICollection<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => (IReadOnlyCollection<IPlanet>)this.planets;

        public IPlanet FindByName(string name)
        {
            return this.planets.FirstOrDefault(p => p.Name == name);
        }

        public void AddItem(IPlanet model)
        {
            this.planets.Add(model);
        }

        public bool RemoveItem(string name)
        {
            IPlanet planet = this.FindByName(name);

            if (planet == null)
            {
                return false;
            }
            else
            {
                return this.planets.Remove(planet);
            }
        }
    }
}
