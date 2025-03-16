namespace SimpleSnake
{
    using SimpleSnake.Core;
    using SimpleSnake.Enums;
    using SimpleSnake.GameObjects;
    using System;
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            DifficultyLevel diffLevel = PromptDifficultyLevel();

            Field field = new Field(60, 20);
            DisplayDifficulty(field, diffLevel);
            Snake snake = new Snake(field);


            IEngine engine = new Engine(field, snake, diffLevel);
            engine.Run();
        }

        private static DifficultyLevel PromptDifficultyLevel()
        {
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");

            Console.Write("Your choice: ");
            bool validInteger = int.TryParse(Console.ReadLine(), out int diffLevel);

            if (!validInteger)
            {
                Console.WriteLine("Invalid choice!");
                Console.WriteLine("Try again!");
                Main();
            }

            if (diffLevel < 1 || diffLevel > 3)
            {
                Console.WriteLine("Invalid choice!");
                Console.WriteLine("Try again!");
                Main();
            }

            DifficultyLevel level = (DifficultyLevel)diffLevel;

            Console.Clear();
            return level;
        }

        private static void DisplayDifficulty(Field field, DifficultyLevel level)
        {
            Console.SetCursorPosition(field.LeftX + 2, 1);
            Console.Write($"Difficulty level: {level.ToString()}");
        }
    }
}
