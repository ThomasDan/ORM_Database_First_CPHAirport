using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Database_First_CPHAirport.dal
{
    internal class AirportCRUDHandler
    {
        /// <summary>
        /// Insert Airport into Database
        /// </summary>
        /// <param name="airport"></param>
        internal void Create(Airport airport)
        {
            using (var ctx = new CPHAirportEntities())
            {
                ctx.Airports.Add(airport);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Acquires the airport, and sets the Route found to it, which is bad, but no time so deal with it
        /// </summary>
        /// <returns></returns>
        internal List<Airport> GetAirports()
        {
            List<Airport> airports;

            using (var ctx = new CPHAirportEntities())
            {
                // First we get the airports
                airports = ctx.Airports.ToList<Airport>();

                // Then we break all common coding sense, and break this class' area of responsibility by looking at its partner in crime, Flight Routes
                // In a perfect world, I had time to, IN THE LOGIC LAYER, call the FlightRoutes CRUD and sort out the route-airport relations from there.
                // BUt this is not a perfect world! And so you get this shit! Eat up!
                foreach(Airport airport in airports)
                {
                    if(airport.FlightRoute == null && ctx.FlightRoutes.Any(fr => fr.airport_fk.Equals(airport.iATA)))
                    {
                        airport.FlightRoute = ctx.FlightRoutes.First(fr => fr.airport_fk.Equals(airport.iATA));
                    }
                }
            }

            return airports;
        }

        /// <summary>
        /// Updates name of existing Airport
        /// </summary>
        /// <param name="airport"></param>
        internal void Update(Airport airport)
        {
            using (var ctx = new CPHAirportEntities())
            {
                ctx.Airports.First(a => a.iATA.Equals(airport.iATA)).name = airport.name;
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes Airport from Database.. Make Sure to check there are no foreign key constraints!!!
        /// </summary>
        /// <param name="airport"></param>
        internal void Delete(Airport airport)
        {
            using (var ctx = new CPHAirportEntities())
            {
                // First we Ensure that the context knows we're dealing with the same object-entity-thingamajig
                airport = ctx.Airports.First(a => a.iATA.Equals(airport.iATA));
                // Then we remove the Object.
                ctx.Airports.Remove(airport);
                // DOES WOT IT SAYS ON DE FOKIN TIN, M8!1!!
                ctx.SaveChanges();
            }
        }
    }
}
