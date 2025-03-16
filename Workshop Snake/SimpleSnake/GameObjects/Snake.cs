using SimpleSnake.GameObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char snakeSymbol = '\u25CF';
        private const char emptySpace = ' ';

        private readonly Queue<Point> snakeElements;
        private readonly IList<Food> food;
        private readonly Field field;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;
        private int totalPoints;

        private Snake()
        {
            this.snakeElements = new Queue<Point>();
            this.food = new List<Food>();
            this.foodIndex = this.RandomFoodNumber;
            this.totalPoints = 0;
        }

        public Snake(Field field)
            : this()
        {
            this.field = field;
            
            this.GetFoods();
            this.CreateSnake();
        }

        private int RandomFoodNumber => new Random().Next(0, this.food.Count);

        public bool CanMove(Point direction)
        {
            Point currSnakeHead = this.snakeElements.Last();
            this.GetNextPoint(direction, currSnakeHead);
            bool isPointOfSnake = this.snakeElements.Any(x => x.LeftX == this.nextLeftX && x.TopY == this.nextTopY);

            if (isPointOfSnake)
            {
                return false;
            }

            Point newSnakeHead = new Point(this.nextLeftX, this.nextTopY);

            if (this.field.IsPointOfWall(newSnakeHead))
            {
                return false;
            }

            this.snakeElements.Enqueue(newSnakeHead);
            newSnakeHead.Draw(snakeSymbol);

            if (this.food[foodIndex].IsFoodPoint(newSnakeHead))
            {
                this.Eat(direction, currSnakeHead);
            }

            Point snakeTail = snakeElements.Dequeue();
            snakeTail.Draw(emptySpace);

            return true;
        }

        private void CreateSnake()
        {
            for (int topY = 1; topY <= 6; topY++)
            {
                this.snakeElements.Enqueue(new Point(2, topY));
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[foodIndex].SetRandomPosition(this.snakeElements);
        }

        private void GetFoods()
        {
            Type[] foodTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.ToLower().StartsWith("food") && !t.IsAbstract).ToArray();

            foreach (var foodType in foodTypes)
            {
                Food currFood = (Food)Activator.CreateInstance(foodType, new object[] { this.field });
                this.food.Add(currFood);
            }
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }

        private void Eat(Point direction, Point currSnakeHead)
        {
            int points = this.food[foodIndex].FoodPoints;
            this.totalPoints += points;

            for (int i = 0; i < points; i++)
            {
                Point newPoint = new Point(this.nextLeftX, this.nextTopY);
                this.snakeElements.Enqueue(newPoint);
                newPoint.Draw(snakeSymbol);

                this.GetNextPoint(direction, currSnakeHead);
            }

            this.VisualizePoints();

            this.foodIndex = this.RandomFoodNumber;
            this.food[foodIndex].SetRandomPosition(this.snakeElements);
        }

        private void VisualizePoints()
        {
            Console.SetCursorPosition(field.LeftX + 2, 2);
            Console.Write($"Player points: {this.totalPoints}");
        }
    }
}
