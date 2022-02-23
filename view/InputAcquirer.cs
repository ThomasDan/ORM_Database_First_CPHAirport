using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Database_First_CPHAirport.view
{
    internal class InputAcquirer
    {
        // Probably not super correct to have an outputter in the inputter, but it's too damned convenient
        private Outputter outputter = new Outputter();

        /// <summary>
        /// Acquires a valid, single-digit Integer from Console
        /// </summary>
        /// <returns>Single digit integer</returns>
        internal int AcquireValidInteger()
        {
            int output = 99;
            bool validInt = false;
            while (!validInt)
            {
                if(int.TryParse(Console.ReadKey().KeyChar.ToString(), out output))
                {
                    validInt = true;
                }
                else
                {
                    outputter.WriteColoredMessage("\nInvalid Key! Must be a number; 0-9!", ConsoleColor.Red);
                }
            }
            return output;
        }

        /// <summary>
        /// Acquires a string of a definable character length from console.readline()
        /// </summary>
        /// <param name="min">Minimum length, 0 per default</param>
        /// <param name="max">Maximum length, 10k per default</param>
        /// <returns></returns>
        internal string AcquireStringOfLength(int min = 0, int max = 10000)
        {
            string output = "";
            bool validString = false;
            while (!validString)
            {
                output = Console.ReadLine();
                if (output.Length >= min && output.Length <= max)
                {
                    validString = true;
                }
                else
                {
                    outputter.WriteColoredMessage("\nInvalid Text! Must be at least " + min + " and at most " + max + " chars long", ConsoleColor.Red);
                }
            }

            return output;
        }
    }
}
