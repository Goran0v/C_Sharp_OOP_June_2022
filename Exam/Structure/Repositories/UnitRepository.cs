using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private readonly ICollection<IMilitaryUnit> army;

        public UnitRepository()
        {
            this.army = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models => (IReadOnlyCollection<IMilitaryUnit>)this.army;

        public IMilitaryUnit FindByName(string name)
        {
            return this.army.FirstOrDefault(m => m.GetType().Name == name);
        }

        public void AddItem(IMilitaryUnit model)
        {
            this.army.Add(model);
        }

        public bool RemoveItem(string name)
        {
            IMilitaryUnit militaryUnit = this.FindByName(name);

            if (militaryUnit == null)
            {
                return false;
            }
            else
            {
                return this.army.Remove(militaryUnit);
            }
        }
    }
}
