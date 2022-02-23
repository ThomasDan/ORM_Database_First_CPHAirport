using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Database_First_CPHAirport.logic;

namespace ORM_Database_First_CPHAirport
{
    class Program
    {
        static void Main(string[] args)
        {
            ViewInteractionProcessor vIP = new ViewInteractionProcessor();
            vIP.Looper();
        }
    }
}
