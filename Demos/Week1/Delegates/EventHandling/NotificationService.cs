using System;

namespace EventHandling
{
    public class NotificationService
    {
        public void OnFileDownloaded(object source, FileEventArgs e)
        {
            System.Console.WriteLine($"NotificationService: Notifying you that the file, {e.File.Title}, was downloaded.");
        }
    }
}