using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Robot : IRobot
    {
        public Robot(string model, string id)
        {
            this.Model = model;
            this.Id = id;
        }

        public string Model { get; private set; }
        public string Id { get; private set; }

        public void Detaining(string lastDigits)
        {
            if (this.Id.EndsWith(lastDigits))
            {
                Console.WriteLine(this.Id);
            }
        }
    }
}
