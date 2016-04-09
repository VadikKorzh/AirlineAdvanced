using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineLibrary
{
    public enum TicketClass
    {
        Economy,
        Business
    }

    public struct FlightTicket
    {
        public TicketClass Class { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            StringBuilder resultString = new StringBuilder();
            resultString.Append($"     Class: {Class}\n");
            resultString.Append($"     Price: {Price.ToString("c")}");
            return resultString.ToString();
        }
    }
}
