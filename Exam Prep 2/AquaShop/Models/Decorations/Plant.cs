using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Decorations
{
    public class Plant : Decoration
    {
        private const int ComfortConst = 5;
        private const decimal PriceConst = 10m;

        public Plant()
            : base(ComfortConst, PriceConst)
        {
        }
    }
}
