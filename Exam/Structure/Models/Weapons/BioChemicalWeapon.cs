using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.Weapons
{
    public class BioChemicalWeapon : Weapon
    {
        private const double BioChemicalWeaponPrice = 3.2;

        public BioChemicalWeapon(int destructionLevel)
            : base(BioChemicalWeaponPrice, destructionLevel)
        {
        }
    }
}
