using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineLibrary;
using AirlineLibrary.FlightPrinters;

namespace AirlineManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main method in class Program.");
            Airline zachepilovkaAir = new Airline(7);
            IFlightPrinter flightPrinter = new ConsoleFlightPrinter();
            Passenger[] passengers = new Passenger[10];
            passengers[0] = new Passenger("Truman", "Capote", "American", "GG 554493", DateTime.Now.AddDays(-5000),
                                        Sex.Male, new FlightTicket { Class = TicketClass.Business, Price = 350 }, 2222);
            zachepilovkaAir.AddFlight(new Flight(FlightDirection.Arrival, DateTime.Now, 3333, "Ulaanbaatar", AirportGate.B1, FlightStatus.Departed, 350, 25, passengers));
            flightPrinter.PrintFlightsInfo(zachepilovkaAir.Flights);
        }
    }
}
