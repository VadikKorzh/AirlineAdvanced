using KRZHK.AirlineLibrary;
using KRZHK.AirlineLibrary.Enums;
using KRZHK.AirlineLibrary.FlightPrinters;
using KRZHK.AirlineManager.PassengerManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRZHK.AirlineManager.FlightManagers
{
    class ConsoleFlightManager : FlightManager
    {
        IFlightPrinter _flightPrinter;

        public ConsoleFlightManager(Airline airline, IFlightPrinter printer)
        {
            _airline = airline;
            _flightPrinter = printer;
            _passengersManager = new ConsolePassengerManager(airline, printer);
        }

        #region Add, remove, edit flight

        public override void AddFlight()
        {
            List<Flight> flights = _airline.Flights;
            FlightDirection direction = FlightDirection.Arrival;
            DateTime time;
            int number;
            string destination;
            AirportGate gate = AirportGate.A1;
            FlightStatus status = FlightStatus.Arrived;
            decimal economyClassPrice;
            int numberOfPassengers, maxNumberOfPassengers;
            List<Passenger> passengers;
            AirlineFactory airlineFactory = new AirlineFactory();

            bool isOkParse, isProperOption, isFreeFlightNumber, isOkInput;
            int tempInt;
            string tempString;
            if (_airline.IsFull())
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

                    passengers = airlineFactory.CreateRandomPassengers(maxNumberOfPassengers, numberOfPassengers, number, economyClassPrice);
                    Console.WriteLine($" {numberOfPassengers} random passenger has been created.");
                }
                else
                {
                    int index = 0;
                    bool isFull, isEnough;
                    passengers = new List<Passenger>(maxNumberOfPassengers);
                    do
                    {
                        isFull = isEnough = false;
                        passengers.Add(_passengersManager.CreateNewPassenger(number, economyClassPrice));
                        if (++index < maxNumberOfPassengers)
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

                _airline.AddFlight(new Flight(direction, time, number, destination, gate, status, economyClassPrice, maxNumberOfPassengers, passengers));
                Console.WriteLine("\n The flight has been successfully added.");
            }
        }

        public override void EditFlight()
        {
            List<Flight> flights = _airline.Flights;

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
            List<int> flightNumbers = new List<int>();

            Console.Write($"\n There are {flights.Count(o => o != null)} flights: ");
            foreach (var flight in flights)
            {
                flightNumbers.Add(flight.Number);
                Console.Write($"{flight.Number:d4}, ");
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
                Flight currentFlight = flights.Find(x => x.Number == flightNumber);

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

                _airline.RemoveFlight(currentFlight);
                currentFlight.UpdatePassengers(number);
                _airline.AddFlight(new Flight(direction, time, number, destination, gate, status, economyClassPrice,
                                  currentFlight.MaxNumberOfPassengers, currentFlight.Passengers));
                Console.WriteLine("\n The flight info has been successfully updated.");
            }
        }

        public override void RemoveFlight()
        {
            List<Flight> flights = _airline.Flights;
            List<int> flightNumbers = new List<int>();
            bool isOkParse, isCorrectNumber;
            int flightNumber;

            if (flights.Count == 0)
            {
                Console.WriteLine("\n There are no flights to remove.");
            }
            else {
                Console.Write($"\n There are {flights.Count} flights: ");
                foreach (Flight flight in flights)
                {
                    flightNumbers.Add(flight.Number);
                    Console.Write($"{flight.Number:d4}, ");
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
                    for (i = 0; i < flights.Count; i++)
                    {
                        if (flights[i] != null && flights[i].Number == flightNumber)
                        {
                            _flightPrinter.Print(flights[i]);
                            _flightPrinter.Print(flights[i].Passengers);
                            break;
                        }
                    }
                    Console.WriteLine("\n Press any key to remove the flight.");
                    Console.ReadKey();
                    _airline.RemoveFlight(flights[i]);
                    Console.WriteLine("\n The flight has been removed.");
                }
                else
                {
                    Console.WriteLine(" The flight hasn't been found.");
                }
            }
        }

        #endregion

        public override void FindFlightsByEconomyClassPrice()
        {
            List<Flight> flights = _airline.Flights;
            List<Flight> properFlights = new List<Flight>();

            ConsoleKey keyPressed;
            decimal upperPrice;
            bool isFirstMatch, isFound, isValidInput;

            do
            {
                properFlights.Clear();
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

                foreach (var flight in flights)
                {
                    if (flight.EconomyClassPrice <= upperPrice)
                    {
                        if (isFirstMatch)
                        {
                            isFound = true;
                            isFirstMatch = false;
                        }
                        properFlights.Add(flight);
                    }
                }

                if (isFound)
                {
                    Console.Write(" Some flights've been found. Press any key to show the information. ");
                    Console.ReadKey();
                    Console.WriteLine();
                    _flightPrinter.Print(properFlights);
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

    }
}
