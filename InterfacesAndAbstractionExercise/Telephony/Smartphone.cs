using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class Smartphone : ISmartphone
    {
        public void Call(string phoneNumber)
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
                Console.WriteLine($"Calling... {phoneNumber}");
            }
        }

        public void Browse(string link)
        {
            bool isDigit = false;
            for (int i = 0; i < link.Length; i++)
            {
                if (char.IsDigit(link[i]))
                {
                    isDigit = true;
                }
            }

            if (isDigit)
            {
                Console.WriteLine("Invalid URL!");
            }
            else
            {
                Console.WriteLine($"Browsing: {link}!");
            }
        }
    }
}
