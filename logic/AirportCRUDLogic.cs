using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Database_First_CPHAirport.dal;
using ORM_Database_First_CPHAirport.view;

namespace ORM_Database_First_CPHAirport.logic
{
    internal class AirportCRUDLogic
    {
        private string menuText = @"
###########################################
#    AIRPORT CREATE READ UPDATE DELETE    #
###########################################

Please press a number key:
  1. New Airport | 2. Edit Airport
  |    Any Other Number: Quit    |
";

        private Outputter outputter = new Outputter();
        private InputAcquirer inputAcquirer = new InputAcquirer();

        private AirportCRUDHandler ACRUDDB = new AirportCRUDHandler();

        internal void AirportCRUDMenuLoop()
        {
            

            int choice;
            bool done = false;
            while (!done)
            {
                outputter.PrintMenu(menuText);
                choice = inputAcquirer.AcquireValidInteger();
                done = ProcessChoiceFromMenu(choice);
            }
        }

        private bool ProcessChoiceFromMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    // New Airport
                    ACRUDDB.Create(ConstructAirport(new Airport()));
                    break;
                case 2:
                    // Edit Airport 
                    EditAirportMenu();
                    break;
                default:
                    // Return to main menu
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Fills an Airport object, whether for overwriting an existing one (updating) or creating a new one from scratch!
        /// </summary>
        /// <param name="airport"></param>
        /// <returns>Same airport, but with value(s) changed!</returns>
        private Airport ConstructAirport(Airport airport)
        {
            bool newAirport = airport.iATA == null;
            // IATA cannot be changed if the airport already exists, as it is my Primary Key, which in hindsight might not have been the best idea.. w/e.
            if (newAirport)
            {
                // New airports get to set the IATA
                outputter.WriteColoredMessage("What is this new airport's IATA code?");
                airport.iATA = inputAcquirer.AcquireStringOfLength(3, 3);
            }
            outputter.WriteColoredMessage("What is the airport's name?" + (newAirport ? "" : "\nIt is currently named " + airport.name));
            airport.name = inputAcquirer.AcquireStringOfLength(3, 255);

            return airport;
        }


        private void EditAirportMenu()
        {
            // -> Select Airport -> Select Action Update (name) / Delete (if allowed)
            List<Airport> airports = ACRUDDB.GetAirports();
            outputter.PrintMenu("AIRPORT EDIT\n\nWhich Airport would you like to Edit?\nPlease enter its IATA\nIATA - Name");
            outputter.WriteColoredMessage(
                string.Join("\n ", 
                    airports.Select(a => a.iATA + " - " + a.name)
                    ));

            string iATA;
            Airport airport = null;
            bool validIATA = false;
            while (!validIATA)
            {
                iATA = inputAcquirer.AcquireStringOfLength(3, 3).ToUpper();
                if(airports.Any(a => a.iATA.Equals(iATA)))
                {
                    airport = airports.First(a => a.iATA.Equals(iATA));
                    validIATA = true;
                }
            }
            // Now that we know which Airport the user wants to toy with, we need to find out what the users can and wants to do.

            outputter.PrintMenu(" # EDIT " + airport.iATA + " - " + airport.name.ToUpper() + " #\n  Choose: 1. Change Name | " + (airport.FlightRoute != null ? "" : "2. Delete Airport | ") + "Any other number to Exit");
            int choice = inputAcquirer.AcquireValidInteger();

            if(choice == 1)
            {
                // Acquire new Name
                airport = ConstructAirport(airport);
                // Apply Update
                ACRUDDB.Update(airport);
            }
            else if(choice == 2 && airport.FlightRoute == null)
            {
                //delete
                ACRUDDB.Delete(airport);
            }
        }
    }
}