using KRZHK.AirlineLibrary;
using KRZHK.AirlineLibrary.Enums;
using KRZHK.AirlineLibrary.FlightPrinters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineManager.PassengersManagers
{
    class ConsolePassengersManager : PassengersManager
    {
        IFlightPrinter _flightPrinter;

        public ConsolePassengersManager(Airline airline, IFlightPrinter printer)
        {
            _airline = airline;
            _flightPrinter = printer;
        }

        #region Output

        // view the information of all the passengers in one table
        public override void ViewAllPassengersInfo()
        {
            List<Passenger> allPassengers = new List<Passenger>();
            foreach (var flight in _airline)
            {
                foreach (var passenger in flight.Passengers)
                {
                    allPassengers.Add(passenger);
                }
            }
            _flightPrinter.Print(allPassengers);
        }

        // view the full information about the flights and the passengers
        public override void ViewFlightPassengers()
        {
            bool isOkParse, isFound, isCorrectNumber;
            int flightNumber;
            Console.Write($"\n There are {_airline.Flights.Count} flights: ");
            foreach (Flight flight in _airline)
            {
                Console.Write($"{flight.Number:d4}, ");
            }
            Console.Write("\b\b.");

            Console.WriteLine("\n Enter a flight number (enter 0 to view all the passengers of the airline):");
            do
            {
                isCorrectNumber = true;
                Console.Write(' ');
                isOkParse = int.TryParse(Console.ReadLine(), out flightNumber);
                if (isOkParse && (flightNumber < 0 || flightNumber > 9999))
                {
                    Console.WriteLine(" The number must be within [1, 9999]. Try again.");
                    isCorrectNumber = false;
                }
                else if (!isOkParse)
                {
                    Console.WriteLine(" Incorrect input, try again.");
                }
            } while (!isOkParse || !isCorrectNumber);

            if (flightNumber == 0)
            {
                foreach (var flight in _airline)
                {
                    _flightPrinter.Print(flight);
                    _flightPrinter.Print(flight.Passengers);
                    Console.WriteLine(" " + (new StringBuilder()).Append('-', Console.WindowWidth - 2));
                }
            }
            else
            {
                isFound = false;
                foreach (Flight flight in _airline)
                {
                    if (flight.Number == flightNumber)
                    {
                        isFound = true;
                        Console.WriteLine(" The flight has been found, press any key to show the information.");
                        Console.Write(' ');
                        Console.ReadKey();
                        _flightPrinter.Print(flight);
                        _flightPrinter.Print(flight.Passengers);
                        break;
                    }
                }
                if (!isFound)
                {
                    Console.WriteLine(" The flight hasn't been found.");
                }
            }
        }

        #endregion

        #region Passenger search

        //public void FindPassengers()
        //{
        //    List<Passenger> properPassengers = new List<Passenger>();
        //    List<Flight> flights = _airline.Flights;

        //    ConsoleKey keyPressed;
        //    string fullName, nameToSeek;
        //    bool isFirstMatch, isFound;

        //    Console.WriteLine(" Find passengers by");
        //    Console.WriteLine(" 1 - name");
        //    Console.WriteLine(" 2 - passport number");
        //    Console.Write(" ");

        //    do
        //    {
        //        Console.WriteLine("\n Enter a name to seek:");
        //        Console.Write(" ");
        //        nameToSeek = Console.ReadLine().Trim().ToUpper();
        //        if (nameToSeek == "")
        //        {
        //            isFound = false;
        //        }
        //        else
        //        {
        //            isFirstMatch = true;
        //            isFound = false;
        //            foreach (var flight in flights)
        //            {
        //                if (flight.Passengers != null)
        //                {
        //                    foreach (var passenger in flight.Passengers)
        //                    {
        //                        fullName = $"{passenger.FirstName} {passenger.LastName}";
        //                        if (fullName.ToUpper().Contains(nameToSeek))
        //                        {
        //                            if (isFirstMatch)
        //                            {
        //                                isFirstMatch = false;
        //                                isFound = true;
        //                            }
        //                            properPassengers.Add(passenger);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        if (isFound)
        //        {
        //            _flightPrinter.Print(properPassengers);
        //        }
        //        else
        //        {
        //            Console.WriteLine(" No matches have been found.");
        //        }
        //        Console.WriteLine("\n Press ESC to quit or any other key to search again.");
        //        Console.Write(" ");
        //        keyPressed = Console.ReadKey().Key;
        //        if (keyPressed != ConsoleKey.Enter)
        //        {
        //            Console.WriteLine();
        //        }
        //    } while (keyPressed != ConsoleKey.Escape);
        //}

        public override void FindPassengersByName()
        {
            List<Passenger> properPassengers = new List<Passenger>();
            List<Flight> flights = _airline.Flights;

            ConsoleKey keyPressed;
            string fullName, nameToSeek;
            bool isFirstMatch, isFound;

            do
            {
                properPassengers.Clear();
                Console.WriteLine("\n Enter a name to seek:");
                Console.Write(" ");
                nameToSeek = Console.ReadLine().Trim().ToUpper();
                if (nameToSeek == "")
                {
                    isFound = false;
                }
                else
                {
                    isFirstMatch = true;
                    isFound = false;
                    foreach (var flight in flights)
                    {
                        if (flight.Passengers != null)
                        {
                            foreach (var passenger in flight.Passengers)
                            {
                                fullName = $"{passenger.FirstName} {passenger.LastName}";
                                if (fullName.ToUpper().Contains(nameToSeek))
                                {
                                    if (isFirstMatch)
                                    {
                                        isFirstMatch = false;
                                        isFound = true;
                                    }
                                    properPassengers.Add(passenger);
                                }
                            }
                        }
                    }
                }
                if (isFound)
                {
                    _flightPrinter.Print(properPassengers);
                }
                else
                {
                    Console.WriteLine(" No matches have been found.");
                }
                Console.WriteLine("\n Press ESC to quit or any other key to search again.");
                Console.Write(" ");
                keyPressed = Console.ReadKey().Key;
                if (keyPressed != ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
            } while (keyPressed != ConsoleKey.Escape);
        }

        public override void FindPassengersByPassportNumber()
        {
            List<Flight> flights = _airline.Flights;
            List<Passenger> properPassengers = new List<Passenger>();

            ConsoleKey keyPressed;
            string passportNumberToSeek;
            bool isFirstMatch, isFound;

            do
            {
                properPassengers.Clear();
                Console.WriteLine("\n Enter a passport number (or a part) to seek:");
                Console.Write(" ");
                passportNumberToSeek = Console.ReadLine().Trim().ToUpper();
                if (passportNumberToSeek == "")
                {
                    isFound = false;
                }
                else
                {
                    isFound = false;
                    isFirstMatch = true;
                    foreach (var flight in flights)
                    {
                        if (flight.Passengers != null)
                        {
                            foreach (var passenger in flight.Passengers)
                            {
                                if (passenger.Passport.Contains(passportNumberToSeek))
                                {
                                    if (isFirstMatch)
                                    {
                                        isFirstMatch = false;
                                        isFound = true;
                                    }
                                    properPassengers.Add(passenger);
                                }
                            }
                        }
                    }
                }
                if (isFound)
                {
                    _flightPrinter.Print(properPassengers);
                }
                else
                {
                    Console.WriteLine(" No matches have been found.");
                }
                Console.WriteLine("\n Press ESC to quit or any other key to search again.");
                Console.Write(" ");
                keyPressed = Console.ReadKey().Key;
                if (keyPressed != ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
            } while (keyPressed != ConsoleKey.Escape);
        }
       
        #endregion

        #region Add, remove, edit passenger

        public override Passenger CreateNewPassenger(int flightNumber, decimal ecTicketPrice)
        {
            string firstName;
            string lastName;
            string nationality;
            string passport;
            DateTime birthday;
            Sex sex = Sex.Male;
            TicketClass ticketClass = TicketClass.Business;
            decimal ticketPrice;
            FlightTicket ticket;

            bool isOkParse, isOkInput;
            int tempInt;

            Console.WriteLine($"\n\n === ADDING OF A NEW PASSENGER TO THE FLIGHT {flightNumber} ===");

            #region First name

            Console.WriteLine("\n --- FIRST NAME ---");
            Console.WriteLine("\n Enter the first name of the passenger:");
            do
            {
                Console.Write(" ");
                isOkInput = true;
                firstName = Console.ReadLine();
                if (firstName.Length == 0)
                {
                    Console.WriteLine(" You've entered an empty string. Try again.");
                    isOkInput = false;
                }
            } while (!isOkInput);
            Console.WriteLine($" The first name \"{firstName}\" has been saved.");

            #endregion

            #region Last name

            Console.WriteLine("\n --- LAST NAME ---");
            Console.WriteLine("\n Enter the last name of the passenger:");
            do
            {
                Console.Write(" ");
                isOkInput = true;
                lastName = Console.ReadLine();
                if (lastName.Length == 0)
                {
                    Console.WriteLine(" You've entered an empty string. Try again.");
                    isOkInput = false;
                }
            } while (!isOkInput);
            Console.WriteLine($" The last name \"{lastName}\" has been saved.");

            #endregion

            #region Nationality

            Console.WriteLine("\n --- NATIONALITY ---");
            Console.WriteLine("\n Enter the nationality of the passenger:");
            do
            {
                Console.Write(" ");
                isOkInput = true;
                nationality = Console.ReadLine();
                if (nationality.Length == 0)
                {
                    Console.WriteLine(" You've entered an empty string. Try again.");
                    isOkInput = false;
                }
            } while (!isOkInput);
            Console.WriteLine($" The nationality \"{nationality}\" has been saved.");

            #endregion

            #region Passport

            Console.WriteLine("\n --- PASSPORT ---");
            Console.WriteLine("\n Enter the passport number of the passenger (format: \"LL DDDDDD\", L - letter, D - digit):");
            do
            {
                Console.Write(" ");
                isOkInput = true;
                passport = Console.ReadLine().ToUpper();
                if (passport.Length == 0)
                {
                    Console.WriteLine(" You've entered an empty string. Try again.");
                    isOkInput = false;
                }
                else if (!IsValidPassportNumber(passport))
                {
                    Console.WriteLine(" Invalid format. Try again.");
                    isOkInput = false;
                }
            } while (!isOkInput);
            Console.WriteLine($" The passport number \"{passport}\" has been saved.");

            #endregion

            #region Birthday

            Console.WriteLine("\n --- BIRTHDAY ---");
            Console.WriteLine("\n Enter the birthday of the passenger (recommended format: MM/dd/yyyy):");
            do
            {
                isOkInput = true;
                Console.Write(" ");
                isOkParse = DateTime.TryParse(Console.ReadLine(), out birthday);
                if (!isOkParse)
                {
                    Console.WriteLine(" Invalid date/time format, try again.");
                }
                else
                {
                    Console.WriteLine($" The date you've entered is {birthday.ToString("d")}.");
                    Console.WriteLine($" Save this value (press ENTER) or try to enter another (press any other key)?");
                    Console.Write(" ");
                    if (Console.ReadKey().Key != ConsoleKey.Enter)
                    {
                        isOkInput = false;
                        Console.WriteLine();
                    }
                }
            } while (!isOkParse || !isOkInput);
            Console.WriteLine($"\n The birthday \"{birthday.ToString("d")}\" has been saved.");

            #endregion

            #region Sex
            Console.WriteLine("\n --- SEX ---");
            Console.WriteLine("\n Enter the sex of the passenger (1 - Male, 2 - Female):");
            do
            {
                isOkInput = true;
                Console.Write(" ");

                isOkParse = int.TryParse(Console.ReadLine(), out tempInt);
                if (isOkParse)
                {
                    switch (tempInt)
                    {
                        case 1:
                        case 2:
                            sex = (Sex)(tempInt - 1);
                            break;
                        default:
                            Console.WriteLine(" Only 1 or 2, try again.");
                            isOkInput = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(" Invalid input, try again.");
                }
            } while (!isOkParse || !isOkInput);
            Console.WriteLine($" The sex \"{sex}\" has been saved.");
            #endregion

            #region Ticket class
            Console.WriteLine("\n --- TICKET CLASS ---");
            Console.WriteLine("\n Enter the ticket class of the passenger (1 - Economy, 2 - Business):");
            do
            {
                isOkInput = true;
                Console.Write(" ");

                isOkParse = int.TryParse(Console.ReadLine(), out tempInt);
                if (isOkParse)
                {
                    switch (tempInt)
                    {
                        case 1:
                        case 2:
                            ticketClass = (TicketClass)(tempInt - 1);
                            break;
                        default:
                            Console.WriteLine(" Only 1 or 2, try again.");
                            isOkInput = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(" Invalid input, try again.");
                }
            } while (!isOkParse || !isOkInput);
            Console.WriteLine($" The ticket class \"{ticketClass}\" has been saved.");
            #endregion


            decimal businessClassIncrement;
            if (!decimal.TryParse(ConfigurationManager.AppSettings["businessClassIncrement"], out businessClassIncrement))
            {
                businessClassIncrement = 200;
            }

            ticketPrice = ecTicketPrice + businessClassIncrement * (int)ticketClass;
            ticket = new FlightTicket { Class = ticketClass, Price = ticketPrice, FlightNumber = flightNumber };

            return new Passenger(firstName, lastName, nationality, passport, birthday, sex,
                                                               ticket);
        }

        public override void AddPassenger()
        {
            bool isOkParse, isCorrectNumber, isFound;
            List<Flight> flights = _airline.Flights;
            int flightNumber;
            string fullOrNot;
            Console.Write($"\n There are {flights.Count} flights: ");
            foreach (var flight in flights)
            {
                fullOrNot = (flight.IsFull()) ? " (full)" : "";
                Console.Write($"{flight.Number:d4}{fullOrNot}, ");
            }
            Console.Write("\b\b.");

            Console.WriteLine("\n Enter a flight number:");
            do
            {
                isCorrectNumber = true;
                Console.Write(' ');
                isOkParse = int.TryParse(Console.ReadLine(), out flightNumber);
                if (isOkParse && (flightNumber < 0 || flightNumber > 9999))
                {
                    Console.WriteLine(" The number must be within [1, 9999]. Try again.");
                    isCorrectNumber = false;
                }
                else if (!isOkParse)
                {
                    Console.WriteLine(" Incorrect input, try again.");
                }
            } while (!isOkParse || !isCorrectNumber);

            isFound = false;
            foreach (Flight flight in flights)
            {
                if (flight != null && flight.Number == flightNumber)
                {
                    isFound = true;
                    Console.WriteLine(" The flight has been found, press any key to show the information.");
                    Console.Write(' ');
                    Console.ReadKey();
                    _flightPrinter.Print(flight);
                    _flightPrinter.Print(flight.Passengers);
                    if (flight.IsFull())
                    {
                        Console.WriteLine("\n The flight is full. Remove some passengers to add another one.");
                    }
                    else {
                        int index = flight.NumberOfPassengers;
                        bool isFull, isEnough;
                        do
                        {
                            isFull = isEnough = false;
                            _airline.AddPassenger(CreateNewPassenger(flightNumber, flight.EconomyClassPrice), flightNumber);
                            index++;
                            Console.WriteLine("\n The passenger has been successfully added to the flight.");

                            if (index < flight.MaxNumberOfPassengers)
                            {
                                Console.WriteLine("\n Press ENTER to add new passenger or any other key to stop adding.");
                                Console.Write(" ");
                                if (Console.ReadKey().Key != ConsoleKey.Enter)
                                {
                                    isEnough = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n The flight is full.");
                                isFull = true;
                            }

                        } while (!isFull && !isEnough);
                    }
                    break;
                }
            }
            if (!isFound)
            {
                Console.WriteLine(" The flight hasn't been found.");
            }
        }

        public override void EditPassenger()
        {
            string firstName;
            string lastName;
            string nationality;
            string passport;
            DateTime birthday;
            Sex sex = Sex.Male;
            TicketClass ticketClass = TicketClass.Business;
            decimal ticketPrice;
            FlightTicket ticket;

            bool isOkParse, isCorrectNumber, isOkInput, isFound;
            string passportToEdit;
            Passenger passengerToEdit = null;
            Flight flightOfThePassenger = null;

            Console.WriteLine("\n All the passengers: \n");
            ViewAllPassengersInfo();

            Console.WriteLine("\n Enter the passport number of the passenger you want to edit:");
            do
            {
                isCorrectNumber = true;
                Console.Write(' ');

                passportToEdit = Console.ReadLine();
                if (!IsValidPassportNumber(passportToEdit))
                {
                    Console.WriteLine(" Invalid format. Try again.");
                    isCorrectNumber = false;
                }
            } while (!isCorrectNumber);
            // search for a passenger by the passport number
            isFound = false;
            foreach (Flight flight in _airline.Flights)
            {
                if (flight != null)
                {
                    foreach (Passenger passenger in flight.Passengers)
                    {
                        if (passenger != null && passenger.Passport == passportToEdit.ToUpper())
                        {
                            isFound = true;
                            passengerToEdit = passenger;
                            break;
                        }
                    }
                    if (isFound)
                    {
                        flightOfThePassenger = flight;
                        break;
                    }
                }
            }

            if (!isFound)
            {
                Console.WriteLine(" The flight hasn't been found.");
            }
            else {
                Console.WriteLine(" The passenger has been found, press any key to edit the information.");

                Console.WriteLine($"\n\n ===  EDITING THE PASSENGER (PASSPORT {passportToEdit.ToUpper()}) ===");

                #region First name

                Console.WriteLine("\n --- FIRST NAME ---");
                Console.WriteLine($"\n Current first name: {passengerToEdit.FirstName}");
                Console.WriteLine("\n Enter the first name of the passenger (press ENTER to save current value):");
                Console.Write(" ");
                firstName = Console.ReadLine();
                if (firstName.Length == 0)
                {
                    firstName = passengerToEdit.FirstName;

                }
                Console.WriteLine($" The first name \"{firstName}\" has been saved.");
                #endregion

                #region Last name
                Console.WriteLine("\n --- LAST NAME ---");
                Console.WriteLine($"\n Current last name: {passengerToEdit.LastName}");
                Console.WriteLine("\n Enter the last name of the passenger (press ENTER to save current value):");
                Console.Write(" ");
                lastName = Console.ReadLine();
                if (lastName.Length == 0)
                {
                    lastName = passengerToEdit.LastName;
                }
                Console.WriteLine($" The last name \"{lastName}\" has been saved.");
                #endregion

                #region Nationality
                Console.WriteLine("\n --- NATIONALITY ---");
                Console.WriteLine($"\n Current nationality: {passengerToEdit.Nationality}");
                Console.WriteLine("\n Enter the nationality of the passenger (press ENTER to save current value):");
                Console.Write(" ");
                nationality = Console.ReadLine();
                if (nationality.Length == 0)
                {
                    nationality = passengerToEdit.Nationality;
                }
                Console.WriteLine($" The nationality \"{nationality}\" has been saved.");
                #endregion

                #region Passport
                Console.WriteLine("\n --- PASSPORT ---");
                Console.WriteLine($"\n Current passport number: {passengerToEdit.Passport}");
                Console.WriteLine("\n Enter the passport number of the passenger (press ENTER to save current value):");
                do
                {
                    Console.Write(" ");
                    isOkInput = true;
                    passport = Console.ReadLine().ToUpper();
                    if (passport.Length == 0)
                    {
                        passport = passengerToEdit.Passport;
                    }
                    else if (!IsValidPassportNumber(passport))
                    {
                        Console.WriteLine(" Invalid format. Try again.");
                        isOkInput = false;
                    }
                } while (!isOkInput);
                Console.WriteLine($" The passport number \"{passport}\" has been saved.");
                #endregion

                #region Birthday
                string input;
                Console.WriteLine("\n --- BIRTHDAY ---");
                Console.WriteLine($"\n Current birthday: {passengerToEdit.Birthday.ToShortDateString()}");
                Console.WriteLine("\n Enter the birthday of the passenger (recommended format: MM/dd/yyyy) or press ENTER to save current value:");
                do
                {
                    isOkInput = true;
                    isOkParse = true;
                    Console.Write(" ");
                    input = Console.ReadLine();
                    if (input == "")
                    {
                        birthday = passengerToEdit.Birthday;
                    }
                    else {
                        isOkParse = DateTime.TryParse(input, out birthday);
                        if (!isOkParse)
                        {
                            Console.WriteLine(" Invalid date/time format, try again.");
                        }
                        else
                        {
                            Console.WriteLine($" The date you've entered is {birthday.ToShortDateString()}.");
                            Console.WriteLine($" Save this value (press ENTER) or try to enter another (press any other key)?");
                            Console.Write(" ");
                            if (Console.ReadKey().Key != ConsoleKey.Enter)
                            {
                                isOkInput = false;
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine();
                            }
                        }
                    }
                } while (!isOkParse || !isOkInput);
                Console.WriteLine($" The birthday \"{birthday.ToShortDateString()}\" has been saved.");

                #endregion

                #region Sex
                Console.WriteLine("\n --- SEX ---");
                Console.WriteLine($"\n Current sex: {passengerToEdit.Sex}");
                Console.WriteLine("\n Enter the sex of the passenger (1 - Male, 2 - Female) or press ENTER to save current value:");
                int tempInt;
                do
                {
                    isOkInput = true;
                    isOkParse = true;
                    Console.Write(" ");
                    input = Console.ReadLine();
                    if (input == "")
                    {
                        sex = passengerToEdit.Sex;
                    }
                    else {
                        isOkParse = int.TryParse(input, out tempInt);
                        if (isOkParse)
                        {
                            switch (tempInt)
                            {
                                case 1:
                                case 2:
                                    sex = (Sex)(tempInt - 1);
                                    break;
                                default:
                                    Console.WriteLine(" Only 1 or 2, try again.");
                                    isOkInput = false;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Invalid input, try again.");
                        }
                    }
                } while (!isOkParse || !isOkInput);
                Console.WriteLine($" The sex \"{sex}\" has been saved.");
                #endregion

                #region Ticket class
                Console.WriteLine("\n --- TICKET CLASS ---");
                Console.WriteLine($"\n Current ticket class: {passengerToEdit.Ticket.Class}");
                Console.WriteLine("\n Enter the ticket class of the passenger (1 - Economy, 2 - Business) or press ENTER to save current value:");
                do
                {
                    isOkInput = true;
                    isOkParse = true;
                    Console.Write(" ");
                    input = Console.ReadLine();
                    if (input == "")
                    {
                        ticketClass = passengerToEdit.Ticket.Class;
                    }
                    else {
                        isOkParse = int.TryParse(input, out tempInt);
                        if (isOkParse)
                        {
                            switch (tempInt)
                            {
                                case 1:
                                case 2:
                                    ticketClass = (TicketClass)(tempInt - 1);
                                    break;
                                default:
                                    Console.WriteLine(" Only 1 or 2, try again.");
                                    isOkInput = false;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Invalid input, try again.");
                        }
                    }
                } while (!isOkParse || !isOkInput);
                Console.WriteLine($" The ticket class \"{ticketClass}\" has been saved.");
                #endregion

                decimal businessClassIncrement;
                if (!decimal.TryParse(ConfigurationManager.AppSettings["businessClassIncrement"], out businessClassIncrement))
                {
                    businessClassIncrement = 200;
                }
                ticketPrice = flightOfThePassenger.EconomyClassPrice + businessClassIncrement * (int)ticketClass;
                ticket = new FlightTicket { Class = ticketClass, Price = ticketPrice, FlightNumber = flightOfThePassenger.Number };

                _airline.RemovePassenger(passengerToEdit);
                _airline.AddPassenger(new Passenger(firstName, lastName, nationality, passport, birthday, sex,
                                                   ticket), flightOfThePassenger.Number);

                Console.WriteLine("\n\n The passenger's info has been successfully updated.");
            }
        }

        public override void RemovePassenger()
        {
            Passenger passengerToRemove;
            string passportNumber;
            Console.WriteLine();
            ViewAllPassengersInfo();
            do
            {
                Console.WriteLine("\n Enter the passport number of the passenger to remove:");
                Console.Write(' ');
                passportNumber = Console.ReadLine();
                passengerToRemove = GetPassengerByPassportNumber(passportNumber);
                
                if (passengerToRemove != null)
                {
                    Console.WriteLine(" The passenger has been found. Press any key to remove him/her.");
                    Console.ReadKey();
                    _airline.RemovePassenger(passengerToRemove);
                    Console.WriteLine("\n The passenger has been successfully removed.");
                }
                else
                {
                    Console.WriteLine(" The passenger hasn't been found.");
                }
                Console.WriteLine("\n Press ENTER to search again or any other key to quit.");
                Console.Write(" ");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        #endregion
    }
}
