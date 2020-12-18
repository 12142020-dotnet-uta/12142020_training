using System;
using System.Threading.Tasks;
using System.Threading;

namespace AsyncAwait
{
    public class AsyncMethods
    {
        public async Task Method1Async()//convention says to add Async in the name of the method
        {
            Task task = Task.Delay(5000);
            // await Task.Delay(5000);
            System.Console.WriteLine("Method1Async - Just after await 5 secs");
            await task;
        }

        public async Task Method2Async()//convention says to add Async in the name of the method
        {
            Task task = Task.Delay(3000);
            // await Task.Delay(5000);
            System.Console.WriteLine("Method2Async - Just after await 3 secs");
            await task;
        }

        public async Task Method3Async()//convention says to add Async in the name of the method
        {
            Task task = Task.Delay(3000);
            // await Task.Delay(5000);
            System.Console.WriteLine("Method3Async - Just after await 3 secs");
            await task;
        }

        public async Task Method4Async()//convention says to add Async in the name of the method
        {
            Task task = Task.Delay(3000);
            // await Task.Delay(5000);
            System.Console.WriteLine("Method4Async - Just after await 3 secs");
            await task;
        }

        public async Task Method5Async()//convention says to add Async in the name of the method
        {
            Task task = Task.Delay(3000);
            // await Task.Delay(5000);
            System.Console.WriteLine("Method5Async - Just after await 3 secs");
            await task;
        }


        //all asynchronous methods must return Task or Task<DataTypeHere>
        // public async Task<int> Method1Async()//convention says to add Async in the name of the method
        // {
        //     await Task.Delay(3000);
        //     System.Console.WriteLine("In method 1 now waiting 3 seconds");
        //     return 1;
        // }

        // public async Task<int> Method2Async()
        // {
        //     await Task.Delay(3000);
        //     System.Console.WriteLine("In method 2 now waiting 3 seconds");
        //     return 1;
        // }

        // public async Task<int> Method3Async()
        // {
        //     await Task.Delay(3000);
        //     System.Console.WriteLine("In method 3 now waiting 3 seconds");
        //     return 1;
        // }

        // public async Task<int> Method4Async()
        // {
        //     await Task.Delay(3000);
        //     System.Console.WriteLine("In method 4 now waiting 3 seconds");
        //     return 1;
        // }

        // public async Task<int> Method5Async()
        // {
        //     System.Console.WriteLine("In method 5 now waiting 3 seconds");
        //     await Task.Delay(3000);
        //     return 1;
        // }
    }
}