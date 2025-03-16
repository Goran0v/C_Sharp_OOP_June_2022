using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            var knights = new List<Knight>();
            var barbarians = new List<Barbarian>();

            foreach (var pl in players)
            {
                if (pl.IsAlive)
                {
                    if (pl is Knight knight)
                    {
                        knights.Add(knight);
                    }
                    else if (pl is Barbarian barbarian)
                    {
                        barbarians.Add(barbarian);
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid player type.");
                    }
                }
            }

            var continueBattle = true;
            while (continueBattle)
            {
                var allKnightsAreDead = true;
                var allBarbariansAreDead = true;
                var aliveKnights = 0;
                var aliveBarbarians = 0;

                foreach (var knight in knights)
                {
                    if (knight.IsAlive)
                    {
                        allKnightsAreDead = false;
                        aliveKnights++;

                        foreach (var br in barbarians.Where(b => b.IsAlive))
                        {
                            var weaponDamage = knight.Weapon.DoDamage();

                            br.TakeDamage(weaponDamage);
                        }
                    }
                }

                foreach (var br in barbarians)
                {
                    if (br.IsAlive)
                    {
                        allBarbariansAreDead = false;
                        aliveBarbarians++;

                        foreach (var kn in knights.Where(k => k.IsAlive))
                        {
                            var weaponDamage = br.Weapon.DoDamage();
                            kn.TakeDamage(weaponDamage);
                        }
                    }
                }

                if (allKnightsAreDead)
                {
                    var deadBarbarians = barbarians.Count - aliveBarbarians;
                    return $"The barbarians took {deadBarbarians} casualties but won the battle.";
                }

                if (allBarbariansAreDead)
                {
                    var deadKnights = knights.Count - aliveKnights;
                    return $"The knights took {deadKnights} casualties but won the battle.";
                }
            }

            throw new InvalidOperationException("The fight logic has a bug.");
        }
    }
}
