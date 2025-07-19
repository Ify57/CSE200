using System;

class Program
{
    static void Main(string[] args)
    {
        // condtional staement
        Console.WriteLine("whats your Grade?");
        string number = Console.ReadLine();

        int grade = int.Parse(number);
        if (grade >= 90)
        {
            Console.WriteLine("A");
        } else if (grade >= 80)
        {
            Console.WriteLine("B");
        } else if (grade >= 70)
        {
            Console.WriteLine("C");
        } else if (grade >= 60)
        {
            Console.WriteLine("D");
        } else
        {
            Console.WriteLine("F");
        }
        {

        }
    }
}