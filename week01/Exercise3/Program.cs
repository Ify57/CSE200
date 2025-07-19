using System;

class Program
{
    static void Main(string[] args)
    {
        Random rand = new Random();
        // whats your magic number?
        // Console.WriteLine("What's your magic number?");
        // string magicNumber = Console.ReadLine();
        Console.WriteLine("What's your Guess?");
        string guess = Console.ReadLine();
        int magic = rand.Next(1, 10);
        int gues = int.Parse(guess);

        while (true)
        {
            if (gues < magic)
            {
                Console.WriteLine("Your guess is too low.");
            }
            else if (gues > magic)
            {
                Console.WriteLine("Your guess is too high.");
            }
            else
            {
                Console.WriteLine("Congratulations! You've guessed the magic number!");
                break;
            }

            Console.WriteLine("What's your Guess?");
            guess = Console.ReadLine();
            gues = int.Parse(guess);
        }
        {

        }

    }
}