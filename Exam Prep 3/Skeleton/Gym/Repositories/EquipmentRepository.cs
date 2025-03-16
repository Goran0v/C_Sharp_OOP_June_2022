using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private ICollection<IEquipment> equipment;

        public EquipmentRepository()
        {
            this.equipment = new HashSet<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models => (IReadOnlyCollection<IEquipment>)this.equipment;

        public void Add(IEquipment model)
        {
            this.equipment.Add(model);
        }

        public bool Remove(IEquipment model)
        {
            return this.equipment.Remove(model);
        }

        public IEquipment FindByType(string type)
        {
            return this.equipment.FirstOrDefault(e => e.GetType().Name == type);
        }
    }
}
