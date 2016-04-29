using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineLibrary
{
    public class Airline : IEnumerable<Flight>
    {
        int _maxNumberOfFlights;

        public List<Flight> Flights { get; private set; }

        public Airline(int maxNumberOfFlights = 100)
        {
            this._maxNumberOfFlights = maxNumberOfFlights;
            Flights = new List<Flight>();
        }

        public bool IsFull()
        {
            return Flights.Count == _maxNumberOfFlights;
        }

        //Flight[] CloneFlights()
        //{
        //    Flight[] tempFlights = new Flight[_flights.Length];

        //    for (int i = 0; i < _flights.Length; i++)
        //    {
        //        tempFlights[i] = (_flights[i] != null) ? _flights[i].Clone() : null;
        //    }

        //    return tempFlights;
        //}

        public void AddFlight(Flight flight)
        {
            if (!IsFull())
            {
                Flights.Add(flight);
            }
            else
            {
                throw new ArgumentException("The list of flights is full. Remove some to add another one.");
            }
        }

        public void RemoveFlight(Flight flightToRemove)
        {
            Flight foundFlightToRemove = Flights.Find((f) => f.Number == flightToRemove.Number);
            if (foundFlightToRemove != null)
            {
                Flights.Remove(foundFlightToRemove);
            }
            else
            {
                throw new ArgumentException("The airline doesn't contain the specified flight.");
            }
        }

        public void AddPassenger(Passenger passengerToAdd, int flightNumber)
        {
            Flight flightToEdit = Flights.Find((f) => f.Number == flightNumber);

            if (flightToEdit != null)
            {
                try
                {
                    flightToEdit.AddPassenger(passengerToAdd);
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentException("The airline doesn't contain the specified flight.");
            }
        }

        public void RemovePassenger(Passenger passengerToRemove)
        {
            Passenger foundPassengerToRemove = null;
            foreach (Flight flight in Flights)
            {
                foundPassengerToRemove = flight.Passengers.Find((p) => p.Passport.Equals(passengerToRemove.Passport));
                if (foundPassengerToRemove != null)
                {
                    flight.RemovePassenger(foundPassengerToRemove);
                    break;
                }
            }
            if (foundPassengerToRemove == null)
            {
                throw new ArgumentException("No such passenger has been found in the airline.");
            }
        }

        public IEnumerator<Flight> GetEnumerator()
        {
            return ((IEnumerable<Flight>)Flights).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Flight>)Flights).GetEnumerator();
        }
    }
}
