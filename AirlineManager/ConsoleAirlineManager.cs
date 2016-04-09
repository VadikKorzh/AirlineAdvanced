using AirlineLibrary.FlightPrinters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineLibrary;

namespace AirlineManager
{
    class ConsoleAirlineManager
    {
        const string Culture = "en-US";
        const int WindowHeight = 30;
        const int WindowWidth = 113;
        const decimal businessClassIncrement = 145;

        Random random = new Random((int)DateTime.Now.Ticks);

        IFlightPrinter _flightPrinter;

        public ConsoleAirlineManager(IFlightPrinter printer)
        {
            _flightPrinter = printer;
        }

        #region Creation of flights
        
        // creates a random date between "startYear" and "stopYear"
        DateTime CreateRandomDate(int startYear, int stopYear)
        {
            if (startYear >= stopYear)
            {
                throw new ArgumentException("startYear must be less or equal to stopYear.");
            }
            else
            {
                DateTime startDate = new DateTime(startYear, 1, 1);
                return startDate.AddDays(random.Next(0, (stopYear - startYear + 1) * 365));
            }
        }
        
        string CreateRandomPassportNumber()
        {
            StringBuilder passport = new StringBuilder();
            int temp;
            temp = random.Next(26);
            passport.Append((char)('A' + temp));
            temp = random.Next(26);
            passport.Append((char)('A' + temp));
            passport.Append(' ');
            passport.Append(String.Format("{0:d6}", random.Next(999999)));
            return passport.ToString();
        }

        FlightTicket CreateRandomTicket(decimal basePrice)
        {
            FlightTicket ticket = new FlightTicket();
            int temp;
            temp = random.Next(2);
            ticket.Class = (TicketClass)temp;
            // business class ticket price = economy class ticket price + "businessClassIncrement" (for example)
            ticket.Price = basePrice + businessClassIncrement * temp;
            return ticket;
        }

        Passenger[] CreateRandomPassengers(int maxNumberOfPassengers, int numberOfPassengers, int flightNumber, decimal economyClassPrice)
        {
            Passenger[] passengers = new Passenger[maxNumberOfPassengers];
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

            int temp = random.Next(1, 6);

            for (int i = 0; i < numberOfPassengers; i++)
            {
                temp = random.Next(2);
                if (temp == 0)
                {
                    sex = Sex.Male;

                    temp = random.Next(firstNamesMale.Length);
                    firstName = firstNamesMale[temp];

                    temp = random.Next(lastNamesMale.Length);
                    lastName = lastNamesMale[temp];
                }
                else
                {
                    sex = Sex.Female;

                    temp = random.Next(firstNamesFemale.Length);
                    firstName = firstNamesFemale[temp];

                    temp = random.Next(lastNamesFemale.Length);
                    lastName = lastNamesFemale[temp];
                }

                temp = random.Next(nationalities.Length);
                nationality = nationalities[temp];

                passport = CreateRandomPassportNumber();

                ticket = CreateRandomTicket(economyClassPrice);

                temp = random.Next(int.MaxValue);
                birthday = CreateRandomDate(1950, 1990);

                passengers[i] = new Passenger(firstName, lastName, nationality, passport, birthday, sex, ticket, flightNumber);
            }
            return passengers;
        }

        void CreateSomeFlights(Airline airline)
        {
            FlightDirection direction = FlightDirection.Arrival;
            DateTime time;
            int number;
            string destination;
            AirportGate gate = AirportGate.A1;
            FlightStatus status = FlightStatus.Arrived;
            decimal economyClassPrice;
            int numberOfPassengersToCreate, maxNumberOfPassengers;
            Passenger[] passengers;

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

            airline.AddFlight(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));

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

