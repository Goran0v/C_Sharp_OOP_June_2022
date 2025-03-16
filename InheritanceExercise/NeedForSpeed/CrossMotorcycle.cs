using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class CrossMotorcycle : Motorcycle
    {
        public CrossMotorcycle(int horsePower, double fuel)
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
