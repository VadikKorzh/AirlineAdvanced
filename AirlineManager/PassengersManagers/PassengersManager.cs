using KRZHK.AirlineLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineManager.PassengersManagers
{
    abstract class PassengersManager
    {
        protected Airline _airline;

        protected Passenger GetPassengerByPassportNumber(string passportNumber)
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

        protected bool IsValidPassportNumber(string passportNumber)
        {
            bool isValid = true;

            if (passportNumber.Length != 9)
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < passportNumber.Length && isValid; i++)
                {
                    if (i == 0 || i == 1)
                    {
                        isValid = Char.IsLetter(passportNumber[i]);
                    }
                    else if (i == 2)
                    {
                        isValid = (passportNumber[i] == ' ');
                    }
                    else
                    {
                        isValid = Char.IsDigit(passportNumber[i]);
                    }
                }
            }
            return isValid;
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
