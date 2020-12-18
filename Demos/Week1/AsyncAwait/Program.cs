using System;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace AsyncAwait
{
    class Program
    {
        private static System.Timers.Timer aTimer;

        static async Task Main(string[] args)
        {
            AsyncMethods am = new AsyncMethods();

            // var time1 = DateTime.Now;
            // System.Console.WriteLine($"The first time is {time1}");

            // Task m1 = am.Method1Async();
            // Task m2 = am.Method2Async();
            // Task m3 = am.Method3Async();
            // Task m4 = am.Method4Async();
            // Task m5 = am.Method5Async();


            // time1 = DateTime.Now;
            // System.Console.WriteLine($"The second time is {time1}");

            // await m1;
            // System.Console.WriteLine("M1 returned");
            // await m2;
            // System.Console.WriteLine("M2 returned");
            // await m3;
            // System.Console.WriteLine("M3 returned");
            // await m4;
            // System.Console.WriteLine("M4 returned");
            // await m5;
            // System.Console.WriteLine("M5 returned");

            // time1 = DateTime.Now;
            // System.Console.WriteLine($"The third time is {time1}");

            //Thread.Sleep(15000);


            //create a timer delegate to be raised every second.
            // https://docs.microsoft.com/en-us/dotnet/api/system.timers.timer.elapsed?view=netcore-3.1
            aTimer = new System.Timers.Timer(); // create a timer
            aTimer.Interval = 500;              // set the timer interval to half a second
            aTimer.Elapsed += OnTimedEvent;     // subscribe the event to the timer
            aTimer.AutoReset = true;            // have the timer fire repeatedly
            aTimer.Enabled = true;              // start the timer



            // Neither the class nor the methods are static so you can 
            // only access them through an instance of the class.
            //AsyncMethods am = new AsyncMethods();

            var time2 = DateTime.Now;
            System.Console.WriteLine($"The first time is {time2}");

            //call the methods the save the returned task into a Task.
            Task m1Task = am.Method1Async();
            var m2Task = am.Method2Async();
            var m3Task = am.Method3Async();
            var m4Task = am.Method4Async();
            var m5Task = am.Method5Async();

            // await am.Method5Async();
            // System.Console.WriteLine("M5 returned");
            // await am.Method4Async();
            // System.Console.WriteLine("M4 returned");
            // await am.Method3Async();
            // System.Console.WriteLine("M3 returned");
            // await am.Method2Async();
            // System.Console.WriteLine("M2 returned");
            // await am.Method1Async();
            // System.Console.WriteLine("M1 returned");
            //print the current time

            time2 = DateTime.Now;
            System.Console.WriteLine($"The second time is {time2}");

            // now await the tasks.
            await m5Task;
            System.Console.WriteLine("M5 returned");
            await m4Task;
            System.Console.WriteLine("M4 returned");
            await m3Task;
            System.Console.WriteLine("M3 returned");
            await m2Task;
            System.Console.WriteLine("M2 returned");
            await m1Task;
            System.Console.WriteLine("M1 returned");

            //wait 4 seconds to allow enough time for the methods to return
            Task.Delay(4000).Wait();

            // print the current time
            var time3 = DateTime.Now;
            System.Console.WriteLine($"The third time is {time3}");
        }//end of Main()

        // This event will fire based ont he trigger interal set above.
        public static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            System.Console.WriteLine($"The time now is {e.SignalTime}");
        }
    }//end of Program
}//End of NameSpace