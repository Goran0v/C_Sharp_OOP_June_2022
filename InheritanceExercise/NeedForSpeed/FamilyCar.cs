﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class FamilyCar : Car
    {
        public FamilyCar(int horsePower, double fuel)
            : base(horsePower, fuel)
        {

        }

        public override double FuelConsumption => base.FuelConsumption;

        public override void Drive(double kilometers)
        {
            base.Drive(kilometers);
        }
    }
}
