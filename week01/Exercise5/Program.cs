using System;
using System.ComponentModel.DataAnnotations;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();

        string userName = PromptUserName();
        int userNumber = PromptUserNumber();

        int squaredNumber = SquareNumber(userNumber);

        DisplayResult(userName, squaredNumber);
    }

    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }
    static string PromptUserName()
    {
        Console.WriteLine("enter a user name");
        string userName = Console.ReadLine();
        return userName;
    }

    static int PromptUserNumber()
    {
        Console.WriteLine("enter a user number");
        int PromptUserNumber = int.Parse(Console.ReadLine());

        return PromptUserNumber;
    }

    static int SquareNumber(int X)
    {
        return X * X;
    }
    static void DisplayResult(string userName, int squaredNumber)
    {
        Console.WriteLine($"Hello {userName} your squared number is {squaredNumber}.");
    }
}
