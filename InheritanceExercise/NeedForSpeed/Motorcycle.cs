using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class Motorcycle : Vehicle
    {
        public Motorcycle(int horsePower, double fuel)
            : base(horsePower, fuel)
        {

        }

        public override double FuelConsumption => base.DefaultFuelConsumption;

        public override void Drive(double kilometers)
        {
            base.Drive(kilometers);
        }
    }
}
