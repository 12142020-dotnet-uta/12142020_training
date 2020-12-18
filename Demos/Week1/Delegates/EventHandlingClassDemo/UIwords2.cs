using System;
using System.Threading;

namespace EventHandlingClassDemo
{
    public class UIwords2
    {
        public void OnUserInput(object source, SentenceEventArgs args)
        {
            System.Console.WriteLine($"In UiWords2.OnUserInput - SentenceEventArgs UiSentence is {args.UiSentence}");
            System.Console.WriteLine("What do you want to say.");
            string ui = Console.ReadLine();
            args.UiSentence += ui;
            System.Console.WriteLine("Waiting 1 second");
            Thread.Sleep(1000);
        }

        public void OnUserInput2(object source, SentenceEventArgs args)
        {
            System.Console.WriteLine($"In UiWords2.OnUserInput2 - SentenceEventArgs UiSentence is {args.UiSentence}");
            System.Console.WriteLine("What do you want to say.");
            string ui = Console.ReadLine();
            args.UiSentence += ui;
            System.Console.WriteLine("Waiting 1 second");
            Thread.Sleep(1000);
        }
    }
}