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
        static XmlDocument doc;
        static int startYear = 2017;
        static int endYear = 2021;
        static void Main(string[] args)
        {
            Console.WriteLine("World Economic Data");
            Console.WriteLine("======================");
            try
            {
                doc = new();
                doc.Load(XmlFile);

                while (true)
                {
                    DisplayMenu();
                }
            }
            catch (XmlException err)
            {
                Console.WriteLine($"\nXML ERROR: {err.Message}");
            }
            catch (XPathException err)
            {
                Console.WriteLine($"\nXPATH ERROR: {err.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERROR:  {ex.Message}");
            }
        }

        public static void DisplayMenu()
        {
            Console.WriteLine($"'Y' to adjust the range of years (currently {startYear} to {endYear})");
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
                PrintRegionalSummary(startYear, endYear);
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
            startYear = string.IsNullOrEmpty(startYearInput) ? 2017 : int.Parse(startYearInput);
            if (startYear < 1970 || startYear > 2021)
            {
                Console.WriteLine("ERROR: Starting year must be an integer between 1970 and 2021.");
                return;
            }
            Console.Write("Ending year (1970 to 2021): ");
            string endYearInput = Console.ReadLine();
            endYear = string.IsNullOrEmpty(endYearInput) ? 2021 : int.Parse(endYearInput);
            if (endYear < 1970 || endYear > 2021 || endYear < startYear)
            {
                Console.WriteLine("ERROR: Ending year must be an integer between " + startYear + " and " + startYear + 5);
                return;
            }
            Console.WriteLine($"Year range set to {startYear} to {endYear}.");
        }

        public static void PrintRegionalSummary(int startYear, int endYear)
        {
            Console.WriteLine("Select a region by number as shown below...\n");
            XmlNodeList regions = doc.SelectNodes("/global_economies/region");
            int count = 1;
            foreach (XmlNode region in regions)
            {
                string rname = region.Attributes["rname"].Value;
                Console.WriteLine(count + ". " + rname);
                count++;
            }

            Console.Write("Enter a region #: ");
            string input = Console.ReadLine();
            int regionNumber = int.Parse(input);
            if (regionNumber < 1 || regionNumber > regions.Count)
            {
                Console.WriteLine("ERROR: Invalid region number.");
                return;
            }

            XmlNode selectedRegion = regions[regionNumber - 1];
            string regionName = selectedRegion.Attributes["rname"].Value;

            Console.WriteLine();
            Console.WriteLine("Economic Information for " + regionName);
            Console.WriteLine("-------------------------------");
            Console.WriteLine();

            List<XmlNode> yearNodesInRange = new List<XmlNode>();
            List<string> years = new List<string>();
            foreach (XmlNode yearNode in selectedRegion.SelectNodes("year"))
            {
                string yearStr = yearNode.Attributes["yid"].Value;
                int year = int.Parse(yearStr);
                if (year >= startYear && year <= endYear)
                {
                    years.Add(yearStr);
                    yearNodesInRange.Add(yearNode);
                }
            }

            Console.Write("          Economic Metric");
            foreach (string y in years)
            {
                Console.Write("    " + y);
            }
            Console.WriteLine();

            Console.Write("            Inflation CPI");
            foreach (XmlNode yearNode in yearNodesInRange)
            {
                XmlNode infNode = yearNode.SelectSingleNode("inflation");
                string val = infNode?.Attributes["consumer_prices_percent"]?.Value ?? "-";
                Console.Write("    " + (val == "" ? "-" : val));
            }
            Console.WriteLine();

            Console.Write("            Inflation GDP");
            foreach (XmlNode yearNode in yearNodesInRange)
            {
                XmlNode infNode = yearNode.SelectSingleNode("inflation");
                string val = infNode?.Attributes["gdp_deflator_percent"]?.Value ?? "-";
                Console.Write("    " + (val == "" ? "-" : val));
            }
            Console.WriteLine();

            Console.Write("          Real Interest %");
            foreach (XmlNode yearNode in yearNodesInRange)
            {
                XmlNode intNode = yearNode.SelectSingleNode("interest_rates");
                string val = intNode?.Attributes["real"]?.Value ?? "-";
                Console.Write("    " + (val == "" ? "-" : val));
            }
            Console.WriteLine();

            Console.Write("       Lending Interest %");
            foreach (XmlNode yearNode in yearNodesInRange)
            {
                XmlNode intNode = yearNode.SelectSingleNode("interest_rates");
                string val = intNode?.Attributes["lending"]?.Value ?? "-";
                Console.Write("    " + (val == "" ? "-" : val));
            }
            Console.WriteLine();

            Console.Write("       Deposit Interest %");
            foreach (XmlNode yearNode in yearNodesInRange)
            {
                XmlNode intNode = yearNode.SelectSingleNode("interest_rates");
                string val = intNode?.Attributes["deposit"]?.Value ?? "-";
                Console.Write("    " + (val == "" ? "-" : val));
            }
            Console.WriteLine();

            Console.Write("       Unemployment NTL %");
            foreach (XmlNode yearNode in yearNodesInRange)
            {
                XmlNode unempNode = yearNode.SelectSingleNode("unemployment_rates");
                string val = unempNode?.Attributes["national_estimate"]?.Value ?? "-";
                Console.Write("    " + (val == "" ? "-" : val));
            }
            Console.WriteLine();

            Console.Write("       Unemployment IPO %");
            foreach (XmlNode yearNode in yearNodesInRange)
            {
                XmlNode unempNode = yearNode.SelectSingleNode("unemployment_rates");
                string val = unempNode?.Attributes["modeled_ILO_estimate"]?.Value ?? "-";
                Console.Write("    " + (val == "" ? "-" : val));
            }
            Console.WriteLine();
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

            XmlNodeList countryNames = doc.SelectNodes("/global_economies/region/@rname");
        }
    }
}
