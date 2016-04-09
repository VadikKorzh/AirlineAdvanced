using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineLibrary.FlightPrinters
{
    public class ConsoleFlightPrinter : IFlightPrinter
    {
        void SwapFlights(ref Flight flight1, ref Flight flight2)
        {
            Flight temp;
            temp = flight1;
            flight1 = flight2;
            flight2 = temp;
        }

        // forms a string to display the flight's info in a table
        string FormFlightString(Flight flight)
        {
            DateTimeFormatInfo formatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
            formatInfo.ShortTimePattern = "hh:mm tt";
            string shortDestination = flight.Destination.Length > 17 ? flight.Destination.Substring(0, 17)
                                                                     : flight.Destination;
            return String.Format($" │{flight.Time,22:g}  │  {shortDestination,-17}│   {flight.Number,-7:d4}" +
                                 $"│{flight.NumberOfPassengers,11} │  {flight.EconomyClassPrice,-10:c}│   {flight.Gate,-5}│  {flight.Status,-16}│");
        }

        // forms a string to display the passebger's info in a table
        string FormPassengerString(Passenger passenger)
        {
            DateTimeFormatInfo formatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
            formatInfo.ShortDatePattern = "yyyy/MM/dd";
            
            string fullName = $"{passenger.FirstName} {passenger.LastName}";
            string shortFullName = (fullName.Length > 20) ? fullName.Substring(0, 20) : fullName;
            string shortNationality = (passenger.Nationality.Length > 12) ? passenger.Nationality.Substring(0, 12) : passenger.Nationality;

            return String.Format($" │  {shortFullName,-20}│ {passenger.Sex,-8}│ {passenger.Birthday,-13:d}" +
                                 $"│ {shortNationality,-12}│ {passenger.Passport,-11}│   {passenger.FlightNumber,-7:d4}│ {passenger.Ticket.Class,-10}│ {passenger.Ticket.Price.ToString("c"),-10}│");
        }

        public void PrintFlightsInfo(Flight[] flights)
        {
            int numberOfArrivals = 0, numberOfDepartures = 0;
            // obtain the number of arrivals and departures
            int i, j;
            for (i = 0; i < flights.Length; i++)
            {
                if (flights[i] != null)
                {
                    if (flights[i].Direction == FlightDirection.Arrival)
                    {
                        numberOfArrivals++;
                    }
                    else
                    {
                        numberOfDepartures++;
                    }
                }
            }
            // sort the flights by datetime increase
            for (i = 0; i < flights.Length; i++)
            {
                for (j = 0; j < flights.Length - 1 - i; j++)
                {
                    if ((flights[j] == null && flights[j + 1] != null) || (flights[j + 1] != null && flights[j].Time > flights[j + 1].Time))
                    {
                        SwapFlights(ref flights[j], ref flights[j + 1]);
                    }

                }
            }
            // print info
            Console.WriteLine();
            if (numberOfArrivals != 0)
            {
                Console.WriteLine(" ┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine(" │                                                   ARRIVALS                                                  │");
                Console.WriteLine(" ├────────────────────────┬───────────────────┬──────────┬────────────┬────────────┬────────┬──────────────────┤");
                Console.WriteLine(" │          TIME          │    DESTINATION    │  FLIGHT  │ PASSENGERS │  EC PRICE  │  GATE  │      STATUS      │");
                Console.WriteLine(" ├────────────────────────┼───────────────────┼──────────┼────────────┼────────────┼────────┼──────────────────┤");

                foreach (var flight in flights)
                {
                    if (flight != null && flight.Direction == FlightDirection.Arrival)
                    {
                        Console.WriteLine(FormFlightString(flight));
                    }
                }
                Console.WriteLine(" └────────────────────────┴───────────────────┴──────────┴────────────┴────────────┴────────┴──────────────────┘");
            }

            if (numberOfDepartures != 0)
            {
                Console.WriteLine(" ┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine(" │                                                  DEPARTURES                                                 │");
                Console.WriteLine(" ├────────────────────────┬───────────────────┬──────────┬────────────┬────────────┬────────┬──────────────────┤");
                Console.WriteLine(" │          TIME          │    DESTINATION    │  FLIGHT  │ PASSENGERS │  EC PRICE  │  GATE  │      STATUS      │");
                Console.WriteLine(" ├────────────────────────┼───────────────────┼──────────┼────────────┼────────────┼────────┼──────────────────┤");
                foreach (var flight in flights)
                {
                    if (flight != null && flight.Direction == FlightDirection.Departure)
                    {
                        Console.WriteLine(FormFlightString(flight));
                    }
                }
                Console.WriteLine(" └────────────────────────┴───────────────────┴──────────┴────────────┴────────────┴────────┴──────────────────┘");
            }
        }

        // display a single flight's info in a table
        public void PrintFlightInfo(Flight flight)
        {
            Console.WriteLine();
            Console.WriteLine(" ┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────┐");
            if (flight.Direction == FlightDirection.Arrival)
            {
                Console.WriteLine(" │                                                   ARRIVALS                                                  │");
            }
            else
            {
                Console.WriteLine(" │                                                  DEPARTURES                                                 │");
            }
            Console.WriteLine(" ├────────────────────────┬───────────────────┬──────────┬────────────┬────────────┬────────┬──────────────────┤");
            Console.WriteLine(" │          TIME          │    DESTINATION    │  FLIGHT  │ PASSENGERS │  EC PRICE  │  GATE  │      STATUS      │");
            Console.WriteLine(" ├────────────────────────┼───────────────────┼──────────┼────────────┼────────────┼────────┼──────────────────┤");
            Console.WriteLine(FormFlightString(flight));
            Console.WriteLine(" └────────────────────────┴───────────────────┴──────────┴────────────┴────────────┴────────┴──────────────────┘");
        }

        public void PrintPassengersInfo(Passenger[] passengers)
        {
            Console.WriteLine(" ┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine(" │                                                 PASSENGERS                                                  │");
            Console.WriteLine(" ├──────────────────────┬─────────┬──────────────┬─────────────┬────────────┬──────────┬───────────┬───────────┤");
            Console.WriteLine(" │         NAME         │   SEX   │   BIRTHDAY   │ NATIONALITY │  PASSPORT  │  FLIGHT  │   CLASS   │   PRICE   │");
            Console.WriteLine(" ├──────────────────────┼─────────┼──────────────┼─────────────┼────────────┼──────────┼───────────┼───────────┤");

            foreach (var passenger in passengers)
            {
                if (passenger != null)
                {
                    Console.WriteLine(FormPassengerString(passenger));
                }
            }
            Console.WriteLine(" └──────────────────────┴─────────┴──────────────┴─────────────┴────────────┴──────────┴───────────┴───────────┘");
        }

        // display a single passenger's info in a table
        public void PrintPassengerInfo(Passenger passenger)
        {
            Console.WriteLine(" ┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine(" │                                                 PASSENGERS                                                  │");
            Console.WriteLine(" ├──────────────────────┬─────────┬──────────────┬─────────────┬────────────┬──────────┬───────────┬───────────┤");
            Console.WriteLine(" │         NAME         │   SEX   │   BIRTHDAY   │ NATIONALITY │  PASSPORT  │  FLIGHT  │   CLASS   │   PRICE   │");
            Console.WriteLine(" ├──────────────────────┼─────────┼──────────────┼─────────────┼────────────┼──────────┼───────────┼───────────┤");
            Console.WriteLine(FormPassengerString(passenger));
            Console.WriteLine(" └──────────────────────┴─────────┴──────────────┴─────────────┴────────────┴──────────┴───────────┴───────────┘");
        }
    }
}
