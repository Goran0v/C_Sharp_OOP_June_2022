namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Car car = new Car(100, 20);
            car.Drive(20);
            System.Console.WriteLine(car.Fuel);
        }
    }
}
