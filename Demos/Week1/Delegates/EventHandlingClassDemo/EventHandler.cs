using System;
using System.Threading;

namespace EventHandlingClassDemo
{
    public class EventHandler
    {
        // // 1. create the delegate event handler in the class where the event will be emitted.
        // public delegate void UserInputEventHandler(object source, EventArgs args);

        // // 2. create an event based on the event handler.
        // public event UserInputEventHandler UIEvent;

        //this event is the newer syntax. It's a combination of the 2 steps above.
        public event EventHandler<SentenceEventArgs> UIEvent;

        private void OnUserInput(/*File file*/)
        {
            if (UIEvent != null)
            {
                UIEvent(this, new SentenceEventArgs() { UiSentence = "This" });
            }
        }

        public void UserInputReceived()
        {
            System.Console.WriteLine("The user input even has been fired");
            Thread.Sleep(1000);
            OnUserInput();
        }
    }
}