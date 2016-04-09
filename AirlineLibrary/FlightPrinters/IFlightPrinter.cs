using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineLibrary.FlightPrinters
{
    public interface IFlightPrinter
    {
        void PrintFlightsInfo(Flight[] flights);
        void PrintFlightInfo(Flight flight);
        void PrintPassengersInfo(Passenger[] passengers);
        void PrintPassengerInfo(Passenger passenger);
    }
}
