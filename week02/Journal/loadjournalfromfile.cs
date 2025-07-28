using System;
using System.IO;


public class LoadJournalFromFile()
{
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