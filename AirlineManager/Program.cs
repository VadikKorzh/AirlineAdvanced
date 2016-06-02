using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KRZHK.AirlineLibrary;
using KRZHK.AirlineLibrary.FlightPrinters;
using KRZHK.AirlineManager.AirlineManagers;
using System.Diagnostics;
using NLog;

namespace KRZHK.AirlineManager
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Application started.");

            AirlineFactory airlineFactory = new AirlineFactory();
            // create an airline with 7 preset flights
            Airline zachepilovkaAir = airlineFactory.Create(7);
            // create a printer for console window
            IFlightPrinter flightPrinter = new ConsoleFlightPrinter();
            // create an airline manager
            ConsoleAirlineManager zachepilovkaAirManager = new ConsoleAirlineManager(flightPrinter, zachepilovkaAir);

            zachepilovkaAirManager.Manage();

            logger.Info("Application ended.");
        }
    }
}
