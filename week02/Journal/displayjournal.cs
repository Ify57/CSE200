using System;
using System.IO;
public class displayJournal()
{
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
    
}