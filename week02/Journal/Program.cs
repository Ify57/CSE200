using System;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("""
        1) Write a new entry
        2) Display the journal
        3) Save the journal to a file
        4) Load the journal from a file
        5) Exit
        """);
        Journal call = new Journal();

        string choice = Console.ReadLine();
        int number = int.Parse(choice);
        if (number == 1)        {
            while (true)
            {
                // Journal call = new Journal();
                call.WriteEntry();
            }
        }
        else if (number == 2)
        {
            // displayJournal dis = new displayJournal();
            call.display();

        }
        // else if (number == 3)
        // {
        //     SaveJournalToFile save = new SaveJournalToFile();
        //     save.SaveToFile();
        // }

        else if (number == 4)
        {
            call.LoadFromFile();
        }
        else if (number == 5)
        {
            Console.WriteLine("Exiting the program.");
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }
    }
}