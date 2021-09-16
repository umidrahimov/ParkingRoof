using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingRoof
{
    class Program
    {
        public class Options
        {

            [Option('c', "cars", Required = true, HelpText = "Array of occupied spots (Numbers separated by comma)")]
            public string cars { get; set; }

            [Option('k', "covered", Required = true, HelpText = "Number of cars to be covered by roof")]
            public int k { get; set; }
        }

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Parser.Default.ParseArguments<Options>(args)
                  .WithParsed<Options>(o =>
                  {

                      string[] parkingSpots = o.cars.Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);

                      int[] occupiedSpot = Array.ConvertAll(parkingSpots, int.Parse);

                      int minRoofLength = carParkingRoof(occupiedSpot, o.k);

                      Console.WriteLine(minRoofLength);

                      Console.ReadKey();

                  });
            }
            else
            {
                Console.WriteLine("Please enter the parking spots where cars are parked\r");

                string input = Console.ReadLine();

                string[] parkingSpots = input.Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);

                int[] occupiedSpot = Array.ConvertAll(parkingSpots, int.Parse);

                Console.WriteLine("Please enter the number of cars that have to be covered by the roof\r");

                int carsCovered = int.Parse(Console.ReadLine());

                int minRoofLength = carParkingRoof(occupiedSpot, carsCovered);

                Console.WriteLine(minRoofLength);

                Console.ReadKey();

            }
        }

        private static int carParkingRoof(int[] cars, int k)
        {
            Array.Sort(cars);

            int minRoofLength = cars.Max() - cars.Min() + 1;

            for (int i = 0; i < cars.Length - k; i++)
            {
                int roofLength = cars[i + k - 1] - cars[i] + 1;

                if (roofLength < minRoofLength)
                {
                    minRoofLength = roofLength;
                }
            }
            return minRoofLength;
        }
    }
}
