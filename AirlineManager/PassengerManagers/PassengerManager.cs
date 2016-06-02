using KRZHK.AirlineLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KRZHK.AirlineManager.PassengerManagers
{
    abstract class PassengerManager
    {
        protected Airline _airline;

        protected virtual Passenger GetPassengerByPassportNumber(string passportNumber)
        {
            List<Flight> flights = _airline.Flights;
            Passenger soughtforPassenger;
            
            foreach (Flight flight in flights)
            {
                soughtforPassenger = flight.Passengers.FirstOrDefault(p => p.Passport.Equals(passportNumber.Trim().ToUpper()));
                if(soughtforPassenger != null)
                {
                    return soughtforPassenger;
                }
            }
            return null;
        }

        protected virtual bool IsValidPassportNumber(string passportNumber)
        {
            Regex passportRegex = new Regex(@"^\w{2}\s\d{6}$");
            return passportRegex.Match(passportNumber).Success;
        }

        abstract public Passenger CreateNewPassenger(int flightNumber, decimal ecTicketPrice);

        abstract public void ViewAllPassengersInfo();
        abstract public void ViewFlightPassengers();

        abstract public void FindPassengersByName();
        abstract public void FindPassengersByPassportNumber();

        abstract public void AddPassenger();
        abstract public void EditPassenger();
        abstract public void RemovePassenger();
    }
}
