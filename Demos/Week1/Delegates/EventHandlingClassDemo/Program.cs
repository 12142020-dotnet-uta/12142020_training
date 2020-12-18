using System;

namespace EventHandlingClassDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EventHandler UiEvent = new EventHandler();//create the event class instance
            UIwords1 uiw1 = new UIwords1();//class instance
            UIwords2 uiw2 = new UIwords2();//class instance

            UiEvent.UIEvent += uiw1.OnUserInput;//subscribe the method to the event
            UiEvent.UIEvent += uiw2.OnUserInput;
            UiEvent.UIEvent += uiw2.OnUserInput2;

            // UiEvent.UIEvent -= uiw2.OnUserInput;

            //do things....

            UiEvent.UserInputReceived();//fire the event


        }
    }
}
