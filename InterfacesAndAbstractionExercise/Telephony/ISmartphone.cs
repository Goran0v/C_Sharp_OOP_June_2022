using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public interface ISmartphone
    {
        void Call(string phoneNumber);
        void Browse(string link);
    }
}
