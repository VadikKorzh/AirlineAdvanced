using KRZHK.AirlineLibrary;
using KRZHK.AirlineManager.PassengersManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineManager.FlightsManagers
{
    abstract class FlightsManager
    {
        protected Airline _airline;
        protected PassengersManager _passengersManager;

        abstract public void FindFlightsByEconomyClassPrice();

        abstract public void AddFlight();
        abstract public void EditFlight();
        abstract public void RemoveFlight();
    }
}
