using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Database_First_CPHAirport.view
{
    internal class Outputter
    {
        /// <summary>
        /// Write a message to console, optionally in a ConsoleColor of your choice! Wow!
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cc"></param>
        internal void WriteColoredMessage(string message, ConsoleColor cc = ConsoleColor.White)
        {
            Console.ForegroundColor = cc;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Clears console and writes "menu" to console.
        /// </summary>
        /// <param name="menu"></param>
        internal void PrintMenu(string menu)
        {
            Console.Clear();
            Console.WriteLine(menu);
        }
    }
}
