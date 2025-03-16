using System;
using System.Collections.Generic;
using System.Text;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public StackOfStrings(IEnumerable<string> stack)
        {
            this.AddRange(stack);
        }

        public bool IsEmpty()
        {
            if (this.Count == 0)
            {
                return true;
            }

            return false;
        }

        public void AddRange(IEnumerable<string> stack)
        {
            foreach (var str in stack)
            {
                this.Push(str);
            }
        }
    }
}
