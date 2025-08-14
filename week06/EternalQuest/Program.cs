using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public virtual string GetDetailsString()
    {
        return $"{_name} ({_description})";
    }

    public abstract string GetStringRepresentation();
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override int RecordEvent()
    {
        _isComplete = true;
        return Points;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{Name}|{Description}|{Points}|{_isComplete}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) 
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return Points; // Always gives points, never completes
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{Name}|{Description}|{Points}";
    }
}

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _bonus;
    private int _amountCompleted;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonus = bonus;
        _amountCompleted = 0;
    }

    public override int RecordEvent()
    {
        _amountCompleted++;
        if (_amountCompleted >= _targetCount)
        {
            return Points + _bonus;
        }
        return Points;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _targetCount;
    }

    public override string GetDetailsString()
    {
        return base.GetDetailsString() + $" -- Completed {_amountCompleted}/{_targetCount}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{Name}|{Description}|{Points}|{_targetCount}|{_bonus}|{_amountCompleted}";
    }
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void Start()
    {
        while (true)
        {
            Console.WriteLine($"\nYou have {_score} points.\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalDetails();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string typeChoice = Console.ReadLine();

        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string description = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        switch (typeChoice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("Enter target completions: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid type selected.");
                break;
        }
    }

    private void ListGoalDetails()
    {
        Console.WriteLine("\nYour goals are:");
        int index = 1;
        foreach (var goal in _goals)
        {
            string checkBox = goal.IsComplete() ? "[X]" : "[ ]";
            Console.WriteLine($"{index}. {checkBox} {goal.GetDetailsString()}");
            index++;
        }
    }

    private void SaveGoals()
    {
        Console.Write("Enter filename: ");
        string file = Console.ReadLine();

        using (StreamWriter sw = new StreamWriter(file))
        {
            sw.WriteLine(_score);
            foreach (var goal in _goals)
            {
                sw.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("Goals saved!");
    }

    private void LoadGoals()
    {
        Console.Write("Enter filename: ");
        string file = Console.ReadLine();

        if (!File.Exists(file))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _goals.Clear();
        string[] lines = File.ReadAllLines(file);
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            string type = parts[0];
            if (type == "SimpleGoal")
            {
                var g = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                if (bool.Parse(parts[4]))
                    g.RecordEvent(); // mark as completed
                _goals.Add(g);
            }
            else if (type == "EternalGoal")
            {
                _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
            }
            else if (type == "ChecklistGoal")
            {
                var g = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                int completedTimes = int.Parse(parts[6]);
                for (int c = 0; c < completedTimes; c++) g.RecordEvent();
                _goals.Add(g);
            }
        }
        Console.WriteLine("Goals loaded!");
    }

    private void RecordEvent()
    {
        Console.WriteLine("Select a goal to record:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].Name}");
        }
        int choice = int.Parse(Console.ReadLine()) - 1;

        if (choice >= 0 && choice < _goals.Count)
        {
            int points = _goals[choice].RecordEvent();
            _score += points;
            Console.WriteLine($"Event recorded! You earned {points} points.");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager gm = new GoalManager();
        gm.Start();
    }
}
