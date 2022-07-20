using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollatzConjecture.Core
{
    public static class Calculations
    {

        public static List<long> CollatzNumbers(long number, bool trimmed = false)
        {
            List<long> result = new List<long>();

            long n = number;

            for (long i = 0; i < 100000 && n != 1; i++)
            {
                if (n < 1)
                {
                    throw new Exception("Something strange happened");
                }

                if (Miscellaneous.IsEven(n))
                {
                    n /= 2;
                }
                else
                {
                    n = 3 * n + 1;
                }

                result.Add(n);

                if (trimmed && n < number)
                {
                    break;
                }
            }

            return result;
        }


        public static int CollatzSequenceLength(long number)
        {
            return CollatzNumbers(number).Count;
        }

        public static bool CollatzSequenceConvergesToOne(long number, bool trimmed = false)
        {
            if (number == 1) return true;

            if (trimmed)
            {
                long n = number;

                for (long i = 0; i < 100000 && n != 1; i++)
                {
                    if (n < 1)
                    {
                        throw new Exception("Something strange happened");
                    }

                    if (Miscellaneous.IsEven(n))
                    {
                        n /= 2;
                    }
                    else
                    {
                        n = 3 * n + 1;
                    }



                    if (n == 1 || n < number)
                    {
                        return true;
                    }

                }
                return false;
            }
            else
            {
                long lastMember = CollatzNumbers(number).Last();
                return lastMember == 1;
            }

        }

        public static bool InvalidCollatzNumberExistsUpTo(long upperBound)
        {
            for (long number = 1; number <= upperBound; number++)
            {
                if (!CollatzSequenceConvergesToOne(number, true))
                {
                    return true;
                }
            }

            return false;
        }

        public static long NumberOfSteps(long number)
        {
            return CollatzNumbers(number).Count;
        }

        public static (long mostSteps, long numberWithMostSteps) HighestNumberOfSteps(long upperBound, long lowerBound = 1)
        {
            long mostSteps = 1;
            long numberWithMostSteps = 1;

            for (long n = lowerBound; n <= upperBound; n++)
            {
                long steps = NumberOfSteps(n);

                if (steps > mostSteps)
                {
                    mostSteps = steps;
                    numberWithMostSteps = n;
                }
            }

            return (mostSteps, numberWithMostSteps);
        }

    }
}
