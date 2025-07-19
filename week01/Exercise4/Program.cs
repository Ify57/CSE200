using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {


        while (true)
        {
            Console.WriteLine("Enter A list of Number type 0 to stop");
            string list = Console.ReadLine();
            int number = int.Parse(list);
            List<int> ages = new List<int>();
            if (number != 0)
            {
                ages.Add(number);
            }
            else
            {
                Console.WriteLine(ages.Count);
                break;
            }
            
        }
    }
}