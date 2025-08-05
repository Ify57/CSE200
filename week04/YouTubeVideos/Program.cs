using System;
using System.Collections.Generic;


// Represents a single comment on a video
public class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }

    public void Display()
    {
        Console.WriteLine($"    {CommenterName}: {Text}");
    }
}

// Represents a YouTube video and the comments on it
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    // Constructor that sets basic info and initializes comment list
    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    // Add a comment to this video
    public void AddComment(Comment comment)
    {
        if (comment != null)
            Comments.Add(comment);
    }

    // Requirement: method that returns number of comments
    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    // Display video info and all comments
    public void DisplayInfo()
    {
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Title : {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Comments ({GetNumberOfComments()}):");
        foreach (var comment in Comments)
        {
            comment.Display();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create videos (3-4 videos as required)
        var video1 = new Video("How Widgets Work", "AcmeChannel", 420);
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Helped me a lot, thanks."));
        video1.AddComment(new Comment("Cara", "Can you make a deep-dive?"));

        var video2 = new Video("Top 10 Gadget Reviews", "TechToday", 860);
        video2.AddComment(new Comment("Derek", "I disagree with #3."));
        video2.AddComment(new Comment("Ellie", "Nice roundup."));
        video2.AddComment(new Comment("Femi", "Where did you buy #7?"));

        var video3 = new Video("Unboxing the Future", "GizmoGuru", 305);
        video3.AddComment(new Comment("Grace", "That packaging is amazing."));
        video3.AddComment(new Comment("Hassan", "Looks expensive."));
        video3.AddComment(new Comment("Ifeanyi", "Love the camera test."));

        // Put videos in a list
        var videos = new List<Video> { video1, video2, video3 };

        // Iterate and display each video's details and its comments
        foreach (var v in videos)
        {
            v.DisplayInfo();
        }

        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("End of video list. Press any key to exit.");
        Console.ReadKey();
    }
}
