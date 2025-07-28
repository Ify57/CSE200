using System;
using System.IO;

public class write_entry
{
    public void WriteEntry()
    {
        // create a random string
        Random r = new Random();
        DateTime time =  DateTime.Now;
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
}