using System;

namespace EventHandlingClassDemo
{
    public class SentenceEventArgs : EventArgs
    {
        public string UiSentence { get; set; }
    }
}