using System;
using System.IO;

public class Journal()
{
    // display method
    public void display()
    {
        string path = "journal.txt";

        if (File.Exists(path))
        {
            string[] entries = File.ReadAllLines(path);
            foreach (string entry in entries)
            {
                Console.WriteLine(entry);

            }

        }
        else
        {
            Console.WriteLine("No journal entries found.");
            return;
        }

    }

    // write entry method
    public void WriteEntry()
    {
        // create a random string
        Random r = new Random();
        DateTime time = DateTime.Now;
        int index = r.Next(0, 7);
        string[] random = { "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "How are you doing today",
        "What is one thing I learned today?" };
        string question = random[index];


        Console.WriteLine(question);
        string entry = Console.ReadLine();

        // dealing with files

        if (File.Exists("journal.txt"))
        {
            File.AppendAllText("journal.txt", $"\n{time}\n{question}\n{entry}\n");
        }
        else
        {
            File.WriteAllText("journal.txt", $"{time}\n{question}\n{entry}");
            Console.WriteLine("File created and entry saved.");
        }

        // For demonstration, we just print it back
        Console.WriteLine($"You wrote: {entry}");
    }
    // load journal from file method
    public void LoadFromFile()
    {
        Console.WriteLine("Enter the file path to load the journal from:");
        string filePath = Console.ReadLine();
        if (File.Exists(filePath))
        {
            string[] entries = File.ReadAllLines(filePath);
            foreach (string entry in entries)
            {
                Console.WriteLine(entry);
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}