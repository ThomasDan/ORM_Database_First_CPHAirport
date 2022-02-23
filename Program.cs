using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Database_First_CPHAirport;

namespace ORM_Database_First_CPHAirport
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Airport> airports;
            using (var ctx = new CPHAirportEntities())
            {
                airports = ctx.Airports.ToList<Airport>();
                foreach (Airport ap in airports)
                {
                    if (!ap.iATA.Equals("CPH"))
                    {
                        Console.WriteLine("(" + ap.iATA + ") " + ap.name + ", route owned by " + ap.FlightRoute.Company.name);
                        if(ap.FlightRoute.Companies.Count > 0)
                        {
                            Console.WriteLine(" Other companies operating this route:");
                            foreach (Company com in ap.FlightRoute.Companies)
                            {
                                Console.WriteLine("   -" + com.name);
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
            
            Console.ReadLine();
        }
    }
}
