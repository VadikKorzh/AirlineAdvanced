using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineLibrary;

namespace AirlineManager.AirlineManagers
{
    interface IAirlineManager
    {
        void Manage(Airline airline);

        void AddFlight(Airline airline);
        void EditFlight(Airline airline);
        void RemoveFlight(Airline airline);

        void FindPassengersByName(Flight[] flights);
        void FindPassengersByPassportNumber(Flight[] flights);
        void FindFlightsByEconomyClassPrice(Flight[] flights);
    }
}
