﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class Car : Vehicle
    {
        public Car(int horsePower, double fuel)
            : base(horsePower, fuel)
        {

        }

        public override double FuelConsumption { get; set; } = 3;

        public override void Drive(double kilometers)
        {
            base.Drive(kilometers);
        }
    }
}