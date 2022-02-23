using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Database_First_CPHAirport.view;

namespace ORM_Database_First_CPHAirport.logic
{
    internal class ViewInteractionProcessor
    {
        private string menuText = @"
###########################################
#    CPH AIRPORT FLIGHT ROUTES MANAGER    #
###########################################

Please press a number key:
  1. Airport CRUD | 2. Company CRUD
  3. Route CRUD   | 4. Route Leasing CRUD
  |  ----  Any Other Number: Quit  ----  |
";

        /// <summary>
        /// The program life cycle loop.
        /// </summary>
        internal void Looper()
        {
            Outputter outputter = new Outputter();
            InputAcquirer inputAcquirer = new InputAcquirer();
            int choice;

            bool done = false;
            while (!done)
            {
                outputter.PrintMenu(menuText);
                choice = inputAcquirer.AcquireValidInteger();
                done = ProcessChoiceFromMainMenu(choice);
            }
        }


        private bool ProcessChoiceFromMainMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    // Airport Crud
                    AirportCRUDLogic aCRUD = new AirportCRUDLogic();
                    aCRUD.AirportCRUDMenuLoop();
                    break;
                case 2:
                    // Company CRUD
                    break;
                case 3:
                    // Route CRUD
                    break;
                case 4:
                    // Route Leasing CRUD
                    break;
                default:
                    return true;
            }
            return false;
        }

        /*
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
        */
    }
}
