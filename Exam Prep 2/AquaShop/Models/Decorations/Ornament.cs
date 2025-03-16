using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Decorations
{
    public class Ornament : Decoration
    {
        private const int ComfortConst = 1;
        private const decimal PriceConst = 5m;

        public Ornament()
            : base(ComfortConst, PriceConst)
        {
        }
    }
}
