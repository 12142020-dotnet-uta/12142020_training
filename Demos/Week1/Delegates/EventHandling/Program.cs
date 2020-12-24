using System;

namespace EventHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            File file = new File() { Title = "File 1" };//create a sample file to pass around
            DownloadHelper downloadHelper = new DownloadHelper();//this is the publisher
            UnpackService unpackService = new UnpackService(); //this is the receiver
            NotificationService notificationService = new NotificationService();//this is another receiver

            //subscribe the method to the event to the event.
            downloadHelper.FileDownloaded += unpackService.OnFileDownloaded;
            downloadHelper.FileDownloaded += notificationService.OnFileDownloaded;

            downloadHelper.Download(file);// .Download downloads the file and calls the method that envookes the event.
        }
    }
}
