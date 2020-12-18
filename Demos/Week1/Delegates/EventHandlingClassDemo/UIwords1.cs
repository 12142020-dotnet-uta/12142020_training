using System;
using System.Threading;

namespace EventHandlingClassDemo
{
    public class UIwords1
    {
        public void OnUserInput(object source, SentenceEventArgs args)
        {
            System.Console.WriteLine(source.ToString());
            System.Console.WriteLine($"In UiWords1.OnUserInput - SentenceEventArgs UiSentence is {args.UiSentence}");
            System.Console.WriteLine("What do you want to say.");
            string ui = Console.ReadLine();
            args.UiSentence += ui;
            System.Console.WriteLine("Waiting 1 second");
            Thread.Sleep(1000);
        }
    }
}