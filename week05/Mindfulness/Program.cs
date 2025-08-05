using System;
using System.Collections.Generic;
using System.Diagnostics;


public abstract class Activity
{
    private string _name;
    private string _description;
    private int _durationSeconds; 
    private static readonly int _preparePauseSeconds = 3;

    // Constructor requires name and description -> enforces abstraction/initialization
    protected Activity(string name, string description)
    {
        _name = name ?? string.Empty;
        _description = description ?? string.Empty;
        _durationSeconds = 0;
    }

    // Public accessor for name (read-only)
    public string Name => _name;

    // Set duration (public method instead of exposing member directly)
    public void SetDuration(int seconds)
    {
        if (seconds < 0) seconds = 0;
        _durationSeconds = seconds;
    }

    // Get duration (public getter)
    public int GetDuration() => _durationSeconds;

    // Standard starting message
    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"*** {_name} ***");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("Enter the duration of the activity in seconds: ");
        // (Setting duration is handled by Program to validate input)
    }

    // standard "prepare" message & brief pause with spinner
    public void PrepareToBegin()
    {
        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ShowSpinner(_preparePauseSeconds);
    }

    // standard ending message
    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        ShowSpinner(2);
        Console.WriteLine($"You have completed the {_name} for {GetDuration()} seconds.");
        ShowSpinner(2);
    }

    // Show spinner animation for No of seconds
    protected void ShowSpinner(int seconds)
    {
        var spinner = new[] { '|', '/', '-', '\\' };
        var sw = Stopwatch.StartNew();
        int i = 0;
        while (sw.Elapsed.TotalSeconds < seconds)
        {
            Console.Write(spinner[i % spinner.Length]);
            Thread.Sleep(250);
            Console.Write('\b');
            i++;
        }
        sw.Stop();
    }

    // Show countdown (numeric) for N seconds
    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i >= 1; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b"); // erase digit (works for 1-digit; ok for simplicity)
        }
    }

    // Entry point for activity behavior; template method pattern
    public void Run()
    {
        DisplayStartingMessage();
        // Caller (Program) should set duration via SetDuration before calling Run
        PrepareToBegin();
        Execute(); // implemented by derived classes
        DisplayEndingMessage();
    }

    // Derived classes implement their specific behavior here
    protected abstract void Execute();
}

// Breathing activity: alternate breathe in/out with countdowns until duration reached
public class BreathingActivity : Activity
{
    // Provide a constructor that calls base
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    { }

    protected override void Execute()
    {
        int totalSeconds = GetDuration();
        if (totalSeconds <= 0) return;

        var sw = Stopwatch.StartNew();
        bool inhale = true;
        while (sw.Elapsed.TotalSeconds < totalSeconds)
        {
            if (inhale)
            {
                Console.WriteLine();
                Console.Write("Breathe in... ");
                // show a 4-second countdown (can be shorter if near end)
                int secondsLeft = Math.Min(4, totalSeconds - (int)sw.Elapsed.TotalSeconds);
                if (secondsLeft <= 0) break;
                ShowCountdown(secondsLeft);
            }
            else
            {
                Console.WriteLine();
                Console.Write("Breathe out... ");
                int secondsLeft = Math.Min(6, totalSeconds - (int)sw.Elapsed.TotalSeconds);
                if (secondsLeft <= 0) break;
                ShowCountdown(secondsLeft);
            }
            inhale = !inhale;
        }
        sw.Stop();
    }
}

// Reflection activity: show a prompt then random reflection questions while time remains
public class ReflectionActivity : Activity
{
    private readonly List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

    private readonly List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

    private readonly Random _rand = new Random();

    public ReflectionActivity() : base(
        "Reflection Activity",
        "This activity will help you reflect on times in your life when you have shown strength and resilience. It helps you recognize the power you have and how you can use it.")
    { }

