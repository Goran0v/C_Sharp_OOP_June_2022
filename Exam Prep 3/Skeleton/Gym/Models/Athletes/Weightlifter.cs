using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Athletes
{
    public class Weightlifter : Athlete
    {
        private const int WeightlifterStamina = 50;

        public Weightlifter(string fullName, string motivation, int numberOfMedals)
            : base(fullName, motivation, WeightlifterStamina, numberOfMedals)
        {

        }

        public override void Exercise()
        {
            if (this.Stamina + 10 > 100)
            {
                this.Stamina = 100;
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }

            this.Stamina += 10;
        }
    }
}
