using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class StationaryPhone : IStationaryPhone
    {
        public void Dial(string phoneNumber)
        {
            bool isNotADigit = false;
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (!char.IsDigit(phoneNumber[i]))
                {
                    isNotADigit = true;
                }
            }

            if (isNotADigit)
            {
                Console.WriteLine("Invalid number!");
            }
            else
            {
                Console.WriteLine($"Dialing... {phoneNumber}");
            }
        }
    }
}
