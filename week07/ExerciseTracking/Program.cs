using System;
using System.Collections.Generic;

public abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime Date => _date;
    public int Minutes => _minutes;

    public abstract double GetDistance(); // km or miles depending on activity
    public abstract double GetSpeed();    // speed per hour
    public abstract double GetPace();     // minutes per km or mile

    public virtual string GetSummary()
    {
        return $"{_date:dd MMM yyyy} {GetType().Name} ({_minutes} min) - " +
               $"Distance: {GetDistance():0.0}, Speed: {GetSpeed():0.0}, Pace: {GetPace():0.0}";
    }
}

public class Running : Activity
{
    private double _distanceKm;

    public Running(DateTime date, int minutes, double distanceKm) : base(date, minutes)
    {
        _distanceKm = distanceKm;
    }

    public override double GetDistance() => _distanceKm;
    public override double GetSpeed() => (_distanceKm / Minutes) * 60;
    public override double GetPace() => Minutes / _distanceKm;
}

public class Cycling : Activity
{
    private double _speedKph;

    public Cycling(DateTime date, int minutes, double speedKph) : base(date, minutes)
    {
        _speedKph = speedKph;
    }

    public override double GetDistance() => (_speedKph * Minutes) / 60;
    public override double GetSpeed() => _speedKph;
    public override double GetPace() => 60 / _speedKph;
}

public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => _laps * 50 / 1000.0; // km
    public override double GetSpeed() => (GetDistance() / Minutes) * 60;
    public override double GetPace() => Minutes / GetDistance();
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2025, 8, 13), 30, 5.0),  // 5 km run
            new Cycling(new DateTime(2025, 8, 12), 40, 20.0), // 20 kph
            new Swimming(new DateTime(2025, 8, 11), 25, 40)   // 40 laps
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