    protected override void Execute()
    {
        int totalSeconds = GetDuration();
        if (totalSeconds <= 0) return;

        // Choose and display a random prompt
        var prompt = _prompts[_rand.Next(_prompts.Count)];
        Console.WriteLine();
        Console.WriteLine("Prompt:");
        Console.WriteLine($"  {prompt}");
        Console.WriteLine();

        // Small pause before questions
        ShowSpinner(3);

        var sw = Stopwatch.StartNew();
        while (sw.Elapsed.TotalSeconds < totalSeconds)
        {
            // choose a random question
            var q = _questions[_rand.Next(_questions.Count)];
            Console.WriteLine();
            Console.WriteLine($"  - {q}");
            // pause for reflection (show spinner for e.g., 5 seconds or remaining time)
            int remaining = Math.Max(0, totalSeconds - (int)sw.Elapsed.TotalSeconds);
            if (remaining <= 0) break;
            int pause = Math.Min(5, remaining);
            ShowSpinner(pause);
        }
        sw.Stop();
    }
}

// Listing activity: show a prompt, give a countdown, then accept as many user inputs as possible until time expires
public class ListingActivity : Activity
{
    private readonly List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

    private readonly Random _rand = new Random();

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    { }

    protected override void Execute()
    {
        int totalSeconds = GetDuration();
        if (totalSeconds <= 0) return;

        var prompt = _prompts[_rand.Next(_prompts.Count)];
        Console.WriteLine();
        Console.WriteLine("List prompt:");
        Console.WriteLine($"  {prompt}");
        Console.WriteLine();

        // countdown before starting
        Console.WriteLine("You may begin in...");
        ShowCountdown(5);
        Console.WriteLine();
        Console.WriteLine($"Start listing! You have {totalSeconds} seconds. (Press Enter after each item)");

        var entries = new List<string>();
        var sw = Stopwatch.StartNew();

        // Loop until time elapsed. Use Task-based ReadLine with timeout so input doesn't block past time.
        while (sw.Elapsed.TotalSeconds < totalSeconds)
        {
            int remainingMs = totalSeconds * 1000 - (int)sw.Elapsed.TotalMilliseconds;
            if (remainingMs <= 0) break;

            // Start a task to read input
            Task<string> readTask = Task.Run(() => Console.ReadLine());
            bool completed = readTask.Wait(remainingMs);
            if (completed)
            {
                string input = readTask.Result?.Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    entries.Add(input);
                }
            }
            else
            {
                // time is up; don't wait for blocked ReadLine
                break;
            }
        }
        sw.Stop();

        Console.WriteLine();
        Console.WriteLine($"You listed {entries.Count} item(s).");
        if (entries.Count > 0)
        {
            Console.WriteLine("Your items:");
            foreach (var e in entries) Console.WriteLine($"  - {e}");
        }
    }
}

// Program with menu system and orchestration
class Program
{
    static void Main(string[] args)
    {
        bool quit = false;
        while (!quit)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness App");
            Console.WriteLine("================");
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1) Breathing Activity");
            Console.WriteLine("2) Reflection Activity");
            Console.WriteLine("3) Listing Activity");
            Console.WriteLine("4) Quit");
            Console.Write("Enter selection (1-4): ");

            string choice = Console.ReadLine()?.Trim();
            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again.");
                    Console.ReadKey();
                    continue;
            }

            if (activity != null)
            {
                // Ask for duration
                int duration = PromptForDuration();
                activity.SetDuration(duration);
                activity.Run();

                Console.WriteLine();
                Console.WriteLine("Press any key to return to the menu.");
                Console.ReadKey();
            }
        }
    }

    // Helper to prompt for a positive integer duration
    private static int PromptForDuration()
    {
        while (true)
        {
            Console.Write("Enter duration in seconds (positive integer): ");
            string s = Console.ReadLine()?.Trim();
            if (int.TryParse(s, out int seconds) && seconds >= 0)
            {
                return seconds;
            }
            Console.WriteLine("Invalid input. Please enter 0 or a positive integer.");
        }
    }
}