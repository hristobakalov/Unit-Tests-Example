using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise1
{
    /// <summary>
    /// Represent a roman numeral, created from an integer
    /// </summary>
    public class RomanNumeral
    {
        private readonly int number;
        public RomanNumeral(int number)
        {
            if (number < 0)
            {
                //throw new ArgumentException("The number cant be less than a 0");
            }

            this.number = number;
        }

        public static IDictionary<int, string> Specs
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 1000, "M" },
                    { 900, "CM" },
                    { 800, "DCCC" },
                    { 700, "DCC" },
                    { 600, "DC" },
                    { 500, "D" },
                    { 400, "CD" },
                    { 300, "CCC" },
                    { 200, "CC" },
                    { 100,"C" },
                    { 90, "XC" },
                    { 80,"LXXX" },
                    { 70,"LXX" },
                    { 60,"LX" },
                    { 50,"L" },
                    { 40, "XL" },
                    { 30, "XXX" },
                    { 20, "XX" },
                    { 10, "X" },
                    { 9, "IX" },
                    { 8, "VIII" },
                    { 7, "VII" },
                    { 6, "VI" },
                    { 5, "V" },
                    { 4, "IV" },
                    { 3, "III" },
                    { 2, "II" },
                    { 1, "I" }
                };
            }

        }

        public override string ToString()
        {
            return FromNumberToNumeral(this.number);
        }

        private static string FromNumberToNumeral(int number)
        {
            var result = new StringBuilder();
            var rest = number;

            // work at it until we've transformed the whole number to string
            while (rest > 0)
            {
                // find the largest number in specs we can subtract from the rest
                foreach (var spec in Specs)
                {
                    // spec number too large
                    if (spec.Key > rest)
                    {
                        // find a smaller value
                        continue;
                    }

                    // found a value we can append to result
                    result.Append(spec.Value);
                    // subtract that value from the rest
                    rest -= spec.Key;
                    break;
                }
            }

            return result.ToString();
        }
    }
}
