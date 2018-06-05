using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsManager.Helper
{
    public static class ValidationInput
    {
        public static int ValidateInput(this string input)
        {
            if (int.TryParse(input, out var number))
            {
                if (number >= 2 && number <= 15)
                {
                    return number;
                }
            }
            throw new ArgumentException("Wrong input or number is outside of [2, 15]");
        }
    }
}
