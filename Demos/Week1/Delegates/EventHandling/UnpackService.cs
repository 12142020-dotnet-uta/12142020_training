using System;

namespace EventHandling
{
    public class UnpackService
    {
        public void OnFileDownloaded(object source, FileEventArgs e)
        {
            System.Console.WriteLine($"UnpackService: Unpacking the file: It's name is {e.File.Title}");
        }
    }
}