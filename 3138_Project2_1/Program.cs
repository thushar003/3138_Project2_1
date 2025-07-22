/**
 * File name: Program.cs
 * Authors: Thushar Joseph Joji, Manny Bagheri
 * Date: July 19, 2025
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;           // XmlDocument (DOM) class
using System.Xml.XPath;     // XPathNavigator (XPath) class

namespace _3138Project2_1
{
    class Program
    {
        const string XmlFile = @"..\..\..\global_economies.xml";

        static void Main(string[] args)
        {
            Console.WriteLine("World Economic Data");
            Console.WriteLine("======================");
            try
            {
                XmlDocument doc = new();

                while (true)
                {
                    DisplayMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERROR:  {ex.Message}");
            }
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("'Y' to adjust the range of years (currently 2017 to 2021)");
            Console.WriteLine("'R' to print a regional summary");
            Console.WriteLine("'M' to print a specific metric for all regions");
            Console.WriteLine("'X' to exit the program'");
            Console.Write("Your selection: ");

            string input = Console.ReadLine();

            if (input.ToLower() == "y")
            {
                AdjustYearRange();
            }
            else if (input.ToLower() == "r")
            {
                PrintRegionalSummary();
            }
            else if (input.ToLower() == "m")
            {
                PrintMetricForAllRegions();
            }
            else if (input.ToLower() == "x")
            {
                Console.WriteLine("\nAll done!");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        }

        public static void AdjustYearRange()
        {
            Console.Write("Starting year (1970 to 2021): ");
            string startYearInput = Console.ReadLine();
            //Maybe modify this section later so it loops until valid input is entered.
            int startYear = string.IsNullOrEmpty(startYearInput) ? 2017 : int.Parse(startYearInput);
            if (startYear < 1970 || startYear > 2021)
            {
                Console.WriteLine("ERROR: Starting year must be an integer between 1970 and 2021.");
                return;
            }
            Console.Write("Ending year (1970 to 2021): ");
            string endYearInput = Console.ReadLine();
            int endYear = string.IsNullOrEmpty(endYearInput) ? 2021 : int.Parse(endYearInput);
            if (endYear < 1970 || endYear > 2021 || endYear < startYear)
            {
                Console.WriteLine("ERROR: Ending year must be an integer between " + startYear + " and " + startYear + 5);
                return;
            }
            Console.WriteLine($"Year range set to {startYear} to {endYear}.");
        }

        public static void PrintRegionalSummary()
        {
            //TO-DO
            Console.WriteLine("Select a region by number as shown below...\n");

            Console.Write("Enter a region #:");
        }

        public static void PrintMetricForAllRegions()
        {
            //TO-DO
            Console.WriteLine("Select a metric by number as shown below...");
            Console.WriteLine("1. Inflation CPI");
            Console.WriteLine("2. Inflation GDP");
            Console.WriteLine("3. Real Interest %");
            Console.WriteLine("4. Lending Interest %");
            Console.WriteLine("5. Deposit Interest %");
            Console.WriteLine("6. Unemployment NTL %");
            Console.WriteLine("7. Unemployment IPO %");
            Console.Write("Enter a metric #");
        }
    }
}
