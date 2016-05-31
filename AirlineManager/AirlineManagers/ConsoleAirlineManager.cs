using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

using KRZHK.AirlineLibrary;
using KRZHK.AirlineLibrary.Enums;
using KRZHK.AirlineLibrary.FlightPrinters;
using KRZHK.AirlineManager.FlightsManagers;
using KRZHK.AirlineManager.PassengersManagers;

namespace KRZHK.AirlineManager.AirlineManagers
{
    class ConsoleAirlineManager
    {
        IFlightPrinter _flightPrinter;
        Airline _airline;
        FlightsManager _flightsManager;
        PassengersManager _passengersManager;

        public ConsoleAirlineManager(IFlightPrinter printer, Airline airline)
        {
            _flightPrinter = printer;
            _airline = airline;
            _flightsManager = new ConsoleFlightsManager(airline, printer);
            _passengersManager = new ConsolePassengersManager(airline, printer);
        }
        
        #region Form menus

        void ShowMenuHeader(string menuTitle)
        {
            int windowWidth = 100;

            try
            {
                windowWidth = int.Parse(ConfigurationManager.AppSettings["WindowWidth"]);
            }
            catch
            {
                Console.WriteLine("An error occurred while reading from .config. Check the file up.");
            }

            int numberOfSpaces = 3;
            int starsLengthLeft = (windowWidth - 2 - menuTitle.Length - 2 * numberOfSpaces) / 2;
            int starsLengthRight = windowWidth - 2 - menuTitle.Length - 2 * numberOfSpaces - starsLengthLeft;
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

        void Operate()
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
                            _flightPrinter.Print(_airline.Flights);
                            break;
                        case 2:
                            ShowMenuHeader("view the passengers of a flight");
                            _passengersManager.ViewFlightPassengers();
                            break;
                        case 3:
                            ShowMenuHeader("find passengers by name");
                            _passengersManager.FindPassengersByName();
                            break;
                        case 4:
                            ShowMenuHeader("find passengers by passport number");
                            _passengersManager.FindPassengersByPassportNumber();
                            break;
                        case 5:
                            ShowMenuHeader("find flights by the economy class price");
                            _flightsManager.FindFlightsByEconomyClassPrice();
                            break;
                        case 6:
                            ShowMenuHeader("add a flight");
                            _flightsManager.AddFlight();
                            break;
                        case 7:
                            ShowMenuHeader("edit a flight");
                            _flightsManager.EditFlight();
                            break;
                        case 8:
                            ShowMenuHeader("remove a flight");
                            _flightsManager.RemoveFlight();
                            break;
                        case 9:
                            ShowMenuHeader("add a passenger");
                            _passengersManager.AddPassenger();
                            break;
                        case 10:
                            ShowMenuHeader("edit a passenger");
                            _passengersManager.EditPassenger();
                            break;
                        case 11:
                            ShowMenuHeader("remove a passenger");
                            _passengersManager.RemovePassenger();
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

        void ConfigureOutput()
        {
            try
            {
                string culture = ConfigurationManager.AppSettings["Culture"];
                int windowHeight = int.Parse(ConfigurationManager.AppSettings["WindowHeight"]);
                int windowWidth = int.Parse(ConfigurationManager.AppSettings["WindowWidth"]);

                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
                Console.WindowHeight = windowHeight;
                Console.WindowWidth = Console.BufferWidth = windowWidth;
            }
            catch
            {
                Console.WriteLine("An error occurred while reading from .config. Check the file up.");
            }
        }

        public void Manage()
        {
            ConfigureOutput();

            do
            {
                Console.Clear();
                ShowMainMenu();
                Operate();
                Console.WriteLine("\n Press ESC to quit or any other key to go back to main menu.");
                Console.Write(" ");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        #endregion
    }
}
