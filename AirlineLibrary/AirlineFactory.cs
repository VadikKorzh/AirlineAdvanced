using KRZHK.AirlineLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineLibrary
{
    public class AirlineFactory
    {
        const decimal businessClassIncrement = 145;
        Random _random = new Random((int)DateTime.Now.Ticks);

        DateTime CreateRandomDate(int startYear, int stopYear)
        {
            if (startYear >= stopYear)
            {
                throw new ArgumentException("startYear must be less or equal to stopYear.");
            }
            else
            {
                DateTime startDate = new DateTime(startYear, 1, 1);
                return startDate.AddDays(_random.Next(0, (stopYear - startYear + 1) * 365));
            }
        }

        string CreateRandomPassportNumber()
        {
            StringBuilder passport = new StringBuilder();
            int temp;
            temp = _random.Next(26);
            passport.Append((char)('A' + temp));
            temp = _random.Next(26);
            passport.Append((char)('A' + temp));
            passport.Append(' ');
            passport.Append(String.Format("{0:d6}", _random.Next(999999)));
            return passport.ToString();
        }

        FlightTicket CreateRandomTicket(int flightNumber, decimal basePrice)
        {
            FlightTicket ticket = new FlightTicket();

            ticket.FlightNumber = flightNumber;
            int temp;
            temp = _random.Next(2);
            ticket.Class = (TicketClass)temp;
            // business class ticket price = economy class ticket price + "businessClassIncrement" (for example)
            ticket.Price = basePrice + businessClassIncrement * temp;
            
            return ticket;
        }

        public List<Passenger> CreateRandomPassengers(int maxNumberOfPassengers, int numberOfPassengers, int flightNumber, decimal economyClassPrice)
        {
            List<Passenger> passengers = new List<Passenger>();
            string[] firstNamesMale = { "John", "Quentin", "Brad", "Cristiano", "Lionel", "Vladimir", "Petro", "Barak" };
            string[] lastNamesMale = { "Travolta", "Tarantino", "Pitt", "Ronaldo", "Messi", "Putin", "Poroshenko", "Obama" };
            string[] firstNamesFemale = { "Angelina", "Jessica", "Angela", "Paris", "Mary", "Yuliya", "Hillary" };
            string[] lastNamesFemale = { "Jolie", "Alba", "Merkel", "Hilton", "Poppins", "Timoshenko", "Clinton" };
            string[] nationalities = { "American", "German", "Russian", "Brazilian", "Mexican", "Indian" };

            string firstName;
            string lastName;
            string nationality;
            string passport;
            DateTime birthday;
            Sex sex = Sex.Male;
            FlightTicket ticket;

            numberOfPassengers = numberOfPassengers > maxNumberOfPassengers ? maxNumberOfPassengers : numberOfPassengers;

            int temp = _random.Next(1, 6);

            for (int i = 0; i < numberOfPassengers; i++)
            {
                temp = _random.Next(2);
                if (temp == 0)
                {
                    sex = Sex.Male;

                    temp = _random.Next(firstNamesMale.Length);
                    firstName = firstNamesMale[temp];

                    temp = _random.Next(lastNamesMale.Length);
                    lastName = lastNamesMale[temp];
                }
                else
                {
                    sex = Sex.Female;

                    temp = _random.Next(firstNamesFemale.Length);
                    firstName = firstNamesFemale[temp];

                    temp = _random.Next(lastNamesFemale.Length);
                    lastName = lastNamesFemale[temp];
                }

                temp = _random.Next(nationalities.Length);
                nationality = nationalities[temp];

                passport = CreateRandomPassportNumber();

                ticket = CreateRandomTicket(flightNumber, economyClassPrice);

                temp = _random.Next(int.MaxValue);
                birthday = CreateRandomDate(1950, 1990);

                passengers.Add(new Passenger(firstName, lastName, nationality, passport, birthday, sex, ticket));
            }
            return passengers;
        }

        public Airline CreateAndPopulateAirline(int numberOfFlights)
        {
            Airline airline = new Airline(numberOfFlights);
            PopulateAirline(airline);
            return airline;
        }

        void PopulateAirline(Airline airline)
        {
            FlightDirection direction;
            DateTime time;
            int number;
            string destination;
            AirportGate gate;
            FlightStatus status;
            decimal economyClassPrice;
            int numberOfPassengersToCreate, maxNumberOfPassengers;
            List<Passenger> passengers;

            List<Flight> presetFlights = new List<Flight>();

            // Abu Dhabi
            direction = FlightDirection.Arrival;
            time = DateTime.Now.AddMinutes(100);
            number = 0298;
            destination = "Abu Dhabi";
            gate = AirportGate.A3;
            status = FlightStatus.InFlight;
            economyClassPrice = 430;
            maxNumberOfPassengers = 5;
            numberOfPassengersToCreate = 5;
            passengers = CreateRandomPassengers(maxNumberOfPassengers, numberOfPassengersToCreate, number, economyClassPrice);

            presetFlights.Add(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));

            // Helsinki
            direction = FlightDirection.Arrival;
            time = DateTime.Now.AddMinutes(200);
            number = 3599;
            destination = "Helsinki";
            gate = AirportGate.C1;
            status = FlightStatus.Unknown;
            economyClassPrice = 370;
            maxNumberOfPassengers = 15;
            numberOfPassengersToCreate = 7;
            passengers = CreateRandomPassengers(maxNumberOfPassengers, numberOfPassengersToCreate, number, economyClassPrice);

            presetFlights.Add(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));

            // Miami
            direction = FlightDirection.Departure;
            time = DateTime.Now.AddMinutes(40);
            number = 1888;
            destination = "Miami";
            gate = AirportGate.A1;
            status = FlightStatus.CheckIn;
            economyClassPrice = 515;
            maxNumberOfPassengers = 25;
            numberOfPassengersToCreate = 4;
            passengers = CreateRandomPassengers(maxNumberOfPassengers, numberOfPassengersToCreate, number, economyClassPrice);

            presetFlights.Add(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));

            // Sydney
            direction = FlightDirection.Departure;
            time = DateTime.Now.AddMinutes(5);
            number = 0011;
            destination = "Sydney";
            gate = AirportGate.B1;
            status = FlightStatus.GateClosed;
            economyClassPrice = 925;
            maxNumberOfPassengers = 15;
            numberOfPassengersToCreate = 5;
            passengers = CreateRandomPassengers(maxNumberOfPassengers, numberOfPassengersToCreate, number, economyClassPrice);

            presetFlights.Add(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));

            // Madrid
            direction = FlightDirection.Arrival;
            time = DateTime.Now.AddMinutes(30);
            number = 3002;
            destination = "Madrid";
            gate = AirportGate.C2;
            status = FlightStatus.Expected;
            economyClassPrice = 260;
            maxNumberOfPassengers = 10;
            numberOfPassengersToCreate = 4;
            passengers = CreateRandomPassengers(maxNumberOfPassengers, numberOfPassengersToCreate, number, economyClassPrice);

            presetFlights.Add(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));

            foreach (Flight flight in presetFlights)
            {
                if (!airline.IsFull())
                {
                    airline.AddFlight(flight);
                    continue;
                }
                break;
            }
        }
    }
}
