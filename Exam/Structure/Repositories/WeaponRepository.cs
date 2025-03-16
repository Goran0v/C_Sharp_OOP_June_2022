using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly ICollection<IWeapon> weapons;

        public WeaponRepository()
        {
            this.weapons = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => (IReadOnlyCollection<IWeapon>)this.weapons;

        public IWeapon FindByName(string name)
        {
            return this.weapons.FirstOrDefault(w => w.GetType().Name == name);
        }

        public void AddItem(IWeapon model)
        {
            this.weapons.Add(model);
        }

        public bool RemoveItem(string name)
        {
            IWeapon weapon = this.FindByName(name);

            if (weapon == null)
            {
                return false;
            }
            else
            {
                return this.weapons.Remove(weapon);
            }
        }
    }
}
