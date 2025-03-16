using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class Vehicle
    {
        public Vehicle(int horsePower, double fuel)
        {
            this.HorsePower = horsePower;
            this.Fuel = fuel;
        }

        public double DefaultFuelConsumption { get; set; } = 1.25;
        public virtual double FuelConsumption { get; set; }
        public double Fuel { get; set; }
        public int HorsePower { get; set; }

        public virtual void Drive(double kilometers)
        {
            if (this.FuelConsumption == this.DefaultFuelConsumption)
            {
                if (this.Fuel - this.DefaultFuelConsumption * kilometers >= 0)
                {
                    this.Fuel -= this.DefaultFuelConsumption * kilometers;
                }
            }
            else
            {
                if (this.Fuel - this.FuelConsumption * kilometers >= 0)
                {
                    this.Fuel -= this.FuelConsumption * kilometers;
                }
            }
        }
    }
}
