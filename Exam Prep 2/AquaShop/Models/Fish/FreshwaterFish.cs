﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {
        private const int FreshwaterFishSize = 3;
        private const int FreshwaterFishIncr = 3;

        public FreshwaterFish(string name, string species, decimal price)
            : base(name, species, price)
        {
            this.Size = FreshwaterFishSize;
        }

        public override void Eat()
        {
            this.Size += FreshwaterFishIncr;
        }
    }
}
