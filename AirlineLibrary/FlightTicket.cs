using KRZHK.AirlineLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineLibrary
{
    public struct FlightTicket
    {
        public int FlightNumber { get; set; }
        public TicketClass Class { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"[ Flight: {FlightNumber}, Class: {Class}, Price: {Price.ToString("c")} ]";
        }
    }
}
