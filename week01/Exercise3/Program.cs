using System;

class Program
{
    static void Main(string[] args)
    {
        // loops
        int x = 20;
        while (x >= 20)
        {
            Console.WriteLine($"{x++}");
            if (x++ > 30)
            {
                break;
            }

        }
    }
}