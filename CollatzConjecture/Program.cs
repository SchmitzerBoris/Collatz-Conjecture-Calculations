using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace CollatzConjecture
{
    internal class Program
    {
        
        public static bool IsEven(long number)
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

        public static List<long> CollatzNumbers(long number, bool trimmed = false)
        {
            List<long> result = new List<long>();
            
            long n = number;
            
            for (long i = 0; i < 100000 && n != 1; i++)
            {
                if (n < 1)
                {
                    throw new Exception("Something fucky happened");
                }

                if (IsEven(n))
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

                    if (IsEven(n))
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
                ShowProgressBar(number * 100 / upperBound, 100, $" {number} of {upperBound} checked.");
            }

            Console.WriteLine("\n\nDone!");
            return false;
        }

        public static void ShowProgressBar(long relativeProgress, int barLength = 10, string additionalText = "")
        {
            string output = "[";

            for (long i = 1; i <= barLength; i++)
            {
                output += i <= relativeProgress ? "█" : " ";
            }

            output += "]";
            if (!string.IsNullOrWhiteSpace(additionalText))
            {
                output += " " + additionalText;
            }

            Console.Write($"\r{output}");
        }

        public static long NumberOfSteps(long number)
        {
            return CollatzNumbers(number).Count;
        }

        public static (long mostSteps, long numberWithMostSteps) HighestNumberOfSteps(long upperBound, long lowerBound = 1, bool progressBar = false)
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
                if (progressBar)
                {
                    ShowProgressBar(n * 100 / upperBound, 100, $"{n} of {upperBound} checked.");
                }
            }

            return (mostSteps, numberWithMostSteps);
        }

        public static void PrintHighestNumberOfSteps(long upperBound)
        {
            (long mostSteps, long numberWithMostSteps) = HighestNumberOfSteps(upperBound, 1, true);
            string result = $"The number {numberWithMostSteps} has {mostSteps} steps before reaching 1 : the highest number of Collatz steps up to {upperBound}.";
            Console.WriteLine("\n\n" + result);
        }

        public static void PrintCollatzSequencesUpTo(long number)
        {
            for (long n = 1; n <= number; n++)
            {
                string output = "> " + n.ToString() + " : " + GetSequenceString(CollatzNumbers(n));

                Console.WriteLine(output);
            }
        }

        public static void PrintNumbersWithCollatzSequenceValidityUpTo(long number)
        {
            for (long n = 1; n <= number; n++)
            {
                string output = (CollatzSequenceConvergesToOne(n) ? "." : "!") + " " + n.ToString() + " " + CollatzSequenceLength(n);

                Console.WriteLine(output);
            }
        }

        public static void ShowWhetherAllCollatzSequencesAreValidUpTo(long upperBound)
        {
            Console.WriteLine(InvalidCollatzNumberExistsUpTo(upperBound) ? "\nThe last checked number doesn't converge to 1." : "All numbers converge to 1!");
        }

        public static void ShowCollatzConjectureExplanation()
        {
            Console.WriteLine(
                "\n\n" +
                "COLLATZ CONJECTURE (From Wikipedia, the free encyclopedia)\n\n" +
                "The Collatz conjecture is one of the most famous unsolved problems in mathematics.\n" +
                "The conjecture asks whether repeating two simple arithmetic operations will eventually transform every positive integer into 1. " +
                "It concerns sequences of integers in which each term is obtained from the previous term as follows:\n" +
                "- If the previous term is even, the next term is one half of the previous term.\n" +
                "- If the previous term is odd, the next term is 3 times the previous term plus 1.\n" +
                "The conjecture is that these sequences always reach 1, no matter which positive integer is chosen to start the sequence.\n\n" +
                "It is named after mathematician Lothar Collatz, who introduced the idea in 1937, two years after receiving his doctorate.\n" +
                "It is also known as the 3n + 1 problem, the 3n + 1 conjecture, the Ulam conjecture(after Stanisław Ulam), Kakutani's problem (after Shizuo Kakutani), the Thwaites conjecture (after Sir Bryan Thwaites), Hasse's algorithm(after Helmut Hasse), or the Syracuse problem.\n" +
                "The sequence of numbers involved is sometimes referred to as the hailstone sequence or hailstone numbers (because the values are usually subject to multiple descents and ascents like hailstones in a cloud), or as wondrous numbers.\n\n" +
                "Paul Erdős said about the Collatz conjecture: 'Mathematics may not be ready for such problems.' He also offered US$500 for its solution. " +
                "Jeffrey Lagarias stated in 2010 that the Collatz conjecture 'is an extraordinarily difficult problem, completely out of reach of present day mathematics'.\n"
                );
        }



        public static void Wait(int timeMilliseconds = 1000)
        {
            Thread.Sleep(timeMilliseconds);
        }

        public static long PromptInputNumber(long maxValue = 10000)
        {
            bool aNumberIsEntered = false;
            string input;
            long number = 1;

            while (!aNumberIsEntered)
            {
                Console.WriteLine("\nWhat upper bound do you want to test? Whole positive numbers only.\n");

                input = Console.ReadLine();
                if (!long.TryParse(input, out number))
                {
                    Console.WriteLine("Input is not a valid number.");
                }
                else if (number > maxValue)
                {
                    Console.WriteLine("The number is too big.");
                }
                else aNumberIsEntered = true;
            }
            Console.WriteLine("");
            return number;
        }

        public void ShowCollatzStepsGraph(long startNumber, long width)
        {
            (long height, long numberWithMostSteps) = HighestNumberOfSteps(startNumber + width - 1, startNumber, false);

            bool[,] graph = new bool[height + 1, width + 1];



        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            
            bool running = true;

            List<string> actionOptions = new List<string>() 
            { 
                "all", 
                "valid", 
                "steps",
                "info",
                "exit"
            };
            List<string> actionDescriptions = new List<string>() 
            {
                "calculate all Collatz sequences up to a certain number",
                "calculate whether all Collatz sequences up to a certain number converge to 1",
                "calculate the highest number of Collatz steps up to a given number",
                "show an explanation of what the Collatz conjecture entails",
                "exit the program"
            };
           
            List<string> actionOptionsNoInputNumberRequired = new List<string>()
            {
                "info", "exit"
            };

            int numberOfActions = actionOptions.Count < actionDescriptions.Count ? actionOptions.Count : actionDescriptions.Count;


            string inputOperationChoice = "";
            bool anOperationIsChosen;
            double processTimeSeconds;
            DateTime startTime;
            DateTime endTime;


            while (running)
            {
                anOperationIsChosen = false;
                while (!anOperationIsChosen)
                {
                    Console.WriteLine("What would you like to do?\n");
                    for (int actionIndex = 0; actionIndex < numberOfActions; actionIndex++)
                    {
                        Console.WriteLine($"Enter '{actionOptions[actionIndex]}' to {actionDescriptions[actionIndex]}.");

                    }
                    
                    inputOperationChoice = Console.ReadLine();

                    if (!actionOptions.Contains(inputOperationChoice))
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                    else anOperationIsChosen = true;
                }                

                


                startTime = DateTime.Now;

                try
                {
                    if (inputOperationChoice == "all")
                    {
                        PrintCollatzSequencesUpTo(PromptInputNumber(10000));
                    }
                    else if (inputOperationChoice == "valid")
                    {
                        ShowWhetherAllCollatzSequencesAreValidUpTo(PromptInputNumber(100000000));
                    }
                    else if (inputOperationChoice == "steps")
                    {
                        PrintHighestNumberOfSteps(PromptInputNumber(1000000000));
                    }
                    else if (inputOperationChoice == "info")
                    {
                        ShowCollatzConjectureExplanation();
                    }
                    else if (inputOperationChoice == "exit")
                    {
                        Environment.Exit(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n" + ex.Message);
                }

                endTime = DateTime.Now;

                processTimeSeconds = (endTime - startTime).TotalSeconds;

                Console.WriteLine($"\nThis operation took {processTimeSeconds.ToString("0.00")} seconds.\n");
            }

            
        }



    }
}
