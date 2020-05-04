using System;
using Microsoft.SPOT;

namespace MFConsoleApplication2
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print(
                Resources.GetString(Resources.StringResources.String1));
            System.Console.WriteLine("enter some text and hit ENTER.");
            System.Console.WriteLine("enter 'x' and hit ENTER to exit.");
            System.Console.WriteLine();
        }

    }
}
