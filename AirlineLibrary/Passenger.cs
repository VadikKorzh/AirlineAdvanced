using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineLibrary
{
    public enum Sex
    {
        Male,
        Female
    }

    public class Passenger
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Nationality { get; private set; }
        public string Passport { get; private set; }
        public DateTime Birthday { get; private set; }
        public Sex Sex { get; private set; }
        public FlightTicket Ticket { get; private set; }
        public int FlightNumber { get; private set; }

        public Passenger() {}

        public Passenger(string firstName, string lastName, string nationality, string passport, DateTime birthday,
                         Sex sex, FlightTicket ticket, int flightNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
            Passport = passport;
            Birthday = birthday;
            Sex = sex;
            Ticket = ticket;
            FlightNumber = flightNumber;
        }

        public override string ToString()
        {
            StringBuilder resultString = new StringBuilder();
            resultString.Append($"   Name: {FirstName} {LastName}\n");
            resultString.Append($"   Sex: {Sex}\n");
            resultString.Append($"   Birthday: {Birthday.ToString("d")}\n");
            resultString.Append($"   Nationality: {Nationality}\n");
            resultString.Append($"   Passport: {Passport}\n");
            resultString.Append($"   Ticket:\n{Ticket}\n");
            return resultString.ToString();
        }

        public Passenger Clone()
        {
            return new Passenger(FirstName, LastName, Nationality, Passport, Birthday, Sex, Ticket, FlightNumber);
        }
    }
}