            airline.AddFlight(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));

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
            airline.AddFlight(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));

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

            airline.AddFlight(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));

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

            airline.AddFlight(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));
        }

        #endregion

        #region Output
        
        // view the information of all the passengers in one table
        void ViewAllPassengersInfo(Airline airline)
        {
            int maxProperPassengers = 1, index = 0;
            Flight[] flights = airline.Flights;
            Passenger[] allPassengers = new Passenger[maxProperPassengers];
            Passenger[] tempPassengers = new Passenger[maxProperPassengers];

            foreach (var flight in flights)
            {
                if (flight != null && flight.Passengers != null)
                {
                    foreach (var passenger in flight.Passengers)
                    {
                        if (passenger != null)
                        {
                            if (index < maxProperPassengers)
                            {
                                allPassengers[index++] = passenger;
                            }
                            else
                            {
                                maxProperPassengers *= 2;
                                tempPassengers = new Passenger[maxProperPassengers];
                                Array.Copy(allPassengers, tempPassengers, allPassengers.Length);
                                allPassengers = tempPassengers;

                                allPassengers[index++] = passenger;
                            }
                        }
                    }
                }
            }
            _flightPrinter.PrintPassengersInfo(allPassengers);
        }
        // view the full information about the flights and the passengers
        void ViewFlightPassengers(Flight[] flights)
        {
            bool isOkParse, isFound, isCorrectNumber;
            int flightNumber;
            Console.Write($"\n There are {flights.Count(o => o != null)} flights: ");
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i] != null)
                {
                    Console.Write($"{flights[i].Number:d4}, ");
                }
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
                foreach (var flight in flights)
                {
                    if (flight != null)
                    {
                        _flightPrinter.PrintFlightInfo(flight);
                        _flightPrinter.PrintPassengersInfo(flight.Passengers);
                        Console.WriteLine(" " + (new StringBuilder()).Append('-', Console.WindowWidth - 2));
                    }
                }
            }
            else
            {
                isFound = false;
                foreach (Flight flight in flights)
                {
                    if (flight != null && flight.Number == flightNumber)
                    {
                        isFound = true;
                        Console.WriteLine(" The flight has been found, press any key to show the information.");
                        Console.Write(' ');
                        Console.ReadKey();
                        _flightPrinter.PrintFlightInfo(flight);
                        _flightPrinter.PrintPassengersInfo(flight.Passengers);
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

        #region Flight and passenger search

        void FindPassengersByName(Flight[] flights)
        {
            int maxProperPassengers = 2;
            Passenger[] properPassengers = new Passenger[maxProperPassengers];
            Passenger[] tempPassengers = new Passenger[maxProperPassengers];

            ConsoleKey keyPressed;
            string fullName, nameToSeek;
            bool isFirstMatch, isFound;
            int index;

            do
            {
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
                    index = 0;
                    foreach (var flight in flights)
                    {
                        if (flight != null && flight.Passengers != null)
                        {
                            foreach (var passenger in flight.Passengers)
                            {
                                if (passenger != null)
                                {
                                    fullName = $"{passenger.FirstName} {passenger.LastName}";
                                    if (fullName.ToUpper().Contains(nameToSeek))
                                    {
                                        if (index < maxProperPassengers)
                                        {
                                            if (isFirstMatch)
                                            {
                                                isFirstMatch = false;
                                                isFound = true;
                                            }
                                            properPassengers[index++] = passenger;
                                        }
                                        else
                                        {
                                            maxProperPassengers *= 2;
                                            tempPassengers = new Passenger[maxProperPassengers];
                                            Array.Copy(properPassengers, tempPassengers, properPassengers.Length);
                                            properPassengers = tempPassengers;

                                            properPassengers[index++] = passenger;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (isFound)
                {
                    _flightPrinter.PrintPassengersInfo(properPassengers);
                    Array.Clear(properPassengers, 0, properPassengers.Length);
                    Array.Clear(tempPassengers, 0, tempPassengers.Length);
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

        void FindPassengersByPassportNumber(Flight[] flights)
        {
            int maxProperPassengers = 1;
            Passenger[] properPassengers = new Passenger[maxProperPassengers];
            Passenger[] tempPassengers = new Passenger[maxProperPassengers];

            ConsoleKey keyPressed;
            string passportNumberToSeek;
            int index;
            bool isFirstMatch, isFound;

            do
            {
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
                    index = 0;
                    foreach (var flight in flights)
                    {
                        if (flight != null && flight.Passengers != null)
                        {
                            foreach (var passenger in flight.Passengers)
                            {
                                if (passenger != null && passenger.Passport.Contains(passportNumberToSeek))
                                {
                                    if (index < maxProperPassengers)
                                    {
                                        if (isFirstMatch)
                                        {
                                            isFirstMatch = false;
                                            isFound = true;
                                        }
                                        properPassengers[index++] = passenger;
                                    }
                                    else
                                    {
                                        maxProperPassengers *= 2;
                                        tempPassengers = new Passenger[maxProperPassengers];
                                        Array.Copy(properPassengers, tempPassengers, properPassengers.Length);
                                        properPassengers = tempPassengers;

                                        properPassengers[index++] = passenger;
                                    }
                                }
                            }
                        }
                    }
                }
                if (isFound)
                {
                    _flightPrinter.PrintPassengersInfo(properPassengers);
                    Array.Clear(properPassengers, 0, properPassengers.Length);
                    Array.Clear(tempPassengers, 0, tempPassengers.Length);
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

        void FindFlightsByEconomyClassPrice(Flight[] flights)
        {
            Flight[] properFlights = new Flight[flights.Length];
            ConsoleKey keyPressed;
            decimal upperPrice;
            int index;
            bool isFirstMatch, isFound, isValidInput;

            do
            {
                Console.WriteLine("\n Enter a price of economy class ticket (maximally affordable) to seek:");
                do
                {
                    Console.Write(" ");
                    isValidInput = Decimal.TryParse(Console.ReadLine(), out upperPrice);
                    if (!isValidInput)
                    {
                        Console.WriteLine(" Invalid input, enter a decimal value.");
                    }
                } while (!isValidInput);

                isFirstMatch = true;
                isFound = false;
                index = 0;

                foreach (var flight in flights)
                {
                    if (flight != null)
                    {
                        if (flight.EconomyClassPrice <= upperPrice)
                        {
                            if (isFirstMatch)
                            {
                                isFound = true;
                                isFirstMatch = false;
                            }
                            properFlights[index] = flight;
                            index++;
                        }
                    }
                }

                if (isFound)
                {
                    Console.Write(" Some flights've been found. Press any key to show the information. ");
                    Console.ReadKey();
                    Console.WriteLine();
                    _flightPrinter.PrintFlightsInfo(properFlights);
                    Array.Clear(properFlights, 0, properFlights.Length);
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

        #region Add, remove, edit flight

        Flight GetFlightByNumber(Flight[] flights, int flightNumber)
        {
            foreach (Flight flight in flights)
            {
                if (flight.Number == flightNumber)
                {
                    return flight;
                }
            }
            return null;
        }

        void AddFlight(Airline airline)
        {
            Flight[] flights = airline.Flights;
            FlightDirection direction = FlightDirection.Arrival;
            DateTime time;
            int number;
            string destination;
            AirportGate gate = AirportGate.A1;
            FlightStatus status = FlightStatus.Arrived;
            decimal economyClassPrice;
            int numberOfPassengers, maxNumberOfPassengers;
            Passenger[] passengers;

            bool isOkParse, isProperOption, isFreeFlightNumber, isOkInput;
            int tempInt;
            string tempString;
            if (airline.IsFull())
            {
                Console.WriteLine("\n The maximum number of flights is reached. Remove some to add another one.");
            }
            else
            {
                #region Direction

                Console.WriteLine("\n --- FLIGHT DIRECTION ---");
                Console.WriteLine("\n Enter the direction (1 - Arrival, 2 - Departure):");
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
                                direction = (FlightDirection)(tempInt - 1);
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
                Console.WriteLine($" The direction \"{direction}\" has been saved.");

                #endregion

                #region Flight number

                Console.WriteLine("\n --- FLIGHT NUMBER ---");
                Console.WriteLine("\n Enter the flight number (an integer within [1, 9999]):");

                do
                {
                    isOkInput = true;
                    isFreeFlightNumber = true;
                    Console.Write(" ");
                    isOkParse = int.TryParse(Console.ReadLine(), out number);
                    if (isOkParse)
                    {
                        if (number < 1 || number >= 10000)
                        {
                            Console.WriteLine(" Flight number must be within the range [1, 9999]. Try again.");
                            isOkInput = false;
                        }
                        else
                        {
                            foreach (var flight in flights)
                            {
                                if (flight != null && flight.Number == number)
                                {
                                    isFreeFlightNumber = false;
                                    break;
                                }
                            }
                            if (!isFreeFlightNumber)
                            {
                                Console.WriteLine(" This number is occupied. Try another one.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Invalid input, try again.");
                    }
                } while (!isOkParse || !isOkInput || !isFreeFlightNumber);
                Console.WriteLine($" The flight number \"{number}\" has been saved.");

                #endregion

                #region Time

                Console.WriteLine("\n --- FLIGHT TIME ---");
                Console.WriteLine($"\n Current time: {DateTime.Now.ToString("g")}");
                do
                {
                    Console.WriteLine(" Enter the flight time (recommended format: MM/dd/yyyy hh:mm):");
                    isOkInput = true;
                    Console.Write(" ");
                    isOkParse = DateTime.TryParse(Console.ReadLine(), out time);
                    if (!isOkParse)
                    {
                        Console.WriteLine(" Invalid date/time format, try again.");
                    }
                    else
                    {
                        Console.WriteLine($" The date/time you've entered is {time.ToString("g")}.");
                        Console.WriteLine($" Save this value (press ENTER) or try to enter another (press any other key)?");
                        Console.Write(" ");
                        if (Console.ReadKey().Key != ConsoleKey.Enter)
                        {
                            isOkInput = false;
                            Console.WriteLine();
                        }
                    }
                } while (!isOkParse || !isOkInput);
                Console.WriteLine($"\n The flight time \"{time}\" has been saved.");

                #endregion

                #region Destination

                Console.WriteLine("\n --- FLIGHT DESTINATION ---");
                Console.WriteLine("\n Enter the destination of the flight:");
                do
                {
                    Console.Write(" ");
                    isOkInput = true;
                    destination = Console.ReadLine();
                    if (destination.Length == 0)
                    {
                        Console.WriteLine(" You've entered an empty string. Try again.");
                        isOkInput = false;
                    }
                } while (!isOkInput);
                Console.WriteLine($" The destination \"{destination}\" has been saved.");

                #endregion

                #region EC price

                Console.WriteLine("\n --- ECONOMY CLASS TICKET PRICE ---");
                Console.WriteLine("\n Enter the price of economy class ticket:");
                bool isProperPrice = true;
                do
                {
                    Console.Write(" ");
                    isOkParse = decimal.TryParse(Console.ReadLine(), out economyClassPrice);
                    if (isOkParse)
                    {
                        isProperPrice = true;
                        if (economyClassPrice < 0)
                        {
                            Console.WriteLine(" You've entered a negative number. Try again.");
                            isProperPrice = false;
                        }
                        else if (economyClassPrice > 10000)
                        {
                            Console.WriteLine(" The price you've entered is too high. Try again.");
                            isProperPrice = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Invalid input, try again.");
                    }
                } while (!isOkParse || !isProperPrice);
                Console.WriteLine($" The economy ticket price \"{economyClassPrice}\" has been saved.");
                
                #endregion

                #region Gate

                Console.WriteLine("\n --- FLIGHT GATE ---");
                Console.Write("\n Enter the gate number (among ");
                var gateNames = Enum.GetNames(gate.GetType());
                for (int i = 0; i < gateNames.Length; i++)
                {
                    if (i != gateNames.Length - 1)
                    {
                        Console.Write(gateNames[i] + ", ");
                    }
                    else
                    {
                        Console.WriteLine(gateNames[i] + "):");
                    }
                }

                do
                {
                    Console.Write(" ");

                    tempString = Console.ReadLine().Trim().ToUpper();
                    isProperOption = gateNames.Contains(tempString);
                    if (!isProperOption)
                    {
                        Console.WriteLine(" Invalid input, try again.");
                    }
                    else
                    {
                        for (int i = 0; i < gateNames.Length; i++)
                        {
                            if (tempString == gateNames[i])
                            {
                                int[] gateValues = (int[])Enum.GetValues(gate.GetType());
                                gate = (AirportGate)gateValues[i];
                                break;
                            }
                        }
                    }
                } while (!isProperOption);
                Console.WriteLine($" The gate \"{gate}\" has been saved.");

                #endregion

                #region Status

                Console.WriteLine("\n --- FLIGHT STATUS ---");
                Console.WriteLine("\n Enter the status:\n");
                var statusNames = Enum.GetNames(status.GetType());
                for (int i = 0; i < statusNames.Length; i++)
                {
                    Console.WriteLine($"    {i} - {statusNames[i]}");
                }

                do
                {
                    isProperOption = true;
                    Console.Write(" ");
                    isOkParse = int.TryParse(Console.ReadLine(), out tempInt);
                    if (isOkParse)
                    {
                        if (tempInt < 0 || tempInt >= statusNames.Length)
                        {
                            Console.WriteLine($" Status must be within the range [0, {statusNames.Length - 1}]. Try again.");
                            isProperOption = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Invalid input, try again.");
                    }
                } while (!isOkParse || !isProperOption);
                status = (FlightStatus)tempInt;
                Console.WriteLine($" The status \"{status}\" has been saved.");

                #endregion

                #region Passengers

                Console.WriteLine("\n --- PASSENGERS ---");
                Console.WriteLine("\n Enter maximum number of passengers of the flight:");
                do
                {
                    Console.Write(" ");
                    isOkInput = true;
                    isOkParse = int.TryParse(Console.ReadLine(), out maxNumberOfPassengers);
                    if (isOkParse)
                    {
                        if (maxNumberOfPassengers <= 0 || maxNumberOfPassengers > 200)
                        {
                            Console.WriteLine(" The number must be within [1, 200]. Try again.");
                            isOkInput = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Invalid input, try again.");
                    }
                } while (!isOkParse || !isOkInput);
                Console.WriteLine($" Maximum number of passengers \"{maxNumberOfPassengers}\" has been saved.");

                Console.WriteLine("\n 1 - Create random passengers");
                Console.WriteLine(" 2 - Add passengers manually");
                int option;
                do
                {
                    Console.Write(" ");
                    isProperOption = true;
                    isOkParse = int.TryParse(Console.ReadLine(), out option);
                    if (isOkParse)
                    {
                        if (option < 1 || option > 2)
                        {
                            Console.WriteLine(" Only 1 or 2. Try again.");
                            isProperOption = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Invalid input, try again.");
                    }
                } while (!isOkParse || !isProperOption);

                if (option == 1)
                {
                    Console.WriteLine("\n Enter a number of passengers to create:");
                    do
                    {
                        Console.Write(" ");
                        isOkInput = true;
                        isOkParse = int.TryParse(Console.ReadLine(), out numberOfPassengers);
                        if (isOkParse)
                        {
                            if (numberOfPassengers <= 0 || numberOfPassengers > maxNumberOfPassengers)
                            {
                                Console.WriteLine($" The number must be within [1, {maxNumberOfPassengers}]. Try again.");
                                isOkInput = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Invalid input, try again.");
                        }
                    } while (!isOkParse || !isOkInput);

                    passengers = CreateRandomPassengers(maxNumberOfPassengers, numberOfPassengers, number, economyClassPrice);
                    Console.WriteLine($" {numberOfPassengers} random passenger has been created.");
                }
                else
                {
                    int index = 0;
                    bool isFull, isEnough;
                    passengers = new Passenger[maxNumberOfPassengers];
                    do
                    {
                        isFull = isEnough = false;
                        passengers[index++] = CreateNewPassenger(number, economyClassPrice);
                        if (index < maxNumberOfPassengers)
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
                            Console.WriteLine(" The flight is full.");
                            isFull = true;
                        }

                    } while (!isFull && !isEnough);

                    Console.WriteLine($"\n {index} passenger has been created.");

                }

                #endregion

                airline.AddFlight(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));
                Console.WriteLine("\n The flight has been successfully added.");
            }
        }

        void EditFlight(Airline airline)
        {
            Flight[] flights = airline.Flights;

            FlightDirection direction = FlightDirection.Arrival;
            DateTime time;
            int number;
            string destination;
            AirportGate gate = AirportGate.A1;
            FlightStatus status = FlightStatus.Arrived;
            decimal economyClassPrice;

            bool isOkParse, isProperOption, isFreeFlightNumber, isOkInput, isCorrectNumber;
            int tempInt;

            int flightNumber;
            int[] flightNumbers = new int[flights.Length];

            Console.Write($"\n There are {flights.Count(o => o != null)} flights: ");
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i] != null)
                {
                    flightNumbers[i] = flights[i].Number;
                    Console.Write($"{flights[i].Number:d4}, ");
                }
            }
            Console.Write("\b\b.");

            Console.WriteLine("\n Enter the number of the flight to edit:");
            do
            {
                isCorrectNumber = true;
                Console.Write(' ');
                isOkParse = int.TryParse(Console.ReadLine(), out flightNumber);
                if (isOkParse && !flightNumbers.Contains(flightNumber))
                {
                    Console.WriteLine(" There is no flight with this number. Try again.");
                    isCorrectNumber = false;
                }
                else if (!isOkParse)
                {
                    Console.WriteLine(" Incorrect input, try again.");
                }
            } while (!isOkParse || !isCorrectNumber);
            Console.WriteLine("\n The flight has been found.");
            Console.WriteLine(" Press ENTER to edit the flight or press any other key to quit.");
            Console.Write(" ");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Flight currentFlight = GetFlightByNumber(flights, flightNumber);

                #region Direction

                Console.WriteLine("\n --- FLIGHT DIRECTION ---");
                Console.WriteLine($"\n Current direction: {currentFlight.Direction}.");
                Console.WriteLine("\n Enter the direction (1 - Arrival, 2 - Departure) or press ENTER to save current value:");
                string input;
                do
                {
                    isOkInput = true;
                    Console.Write(" ");
                    input = Console.ReadLine();
                    if (input == "")
                    {
                        direction = currentFlight.Direction;
                    }
                    else {
                        isOkParse = int.TryParse(input, out tempInt);
                        if (isOkParse)
                        {
                            switch (tempInt)
                            {
                                case 1:
                                case 2:
                                    direction = (FlightDirection)(tempInt - 1);
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
                Console.WriteLine($" The direction \"{direction}\" has been saved.");

                #endregion

                #region Flight number

                Console.WriteLine("\n --- FLIGHT NUMBER ---");
                Console.WriteLine($"\n Current flight number: {currentFlight.Number}.");
                Console.WriteLine("\n Enter the flight number (an integer within [1, 9999]) or press ENTER to save current value:");
                do
                {
                    isOkInput = true;
                    isFreeFlightNumber = true;
                    Console.Write(" ");
                    input = Console.ReadLine();
                    if (input == "")
                    {
                        number = currentFlight.Number;
                    }
                    else {
                        isOkParse = int.TryParse(input, out number);
                        if (isOkParse)
                        {
                            if (number < 1 || number >= 10000)
                            {
                                Console.WriteLine(" Flight number must be within the range [1, 9999]. Try again.");
                                isOkInput = false;
                            }
                            else
                            {
                                foreach (var flight in flights)
                                {
                                    if (flight != null && flight.Number == number)
                                    {
                                        isFreeFlightNumber = false;
                                        break;
                                    }
                                }
                                if (!isFreeFlightNumber)
                                {
                                    Console.WriteLine(" This number is occupied. Try another one.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Invalid input, try again.");
                        }
                    }
                } while (!isOkParse || !isOkInput || !isFreeFlightNumber);
                Console.WriteLine($" The flight number \"{number}\" has been saved.");

                #endregion

                #region Time

                Console.WriteLine("\n --- FLIGHT TIME ---");
                Console.WriteLine($"\n Current flight time: {currentFlight.Time.ToString("g")}");
                do
                {
                    Console.WriteLine("\n Enter the flight time (recommended format: MM/dd/yyyy hh:mm) or press ENTER to save current value:");
                    isOkInput = true;
                    Console.Write(" ");
                    input = Console.ReadLine();
                    if (input == "")
                    {
                        time = currentFlight.Time;
                    }
                    else {
                        isOkParse = DateTime.TryParse(input, out time);
                        if (!isOkParse)
                        {
                            Console.WriteLine(" Invalid date/time format, try again.");
                        }
                        else
                        {
                            Console.WriteLine($" The date/time you've entered is {time.ToString()}.");
                            Console.WriteLine($" Save this value (press ENTER) or try to enter another (press any other key)?");
                            Console.Write(" ");
                            if (Console.ReadKey().Key != ConsoleKey.Enter)
                            {
                                isOkInput = false;
                                Console.WriteLine();
                            }
                        }
                    }
                } while (!isOkParse || !isOkInput);

                Console.WriteLine($" The flight time \"{time.ToString("g")}\" has been saved.");

                #endregion

                #region Destination

                Console.WriteLine("\n --- FLIGHT DESTINATION ---");
                Console.WriteLine($"\n Current destination: {currentFlight.Destination}.");
                Console.WriteLine("\n Enter the destination of the flight or press ENTER to save current value:");
                Console.Write(" ");
                destination = Console.ReadLine();
                if (destination == "")
                {
                    destination = currentFlight.Destination;
                }
                Console.WriteLine($" The destination \"{destination}\" has been saved.");

                #endregion

                #region EC price

                Console.WriteLine("\n --- ECONOMY CLASS TICKET PRICE ---");
                Console.WriteLine($"\n Current economy class ticket price: {currentFlight.EconomyClassPrice}.");
                Console.WriteLine("\n Enter the price of economy class ticket or press ENTER to save current value:");
                bool isProperPrice = true;

                do
                {
                    Console.Write(" ");
                    input = Console.ReadLine();
                    if (input == "")
                    {
                        economyClassPrice = currentFlight.EconomyClassPrice;
                    }
                    else {
                        isOkParse = decimal.TryParse(input, out economyClassPrice);
                        if (isOkParse)
                        {
                            isProperPrice = true;
                            if (economyClassPrice < 0)
                            {
                                Console.WriteLine(" You've entered a negative number. Try again.");
                                isProperPrice = false;
                            }
                            else if (economyClassPrice > 10000)
                            {
                                Console.WriteLine(" The price you've entered is too high. Try again.");
                                isProperPrice = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Invalid input, try again.");
                        }
                    }
                } while (!isOkParse || !isProperPrice);
                Console.WriteLine($" The economy ticket price \"{economyClassPrice}\" has been saved.");
                
                #endregion

                #region Gate

                Console.WriteLine("\n --- FLIGHT GATE ---");
                Console.WriteLine($"\n Current gate: {currentFlight.Gate}.");
                Console.Write("\n Enter the gate number (among ");
                var gateNames = Enum.GetNames(gate.GetType());
                for (int i = 0; i < gateNames.Length; i++)
                {
                    if (i != gateNames.Length - 1)
                    {
                        Console.Write(gateNames[i] + ", ");
                    }
                    else
                    {
                        Console.Write(gateNames[i] + ") or press ENTER to save current value:\n");
                    }
                }

                do
                {
                    isProperOption = true;
                    Console.Write(" ");
                    input = Console.ReadLine().Trim().ToUpper();
                    if (input == "")
                    {
                        gate = currentFlight.Gate;
                    }
                    else {
                        isProperOption = gateNames.Contains(input);
                        if (!isProperOption)
                        {
                            Console.WriteLine(" Invalid input, try again.");
                        }
                        else
                        {
                            for (int i = 0; i < gateNames.Length; i++)
                            {
                                if (input == gateNames[i])
                                {
                                    int[] gateValues = (int[])Enum.GetValues(gate.GetType());
                                    gate = (AirportGate)gateValues[i];
                                    break;
                                }
                            }
                        }
                    }
                } while (!isProperOption);
                Console.WriteLine($" The gate \"{gate}\" has been saved.");

                #endregion

                #region Status

                Console.WriteLine("\n --- FLIGHT STATUS ---");
                Console.WriteLine($"\n Current status: {currentFlight.Status}.");
                Console.WriteLine("\n Enter the status:\n");
                var statusNames = Enum.GetNames(status.GetType());
                for (int i = 0; i < statusNames.Length; i++)
                {
                    Console.WriteLine($"    {i} - {statusNames[i]}");
                }
                Console.WriteLine("\n or press ENTER to save current value:");
                do
                {
                    isProperOption = true;
                    Console.Write(" ");
                    input = Console.ReadLine().Trim().ToUpper();
                    if (input == "")
                    {
                        status = currentFlight.Status;
                    }
                    else {
                        isOkParse = int.TryParse(input, out tempInt);
                        if (isOkParse)
                        {
                            if (tempInt < 0 || tempInt >= statusNames.Length)
                            {
                                Console.WriteLine($" Status must be within the range [0, {statusNames.Length - 1}]. Try again.");
                                isProperOption = false;
                            }
                            else
                            {
                                status = (FlightStatus)tempInt;
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Invalid input, try again.");
                        }
                    }
                } while (!isOkParse || !isProperOption);
                Console.WriteLine($" The status \"{status}\" has been saved.");

                #endregion

                airline.RemoveFlight(currentFlight);
                airline.AddFlight(new Flight(direction, time, number, destination, gate, status, economyClassPrice,
                                  currentFlight.MaxNumberOfPassengers, currentFlight.Passengers));
                Console.WriteLine("\n The flight info has been successfully updated.");
            }
        }

        void RemoveFlight(Airline airline)
        {
            Flight[] flights = airline.Flights;

            int[] flightNumbers = new int[flights.Length];
            bool isOkParse, isCorrectNumber;
            int flightNumber;

            if (airline.NumberOfFlights == 0)
            {
                Console.WriteLine("\n There are no flights to remove.");
            }
            else {
                Console.Write($"\n There are {airline.NumberOfFlights} flights: ");
                for (int i = 0; i < flights.Length; i++)
                {
                    if (flights[i] != null)
                    {
                        flightNumbers[i] = flights[i].Number;
                        Console.Write($"{flights[i].Number:d4}, ");
                    }
                }
                Console.Write("\b\b.");
                Console.WriteLine("\n Enter the number of the flight to remove:");
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

                if (flightNumbers.Contains(flightNumber))
                {
                    Console.WriteLine(" The flight has been found. Press any key to show the flight's info.");
                    Console.ReadKey();
                    int i;
                    for (i = 0; i < flights.Length; i++)
                    {
                        if (flights[i] != null && flights[i].Number == flightNumber)
                        {
                            _flightPrinter.PrintFlightInfo(flights[i]);
                            _flightPrinter.PrintPassengersInfo(flights[i].Passengers);
                            break;
                        }
                    }
                    Console.WriteLine("\n Press any key to remove the flight.");
                    Console.ReadKey();
                    airline.RemoveFlight(flights[i]);
                    Console.WriteLine("\n The flight has been removed.");
                }
                else
                {
                    Console.WriteLine(" The flight hasn't been found.");
                }
            }
        }

        #endregion

        #region Add, remove, edit passenger

        Passenger GetPassengerByPassportNumber(Flight[] flights, string passportNumber)
        {
            foreach (Flight flight in flights)
            {
                if (flight != null)
                {
                    foreach (Passenger passenger in flight.Passengers)
                    {
                        if (passenger != null && passenger.Passport.Equals(passportNumber))
                        {
                            return passenger;
                        }
                    }
                }
            }
            return null;
        }

        bool IsValidPassportNumber(string passportNumber)
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

        Passenger CreateNewPassenger(int flightNumber, decimal ecTicketPrice)
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

            ticketPrice = ecTicketPrice + businessClassIncrement * (int)ticketClass;
            ticket = new FlightTicket { Class = ticketClass, Price = ticketPrice };

            return new Passenger(firstName, lastName, nationality, passport, birthday, sex,
                                                               ticket, flightNumber);
        }

        void AddPassenger(Airline airline)
        {
            bool isOkParse, isCorrectNumber, isFound;
            Flight[] flights = airline.Flights;
            int flightNumber;
            string fullOrNot;
            Console.Write($"\n There are {airline.NumberOfFlights} flights: ");
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[i] != null)
                {
                    fullOrNot = (flights[i].IsFull()) ? " (full)" : "";
                    Console.Write($"{flights[i].Number:d4}{fullOrNot}, ");
                }
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
                    _flightPrinter.PrintFlightInfo(flight);
                    _flightPrinter.PrintPassengersInfo(flight.Passengers);
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
                            airline.AddPassenger(CreateNewPassenger(flightNumber, flight.EconomyClassPrice), flightNumber);
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

        void EditPassenger(Airline airline)
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
            ViewAllPassengersInfo(airline);

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
            foreach (Flight flight in airline.Flights)
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

                ticketPrice = flightOfThePassenger.EconomyClassPrice + businessClassIncrement * (int)ticketClass;
                ticket = new FlightTicket { Class = ticketClass, Price = ticketPrice };

                airline.RemovePassenger(passengerToEdit);
                airline.AddPassenger(new Passenger(firstName, lastName, nationality, passport, birthday, sex,
                                                   ticket, flightOfThePassenger.Number), flightOfThePassenger.Number);

                Console.WriteLine("\n\n The passenger's info has been successfully updated.");
            }
        }

        void RemovePassenger(Airline airline)
        {
            Passenger passengerToRemove;
            string passportNumber;
            Console.WriteLine();
            ViewAllPassengersInfo(airline);
            do
            {
                Console.WriteLine("\n Enter the passport number of the passenger to remove:");
                Console.Write(' ');
                passportNumber = Console.ReadLine().Trim().ToUpper();
                passengerToRemove = GetPassengerByPassportNumber(airline.Flights, passportNumber);
                if (passengerToRemove != null)
                {
                    Console.WriteLine(" The passenger has been found. Press any key to remove him/her.");
                    Console.ReadKey();
                    airline.RemovePassenger(passengerToRemove);
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

        #region Form menus

        void ShowMenuHeader(string menuTitle)
        {
            int numberOfSpaces = 3;
            int starsLengthLeft = (WindowWidth - 2 - menuTitle.Length - 2 * numberOfSpaces) / 2;
            int starsLengthRight = WindowWidth - 2 - menuTitle.Length - 2 * numberOfSpaces - starsLengthLeft;
            StringBuilder menuHeader = new StringBuilder();
            menuHeader.Append(' ');
            menuHeader.Append('*', starsLengthLeft);
            menuHeader.Append(' ', numberOfSpaces);
            menuHeader.Append($"{menuTitle.ToUpper()}");
            menuHeader.Append(' ', numberOfSpaces);
            menuHeader.Append('*', starsLengthRight);

            Console.WriteLine();
            Console.WriteLine(menuHeader.ToString());
        }

        void ShowMainMenu()
        {
            ShowMenuHeader("main menu");
            Console.WriteLine();
            Console.WriteLine("  1. View all the flights");
            Console.WriteLine("  2. View all the passengers of a specified flight");
            Console.WriteLine();
            Console.WriteLine("  3. Find passengers by name");
            Console.WriteLine("  4. Find passengers by passport number");
            Console.WriteLine("  5. Find flights by the economy class price");
            Console.WriteLine();
            Console.WriteLine("  6. Add a flight");
            Console.WriteLine("  7. Edit a flight");
            Console.WriteLine("  8. Remove a flight");
            Console.WriteLine();
            Console.WriteLine("  9. Add a passenger");
            Console.WriteLine(" 10. Edit a passenger");
            Console.WriteLine(" 11. Remove a passenger");
            Console.WriteLine();
            Console.WriteLine("  0. Quit menu");
            Console.WriteLine();
        }

        #endregion

        #region Management 

        void Operate(Airline airline)
        {
            bool isOkParse, isProperOption;
            byte option;

            do
            {
                Console.Write(" ");
                isProperOption = true;
                isOkParse = byte.TryParse(Console.ReadLine(), out option);
                if (isOkParse)
                {
                    switch (option)
                    {
                        case 1:
                            ShowMenuHeader("view all the flights");
                            _flightPrinter.PrintFlightsInfo(airline.Flights);
                            break;
                        case 2:
                            ShowMenuHeader("view the passengers of a flight");
                            ViewFlightPassengers(airline.Flights);
                            break;
                        case 3:
                            ShowMenuHeader("find passengers by name");
                            FindPassengersByName(airline.Flights);
                            break;
                        case 4:
                            ShowMenuHeader("find passengers by passport number");
                            FindPassengersByPassportNumber(airline.Flights);
                            break;
                        case 5:
                            ShowMenuHeader("find flights by the economy class price");
                            FindFlightsByEconomyClassPrice(airline.Flights);
                            break;
                        case 6:
                            ShowMenuHeader("add a flight");
                            AddFlight(airline);
                            break;
                        case 7:
                            ShowMenuHeader("edit a flight");
                            EditFlight(airline);
                            break;
                        case 8:
                            ShowMenuHeader("remove a flight");
                            RemoveFlight(airline);
                            break;
                        case 9:
                            ShowMenuHeader("add a passenger");
                            AddPassenger(airline);
                            break;
                        case 10:
                            ShowMenuHeader("edit a passenger");
                            EditPassenger(airline);
                            break;
                        case 11:
                            ShowMenuHeader("remove a passenger");
                            RemovePassenger(airline);
                            break;
                        case 0:
                            break;
                        default:
                            isProperOption = false;
                            Console.WriteLine(" Incorrect input, try again");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(" Incorrect input, try again");
                }
            } while (!isOkParse || !isProperOption);
        }

        public void Manage(Airline airline)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(Culture);
            Console.WindowHeight = WindowHeight;
            Console.WindowWidth = Console.BufferWidth = WindowWidth;
            CreateSomeFlights(airline);
            do
            {
                Console.Clear();
                ShowMainMenu();
                Operate(airline);
                Console.WriteLine("\n Press ESC to quit or any other key to go back to main menu.");
                Console.Write(" ");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        #endregion
    }
}
