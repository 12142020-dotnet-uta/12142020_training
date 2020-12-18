using System;
using System.Threading;

namespace EventHandling
{
    public class DownloadHelper
    {
        // //1. create the delegate event handler in the class where the event will be emitted.
        // public delegate void FileDownloadEventHandler(object source, FileEventArgs args);

        // //2. create an event based on the event handler.
        // // methods subscribe to this event, making it not null.
        // public event FileDownloadEventHandler FileDownloaded;

        public event EventHandler<FileEventArgs> FileDownloaded;

        //3.b. (3.a is below when this method is called) raise the event with a method 
        protected virtual void OnFileDownloaded(File file1)
        {
            //if the event has subscribers
            if (FileDownloaded != null)
            {
                // invoke the event handler delegate with 'this' class object 
                // (DownloadHelper), and a list of arguments (here it is empty)
                FileDownloaded(this, new FileEventArgs() { File = file1 });
            }
        }

        public void Download(File file)
        {
            System.Console.WriteLine("Downloading File...");
            Thread.Sleep(4000);

            // call the method that emits the event.
            OnFileDownloaded(file);
        }
    }
}