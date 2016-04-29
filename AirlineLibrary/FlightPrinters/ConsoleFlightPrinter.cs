using KRZHK.AirlineLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineLibrary.FlightPrinters
{
    public class ConsoleFlightPrinter : IFlightPrinter
    {
        // forms a string to display the flight's info in a table
        string GetFlightString(Flight flight)
        {
            DateTimeFormatInfo formatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
            formatInfo.ShortTimePattern = "hh:mm tt";
            string shortDestination = flight.Destination.Length > 17 ? flight.Destination.Substring(0, 17)
                                                                     : flight.Destination;
            return String.Format($" │{flight.Time,22:g}  │  {shortDestination,-17}│   {flight.Number,-7:d4}" +
                                 $"│{flight.NumberOfPassengers,11} │  {flight.EconomyClassPrice,-10:c}│   {flight.Gate,-5}│  {flight.Status,-16}│\n");
        }

        // forms a string to display the passenger's info in a table
        string GetPassengerString(Passenger passenger)
        {
            DateTimeFormatInfo formatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
            formatInfo.ShortDatePattern = "yyyy/MM/dd";

            string fullName = $"{passenger.FirstName} {passenger.LastName}";
            string shortFullName = (fullName.Length > 20) ? fullName.Substring(0, 20) : fullName;
            string shortNationality = (passenger.Nationality.Length > 12) ? passenger.Nationality.Substring(0, 12) : passenger.Nationality;

            return String.Format($" │  {shortFullName,-20}│ {passenger.Sex,-8}│ {passenger.Birthday,-13:d}" +
                                 $"│ {shortNationality,-12}│ {passenger.Passport,-11}│   {passenger.Ticket.FlightNumber,-7:d4}│ {passenger.Ticket.Class,-10}│ {passenger.Ticket.Price.ToString("c"),-10}│\n");
        }

        string GetFlightTableHeader(FlightDirection direction)
        {
            StringBuilder sb = new StringBuilder();
            string title = (direction == FlightDirection.Arrival)
                   ? " │                                                   ARRIVALS                                                  │\n"
                   : " │                                                  DEPARTURES                                                 │\n";

            sb.Append(" ┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────┐\n");
            sb.Append(title);
            sb.Append(" ├────────────────────────┬───────────────────┬──────────┬────────────┬────────────┬────────┬──────────────────┤\n");
            sb.Append(" │          TIME          │    DESTINATION    │  FLIGHT  │ PASSENGERS │  EC PRICE  │  GATE  │      STATUS      │\n");
            sb.Append(" ├────────────────────────┼───────────────────┼──────────┼────────────┼────────────┼────────┼──────────────────┤\n");

            return sb.ToString();
        }

        string GetFlightTableFooter()
        {
            return " └────────────────────────┴───────────────────┴──────────┴────────────┴────────────┴────────┴──────────────────┘\n";
        }

        string GetPassengerTableHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" ┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────┐\n");
            sb.Append(" │                                                 PASSENGERS                                                  │\n");
            sb.Append(" ├──────────────────────┬─────────┬──────────────┬─────────────┬────────────┬──────────┬───────────┬───────────┤\n");
            sb.Append(" │         NAME         │   SEX   │   BIRTHDAY   │ NATIONALITY │  PASSPORT  │  FLIGHT  │   CLASS   │   PRICE   │\n");
            sb.Append(" ├──────────────────────┼─────────┼──────────────┼─────────────┼────────────┼──────────┼───────────┼───────────┤\n");

            return sb.ToString();
        }

        string GetPassengerTableFooter()
        {
            return " └──────────────────────┴─────────┴──────────────┴─────────────┴────────────┴──────────┴───────────┴───────────┘\n";
        }

        public void Print(IEnumerable<Flight> flights)
        {
            int numberOfArrivals = 0, numberOfDepartures = 0;
            // obtain the number of arrivals and departures
            foreach (Flight flight in flights)
            {
                if (flight.Direction == FlightDirection.Arrival)
                {
                    numberOfArrivals++;
                }
                else
                {
                    numberOfDepartures++;
                }
            }
            // sort the flights by datetime increase
            if (flights is List<Flight>)
            {
                ((List<Flight>)flights).Sort(new Flight.FlightComparerByTime());
            }

            // print info
            Console.WriteLine();
            if (numberOfArrivals != 0)
            {
                Console.Write(GetFlightTableHeader(FlightDirection.Arrival));

                foreach (var flight in flights)
                {
                    if (flight.Direction == FlightDirection.Arrival)
                    {
                        Console.Write(GetFlightString(flight));
                    }
                }
                Console.Write(GetFlightTableFooter());
            }

            if (numberOfDepartures != 0)
            {
                Console.Write(GetFlightTableHeader(FlightDirection.Departure));

                foreach (var flight in flights)
                {
                    if (flight.Direction == FlightDirection.Departure)
                    {
                        Console.Write(GetFlightString(flight));
                    }
                }
                Console.Write(GetFlightTableFooter());
            }
        }

        // display a single flight's info in a table
        public void Print(Flight flight)
        {
            Console.WriteLine();
            Console.Write(GetFlightTableHeader(flight.Direction));
            Console.Write(GetFlightString(flight));
            Console.Write(GetFlightTableFooter());
        }

        public void Print(IEnumerable<Passenger> passengers)
        {
            Console.Write(GetPassengerTableHeader());
            foreach (var passenger in passengers)
            {
                Console.Write(GetPassengerString(passenger));
            }
            Console.Write(GetPassengerTableFooter());
        }

        // display a single passenger's info in a table
        public void Print(Passenger passenger)
        {
            Console.Write(GetPassengerTableHeader());
            Console.Write(GetPassengerString(passenger));
            Console.Write(GetPassengerTableFooter());
        }
    }
}
