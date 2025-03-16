using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
    public abstract class Food : Point
    {
        private readonly Field field;
        private readonly ConsoleColor foodColor;
        private readonly Random random;
        private readonly char foodSymbol;

        protected Food(Field field, char foodSymbol, int points, ConsoleColor foodColor)
            : base(field.LeftX, field.TopY)
        {
            random = new Random();
            this.field = field;
            FoodPoints = points;
            this.foodSymbol = foodSymbol;
            this.foodColor = foodColor;
        }

        public int FoodPoints { get; private set; }

        public void SetRandomPosition(Queue<Point> snake)
        {
            do
            {
                LeftX = random.Next(2, field.LeftX - 2);
                TopY = random.Next(2, field.TopY - 2);

            } while (snake.Any(p => p.LeftX == LeftX && p.TopY == TopY));

            Console.BackgroundColor = this.foodColor;
            Draw(foodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snake)
        {
            return snake.TopY == TopY && snake.LeftX == LeftX;
        }
    }
}
