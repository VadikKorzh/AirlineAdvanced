using KRZHK.AirlineLibrary;
using KRZHK.AirlineManager.PassengerManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineManager.FlightManagers
{
    abstract class FlightManager
    {
        protected Airline _airline;
        protected PassengerManager _passengersManager;

        abstract public void FindFlightsByEconomyClassPrice();

        abstract public void AddFlight();
        abstract public void EditFlight();
        abstract public void RemoveFlight();
    }
}
