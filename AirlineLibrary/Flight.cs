using KRZHK.AirlineLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineLibrary
{
    public class Flight
    {
        public readonly int MaxNumberOfPassengers = 20;

        public FlightDirection Direction { get; private set; }
        public DateTime Time { get; private set; }
        public int Number { get; private set; }
        public string Destination { get; private set; }
        public AirportGate Gate { get; private set; }
        public FlightStatus Status { get; private set; }
        public decimal EconomyClassPrice { get; private set; }

        public List<Passenger> Passengers { get; private set; }

        public int NumberOfPassengers { get { return Passengers.Count; } }

        #region Constructors

        public Flight(FlightDirection direction, DateTime time, int number, string destination, AirportGate gate,
                       FlightStatus status, decimal economyClassPrice, int maxNumberOfPassengers, IEnumerable<Passenger> passengers)
        {
            Direction = direction;
            Time = time;
            Number = number;
            Destination = destination;
            Gate = gate;
            Status = status;
            EconomyClassPrice = economyClassPrice;
            MaxNumberOfPassengers = maxNumberOfPassengers;
            Passengers = new List<Passenger>(maxNumberOfPassengers);
            foreach (var passenger in passengers)
            {
                AddPassenger(passenger);
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder resultString = new StringBuilder();
            resultString.Append($"[ Direction: {Direction},");
            resultString.Append($" Flight time: {Time:g},");
            resultString.Append($" Flight number: {Number:d4},");
            resultString.Append($" Destination: {Destination},");
            resultString.Append($" Gate: {Gate},");
            resultString.Append($" Flight status: {Status},");
            resultString.Append($" Passengers: {Passengers.Count} ]");
            return resultString.ToString();
        }

        public bool IsFull()
        {
            return Passengers.Count == MaxNumberOfPassengers;
        }

        //public Flight Clone()
        //{
        //    return new Flight(Direction, Time, Number, Destination, Gate, Status, EconomyClassPrice, MaxNumberOfPassengers, Passengers);
        //}

        public void AddPassenger(Passenger passengerToAdd)
        {
            if (!IsFull())
            {
                if (passengerToAdd.Ticket.FlightNumber == Number)
                {
                    Passengers.Add(passengerToAdd);
                }
                else
                {
                    throw new ArgumentException("The passenger's ticket is not for this flight.");
                }
            }
            else
            {
                throw new ArgumentException("The list of passengers is full. Remove some to add another one.");
            }
        }

        public void RemovePassenger(Passenger passengerToRemove)
        {
            foreach (var passenger in Passengers)
            {
                if (passenger.Passport.Equals(passengerToRemove.Passport))
                {
                    Passengers.Remove(passengerToRemove);
                }
            }
        }

        // compares flights by time
        public class FlightComparerByTime : IComparer<Flight>
        {
            public int Compare(Flight x, Flight y)
            {
                return x.Time.CompareTo(y.Time);
            }
        }
    }
}
