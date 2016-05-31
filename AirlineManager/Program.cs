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
            #region Tracing
            //TraceSource traceSource = new TraceSource("airlineTraceSource");
            //TextWriterTraceListener textTraceListener = new TextWriterTraceListener("trace.txt");
            //traceSource.Switch = new SourceSwitch("arilineSourceSwitch");
            //traceSource.Switch.Level = SourceLevels.All;

            //textTraceListener.TraceOutputOptions = TraceOptions.DateTime;
            //traceSource.Listeners.Clear();
            //traceSource.Listeners.Add(textTraceListener);
            //Trace.AutoFlush = true;

            //traceSource.TraceEvent(TraceEventType.Information, 0, "Application started.");
            #endregion Tracing

            logger.Info("Application started.");

            AirlineFactory airlineFactory = new AirlineFactory();
            Airline zachepilovkaAir = airlineFactory.CreateAndPopulateAirline(7);
            
            IFlightPrinter flightPrinter = new ConsoleFlightPrinter();
            ConsoleAirlineManager zachepilovkaAirManager = new ConsoleAirlineManager(flightPrinter, zachepilovkaAir);
            zachepilovkaAirManager.Manage();

            //Passenger[] passengers = new Passenger[10];
            //passengers[0]  = new Passenger("Truman", "Capote", "American", "GG 554493", DateTime.Now.AddDays(-5000),
            //                            Sex.Male, new FlightTicket { Class = TicketClass.Business, Price = 350 }, 2222);
            //zachepilovkaAir.AddFlight(new Flight(FlightDirection.Arrival, DateTime.Now, 3333, "Ulaanbaatar", AirportGate.B1, FlightStatus.Departed, 350, 25, passengers));

            //flightPrinter.PrintFlightsInfo(zachepilovkaAir.Flights);

            logger.Info("Application ended.");
        }
    }
}
