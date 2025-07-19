using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {


        while (true)
        {
            Console.WriteLine("Enter A Number");
            string list = Console.ReadLine();
            int number = int.Parse(list);
            List<int> ages = new List<int>();
            if (number != 0)
            {
                ages.Add(number);
            }
            else
            {
                break;
            }
            
        }
    }
}