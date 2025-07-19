// using System;

// class Program
// {
//     static void Main(string[] args)
//     {
//         // variable
//         int num = 10;
//         Console.WriteLine(num);
//         // input
//         Console.WriteLine("whats ur name");
//         // OUTPUT
//         String name = Console.ReadLine();
//         Console.WriteLine("hello" +" "+ name);
//     }
// }


using System;

class Program
{
    static void Main(string[] args)
    {
        //input

        Console.WriteLine("Whats your first name?");
        String firstName = Console.ReadLine();
        Console.WriteLine("Whats your last name?");
        String lastName = Console.ReadLine();
        //output
        Console.WriteLine("your name is "+ lastName + ", "+ firstName+ " "+ lastName);
        
    }
}