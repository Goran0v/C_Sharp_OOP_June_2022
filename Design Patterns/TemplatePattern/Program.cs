using System;

namespace TemplatePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sourdough sourdough = new Sourdough();
            sourdough.Make();

            TwelveGrain twelveGrain = new TwelveGrain();
            twelveGrain.Make();

            WholeWheat wheat = new WholeWheat();
            wheat.Make();
        }
    }
}
