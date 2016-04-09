using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineLibrary
{
    public enum FlightDirection
    {
        Arrival,
        Departure
    }

    public enum AirportGate
    {
        A1,
        A2,
        A3,
        B1,
        B2,
        C1,
        C2
    }

    public enum FlightStatus
    {
        CheckIn,
        GateClosed,
        Arrived,
        Departed,
        Canceled,
        Expected,
        Delayed,
        Unknown,
        InFlight
    }

    public class Flight
    {
        Passenger[] _passengers;
        public readonly int MaxNumberOfPassengers = 20;
        int _numberOfPassengers = 0;

        public FlightDirection Direction { get; private set; }
        public DateTime Time { get; private set; }
        public int Number { get; private set; }
        public string Destination { get; private set; }
        public AirportGate Gate { get; private set; }
        public FlightStatus Status { get; private set; }
        public decimal EconomyClassPrice { get; private set; }
        public Passenger[] Passengers
        {
            get
            {
                return ClonePassengers();
            }
            private set
            {
                _passengers = value;
            }
        }

        public int NumberOfPassengers { get { return _numberOfPassengers; } }

        #region Constructors

        public Flight() { }

        public Flight(FlightDirection direction, DateTime time, int number, string destination, AirportGate gate,
                       FlightStatus status, decimal economyClassPrice, int maxNumberOfPassengers, Passenger[] passengers)
        {
            Direction = direction;
            Time = time;
            Number = number;
            Destination = destination;
            Gate = gate;
            Status = status;
            EconomyClassPrice = economyClassPrice;
            MaxNumberOfPassengers = maxNumberOfPassengers;
            Passengers = new Passenger[maxNumberOfPassengers];
            foreach (var passenger in passengers)
            {
                if (passenger != null)
                {
                    AddPassenger(passenger);
                }
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder resultString = new StringBuilder();
            resultString.Append($" Direction: {Direction}\n");
            resultString.Append($" Flight time: {Time:g}\n");
            resultString.Append($" Flight number: {Number:d4}\n");
            resultString.Append($" Destination: {Destination}\n");
            resultString.Append($" Gate: {Gate}\n");
            resultString.Append($" Flight status: {Status}\n");
            resultString.Append($" Passengers: {Passengers.Count(o => o != null)}\n\n");
            foreach (var passenger in Passengers)
            {
                resultString.Append($"{passenger}\n");
            }
            return resultString.ToString();
        }

        public bool IsFull()
        {
            return _numberOfPassengers == MaxNumberOfPassengers;
        }

        public Flight Clone()
        {
            return new Flight(Direction, Time, Number, Destination, Gate, Status, EconomyClassPrice, MaxNumberOfPassengers, Passengers);
        }

        Passenger[] ClonePassengers()
        {
            Passenger[] tempPassengers = new Passenger[_passengers.Length];

            for (int i = 0; i < _passengers.Length; i++)
            {
                tempPassengers[i] = (_passengers[i] != null) ? _passengers[i].Clone() : null;
            }

            return tempPassengers;
        }

        public void AddPassenger(Passenger passengerToAdd)
        {
            if (!IsFull())
            {
                for (int i = 0; i < _passengers.Length; i++)
                {
                    if (_passengers[i] == null)
                    {
                        _passengers[i] = passengerToAdd.Clone();
                        _numberOfPassengers++;
                        break;
                    }
                }
            }
            else
            {
                throw new ArgumentException("The list of passengers is full. Remove some to add another one.");
            }
        }

        public void RemovePassenger(Passenger passengerToRemove)
        {
            for (int i = 0; i < _passengers.Length; i++)
            {
                if (_passengers[i] != null && _passengers[i].Passport.Equals(passengerToRemove.Passport))
                {
                    _passengers[i] = null;
                    _numberOfPassengers--;
                }
            }
        }
    }
}
