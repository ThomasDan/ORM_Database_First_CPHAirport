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

        private AirportCRUDHandler aCRUDDB = new AirportCRUDHandler();

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
                    aCRUDDB.Create(ConstructAirport(new Airport()));
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
        /// <returns>Same airport, but with value(s) set/changed!</returns>
        private Airport ConstructAirport(Airport airport)
        {
            bool newAirport = airport.iATA == null;
            // IATA cannot be changed if the airport already exists, as it is the Primary Key, which in hindsight might not have been the best idea.. w/e.
            if (newAirport)
            {
                // New airports get to set the IATA
                outputter.WriteColoredMessage("\nWhat is this new airport's IATA code?\n");
                airport.iATA = inputAcquirer.AcquireStringOfLength(3, 3);
            }
            outputter.WriteColoredMessage("\nWhat is the airport's name?" + (newAirport ? "" : "\nIt is currently named " + airport.name) + "\n");
            airport.name = inputAcquirer.AcquireStringOfLength(3, 255);

            return airport;
        }

        /// <summary>
        /// Takes you on a magical journey through all the potential airports, and the potential ways in which you can manipulate them.
        /// </summary>
        private void EditAirportMenu()
        {
            // First we get all the airports (and see if there are routes to them)
            List<Airport> airports = aCRUDDB.GetAirports();

            // Then we print them out and ask which one the user'd like to Edit, if any
            outputter.PrintMenu("AIRPORT EDIT\n\nWhich Airport would you like to Edit?\nPlease enter its IATA\n IATA - Name\n ");
            outputter.WriteColoredMessage(
                string.Join("\n ", 
                    airports.Select(a => a.iATA + " - " + a.name)
                    ));

            // Here we find out which one of the airports the user want to mess with
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
                else
                {
                    outputter.WriteColoredMessage("Airport not found! Enter a valid one.", ConsoleColor.Red);
                }
            }

            // Now we need to find out what the users can and wants to do.
            // The user cannot delete an airport which has a Route to it.
            outputter.PrintMenu(" # EDIT " + airport.iATA + " - " + airport.name.ToUpper() + " #\n  Choose: 1. Change Name | " + (airport.FlightRoute != null ? "" : "2. Delete Airport | ") + "Any other number to Cancel");
            int choice = inputAcquirer.AcquireValidInteger();

            if(choice == 1)
            {
                // Acquire new Name
                airport = ConstructAirport(airport);
                // Apply Update
                aCRUDDB.Update(airport);
            }
            else if(choice == 2 && airport.FlightRoute == null)
            {
                //delete
                aCRUDDB.Delete(airport);
            }
        }
    }
}