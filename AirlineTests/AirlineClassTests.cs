using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KRZHK.AirlineLibrary;
using System.Collections.Generic;
using KRZHK.AirlineLibrary.Enums;

namespace AirlineTests
{
    [TestClass]
    public class AirlineClassTests
    {
        Airline _airline;
        List<Passenger> _passengers1, _passengers2;
        Passenger _kurtCobain, _amyWinehouse, _mickJagger, _ringoStarr, _wrongPassenger;

        [TestInitialize]
        public void TestInitialize()
        {
            _airline = new Airline(3);
            _kurtCobain = new Passenger("Kurt", "Cobain", "American", "dd 333333", DateTime.Now, Sex.Male, new FlightTicket() { FlightNumber = 1, Class = TicketClass.Business, Price = 200 });
            _amyWinehouse = new Passenger("Amy", "Winehouse", "British", "mm 111111", DateTime.Now, Sex.Female, new FlightTicket() { FlightNumber = 1, Class = TicketClass.Economy, Price = 300 });
            _mickJagger = new Passenger("Mick", "Jagger", "British", "ee 444444", DateTime.Now, Sex.Male, new FlightTicket() { FlightNumber = 2, Class = TicketClass.Business, Price = 200 });
            _ringoStarr = new Passenger("Ringo", "Starr", "British", "ee 442233", DateTime.Now, Sex.Male, new FlightTicket() { FlightNumber = 2, Class = TicketClass.Business, Price = 200 });
            _wrongPassenger = new Passenger("Ro", "Sr", "British", "ee 443234", DateTime.Now, Sex.Male, new FlightTicket() { FlightNumber = 3, Class = TicketClass.Business, Price = 200 });

            _passengers1 = new List<Passenger>(); 
            _passengers1.Add(_kurtCobain);
            _passengers1.Add(_amyWinehouse);

            _passengers2 = new List<Passenger>();
            _passengers2.Add(_mickJagger);
            _passengers2.Add(_ringoStarr);

            _airline.AddFlight(new Flight(FlightDirection.Arrival, DateTime.Now, 1, "Lisbon", AirportGate.A3, FlightStatus.Canceled, 100, 5, _passengers1));
            _airline.AddFlight(new Flight(FlightDirection.Departure, DateTime.Now.AddHours(2.5), 2, "Buenos Aires", AirportGate.B2, FlightStatus.CheckIn, 300, 15, _passengers2));
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void AddFlight_Add1FlightToAirline_Count3()
        {
            // adjust

            // act
            _airline.AddFlight(new Flight(FlightDirection.Arrival, DateTime.Now, 1, "Lisbon", AirportGate.A3, FlightStatus.Canceled, 100, 5, _passengers1));
            // assert
            Assert.AreEqual(3, _airline.Flights.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddFlight_Add2FlightsToAirline_ArgumentException()
        {
            // act
            _airline.AddFlight(new Flight(FlightDirection.Arrival, DateTime.Now, 1, "Lisbon", AirportGate.A3, FlightStatus.Canceled, 100, 5, _passengers1));
            _airline.AddFlight(new Flight(FlightDirection.Arrival, DateTime.Now, 1, "Lisbon", AirportGate.A3, FlightStatus.Canceled, 100, 5, _passengers1));
        }

        [TestMethod]
        public void RemoveFlight_Remove1Flight_Count1()
        {
            _airline.RemoveFlight(new Flight(FlightDirection.Arrival, DateTime.Now, 1, "Lisbon", AirportGate.A3, FlightStatus.Canceled, 100, 5, _passengers1));

            Assert.AreEqual(1, _airline.Flights.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddPassenger_AddWrongPassenger_Exception()
        {
            // act
            _airline.Flights[0].AddPassenger(_wrongPassenger);
        }

        [TestMethod]
        public void RemovePassenger_Count1()
        {
            // act
            _airline.Flights[0].RemovePassenger(_kurtCobain);
            // assert
            Assert.AreEqual(1, _airline.Flights[0].Passengers.Count);
        }

        [TestMethod]
        public void UpdatePassengers_AllFlightNumbersChangedTo3()
        {
            // act
            _airline.Flights[0].UpdatePassengers(3);
            // assert
            foreach (var passenger in _airline.Flights[0].Passengers)
            {
                Assert.AreEqual(3, passenger.Ticket.FlightNumber);
            }
        }
    }
}
