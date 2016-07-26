    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter an IP address.");
                return 1;
            }
            string ip = args[0];
            List<Tuple<int, int>> coordList = new List<Tuple<int, int>>();
            coordList = GetCoordinatesFromIp(ip);
            double distance = 0.0;
            for (int origin = 0; origin < coordList.Count - 1; origin++)
            {     
                Tuple<int, int> source = coordList[origin];
                Tuple<int, int> destination = coordList[origin + 1];
                distance += GetDistance(source, destination);
            }
            Console.WriteLine("distance: {0}cm", distance = Math.Round(distance, 2));
            return 0;
        }

        //returns a list of (row, col) coordinates from each keystroke
        public static List<Tuple<int, int>> GetCoordinatesFromIp(string ip)
        {
            char[] keyStrokes = ip.ToCharArray();
            List<Tuple<int, int>> coordList = new List<Tuple<int, int>>();
            Dictionary<char, Tuple<int, int>> keypadCoordDict = new Dictionary<char, Tuple<int, int>>();
            keypadCoordDict = GenerateDict();
            foreach (char key in keyStrokes)
                coordList.Add(keypadCoordDict[key]);
        
            return coordList;
        }

        //gen dict where char is a the key and the value is a tuple of (row, col)
        public static Dictionary<char, Tuple<int, int>> GenerateDict()
        {
            Dictionary<char, Tuple<int, int>> dict = new Dictionary<char, Tuple<int, int>>();
            char[] keypad = "123456789.0".ToCharArray();
            int count = 0;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (row == 3 && col == 2)
                        break;
                    dict.Add(keypad[count], Tuple.Create(row, col));
                    count++;
                }
            }
            return dict;
        }

        //returns true if the delta is horizontal or vertical to the origin
        public static direction DetermineDirection(Tuple<int, int> origin, Tuple<int, int> destination)
        {
            //compare the row of origin with row of dest. if they are the same, its a horizontal comparison
            if (origin.Item1 == destination.Item1)
                return direction.horizontal;
            
            else if (origin.Item2 == destination.Item2)
                return direction.vertical;
        
            //else it has some diag property
             return direction.diagonal;
        }

        //1 cm per key horizontal or vertifical. sqrt of sides otherwise
        public static double GetDistance(Tuple<int, int> origin, Tuple<int, int> destination)
        {
            direction d = DetermineDirection(origin, destination);

            if (d == direction.horizontal)
                return Math.Abs(origin.Item2 - destination.Item2);
        
            else if(d == direction.vertical)
                return Math.Abs(origin.Item1 - destination.Item1);
      
            else
            {
                //get horizontal movement
                int horizontal = Math.Abs(origin.Item2 - destination.Item2);
                int vertical = Math.Abs(origin.Item1 - destination.Item1);
                return Math.Sqrt(Math.Pow(horizontal, 2) + Math.Pow(vertical, 2)); 
            }
        }
    }

    public enum direction
    {
        horizontal,
        vertical,
        diagonal
    }