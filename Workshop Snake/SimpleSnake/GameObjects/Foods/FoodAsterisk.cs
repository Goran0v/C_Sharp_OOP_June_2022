using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
    public class FoodAsterisk : Food
    {
        private const int foodPoints = 1;
        private const char foodSymbol = '*';
        private const ConsoleColor foodColor = ConsoleColor.Yellow;

        public FoodAsterisk(Field field) 
            : base(field, foodSymbol, foodPoints, foodColor)
        {
        }
    }
}
