using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManager.Validators
{
    static class Validator
    {
        public static void ValidateInt(ref int inputInt, params int[] setOfValues)
        {
            bool isOkParse = false;
            bool isValid = false;
            string inputString;
            int valueToCheck;

            do
            {
                Console.Write(" ");
                inputString = Console.ReadLine();
                isOkParse = int.TryParse(inputString, out valueToCheck);
                if (isOkParse)
                {
                    isValid = setOfValues.Contains(valueToCheck);
                    if (isValid)
                    {
                        inputInt = valueToCheck;
                        return;
                    }
                    else
                    {
                        Console.WriteLine(" The value is out of range. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine(" Invalid input! Try again!");
                }
            } while (!isValid || !isOkParse);
        }
    }
}
