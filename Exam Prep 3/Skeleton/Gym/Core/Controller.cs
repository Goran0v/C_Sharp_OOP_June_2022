using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private EquipmentRepository equipment;
        private ICollection<IGym> gyms;

        public Controller()
        {
            this.equipment = new EquipmentRepository();
            this.gyms = new HashSet<IGym>();
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym;

            if (gymType == "BoxingGym")
            {
                gym = new BoxingGym(gymName);
            }
            else if (gymType == "WeightliftingGym")
            {
                gym = new WeightliftingGym(gymName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            this.gyms.Add(gym);
            return String.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment equipment;

            if (equipmentType == "BoxingGloves")
            {
                equipment = new BoxingGloves();
            }
            else if (equipmentType == "Kettlebell")
            {
                equipment = new Kettlebell();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            this.equipment.Add(equipment);
            return String.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IGym gym = this.gyms.FirstOrDefault(g => g.Name == gymName);
            string gymType = gym.GetType().Name;
            IEquipment equipment = this.equipment.FindByType(equipmentType);
            string message = string.Empty;

            if (equipment == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            if (equipmentType == "BoxingGloves")
            {
                if (gymType == "BoxingGym")
                {
                    gym.AddEquipment(equipment);
                    this.equipment.Remove(equipment);
                    message = String.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
                }
                else if (gymType == "WeightliftingGym")
                {
                    message = String.Format(ExceptionMessages.InexistentEquipment, equipmentType);
                }
            }
            else if (equipmentType == "Kettlebell")
            {
                if (gymType == "BoxingGym")
                {
                    message = String.Format(ExceptionMessages.InexistentEquipment, equipmentType);
                }
                else if (gymType == "WeightliftingGym")
                {
                    gym.AddEquipment(equipment);
                    this.equipment.Remove(equipment);
                    message = String.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
                }
            }

            //gym.AddEquipment(equipment);
            //this.equipment.Remove(equipment);
            //message = String.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
            return message;
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete athlete;

            if (athleteType == "Boxer")
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else if (athleteType == "Weightlifter")
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }

            IGym gym = this.gyms.First(g => g.Name == gymName);
            string gymType = gym.GetType().Name;

            string message = string.Empty;

            if (athleteType == "Boxer")
            {
                if (gymType == "BoxingGym")
                {
                    gym.AddAthlete(athlete);
                    message = String.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
                }
                else if (gymType == "WeightliftingGym")
                {
                    message = String.Format(OutputMessages.InappropriateGym);
                }
            }
            else if (athleteType == "Weightlifter")
            {
                if (gymType == "BoxingGym")
                {
                    message = String.Format(OutputMessages.InappropriateGym);
                }
                else if (gymType == "WeightliftingGym")
                {
                    gym.AddAthlete(athlete);
                    message = String.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
                }
            }

            return message;
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = this.gyms.FirstOrDefault(g => g.Name == gymName);
            int count = 0;

            foreach (var athlete in gym.Athletes)
            {
                athlete.Exercise();
                count++;
            }

            return String.Format(OutputMessages.AthleteExercise, count);
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = this.gyms.FirstOrDefault(g => g.Name == gymName);

            return String.Format(OutputMessages.EquipmentTotalWeight, gymName, gym.EquipmentWeight);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var gym in this.gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().Trim();
        }
    }
}
