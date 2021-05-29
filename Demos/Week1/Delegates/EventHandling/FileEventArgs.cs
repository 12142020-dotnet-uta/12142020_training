using System;

namespace EventHandling
{
    //this class inherits EventArgs and contains the arguments to be sent with the event.
    public class FileEventArgs : EventArgs
    {
        public File File { get; set; }
    }
}