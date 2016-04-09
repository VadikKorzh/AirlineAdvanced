using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineLibrary
{
    public class Airline
    {
        int _maxNumberOfFlights;
        int _numberOfFlights = 0;
        Flight[] _flights;

        public Flight[] Flights
        {
            get
            {
                return CloneFlights();
            }
            private set
            {
                _flights = value;
            }
        }

        public int NumberOfFlights
        {
            get { return _numberOfFlights; }
        }

        public Airline(int maxNumberOfFlights)
        {
            this._maxNumberOfFlights = maxNumberOfFlights;
            Flights = new Flight[this._maxNumberOfFlights];
        }

        public bool IsFull()
        {
            return _numberOfFlights == _maxNumberOfFlights;
        }

        Flight[] CloneFlights()
        {
            Flight[] tempFlights = new Flight[_flights.Length];

            for (int i = 0; i < _flights.Length; i++)
            {
                tempFlights[i] = (_flights[i] != null) ? _flights[i].Clone() : null;
            }

            return tempFlights;
        }

        public void AddFlight(Flight flight)
        {
            int i;
            if (_numberOfFlights < _maxNumberOfFlights)
            {
                for (i = 0; i < _maxNumberOfFlights; i++)
                {
                    if (_flights[i] == null)
                    {
                        break;
                    }
                }
                _flights[i] = flight.Clone();
                _numberOfFlights++;
            }
            else
            {
                throw new ArgumentException("The list of flights is full. Remove some to add another one.");
            }
        }

        public void RemoveFlight(Flight flight)
        {
            for (int i = 0; i < _maxNumberOfFlights; i++)
            {
                if (_flights[i] != null && _flights[i].Number == flight.Number)
                {
                    _flights[i] = null;
                    _numberOfFlights--;
                    break;
                }
            }
        }

        public void AddPassenger(Passenger passengerToAdd, int flightNumber)
        {
            for (int i = 0; i < _maxNumberOfFlights; i++)
            {
                if (_flights[i] != null && _flights[i].Number == flightNumber)
                {
                    try
                    {
                        _flights[i].AddPassenger(passengerToAdd);
                    }
                    catch (ArgumentException)
                    {
                        throw;
                    }
                    break;
                }
            }
        }

        public void RemovePassenger(Passenger passengerToRemove)
        {
            for (int i = 0; i < _maxNumberOfFlights; i++)
            {
                if (_flights[i] != null)
                {
                    for (int j = 0; j < _flights[i].Passengers.Length; j++)
                    {
                        if (_flights[i].Passengers[j] != null && _flights[i].Passengers[j].Passport == passengerToRemove.Passport.ToUpper())
                        {
                            _flights[i].RemovePassenger(passengerToRemove);
                        }
                    }
                }
            }
        }
    }
}
