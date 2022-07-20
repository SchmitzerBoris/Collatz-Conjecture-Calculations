using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollatzConjecture.Core
{
    public static class Miscellaneous
    {        public static bool IsEven(long number)
        {
            return number % 2 == 0;
        }

        public static string GetSequenceString(List<long> numberList)
        {
            string result = "";

            bool firstNumber = true;

            foreach (long number in numberList)
            {
                if (!firstNumber)
                {
                    result += ", ";
                }
                else
                {
                    firstNumber = false;
                }

                result += number.ToString();
            }

            return result;
        }




    }
}
