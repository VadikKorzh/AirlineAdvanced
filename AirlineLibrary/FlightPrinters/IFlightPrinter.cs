using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineLibrary.FlightPrinters
{
    public interface IFlightPrinter
    {
        void Print(IEnumerable<Flight> flights);
        void Print(Flight flight);
        void Print(IEnumerable<Passenger> passengers);
        void Print(Passenger passenger);
    }
}
