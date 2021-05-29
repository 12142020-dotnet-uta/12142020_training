using System;

namespace Delegates
{
    class Program
    {
        //a delegate has a particular param list and return type.
        public delegate void PerformCalculation(SampleObject x);

        static void Main(string[] args)
        {
            PerformCalculation pc = Multiply;//[0] of invocationList
            pc = pc + Subtract;             // [1] of invocationList
            pc += Divide;                   // [2] of invocationList

            //pc -= Subtract;//take one method out of the list.
            pc -= Multiply;

            NewMethod nm = new NewMethod();
            pc += nm.NewClassMethod;

            SampleObject sampleObject = new SampleObject();// the object that will be sent in to the delegate
            sampleObject.X = 10.0;
            sampleObject.Y = 100.0;
            sampleObject.Total = 0;

            pc(sampleObject); // fire off the delegate

            System.Console.WriteLine($"The 'Total' Property in the SampleObject is {sampleObject.Total}.");//gives the result of Divide() only

            // System.Console.WriteLine("\nStarting ForEach loop.\n");
            // //double result1 = 0;
            // // the foreach loop below show how you can access the invocation list that delegate has.
            // foreach (Delegate item in pc.GetInvocationList())
            // {
            //     if (sampleObject.Total == 0)
            //     {
            //         item.DynamicInvoke(sampleObject);
            //         //System.Console.WriteLine(result1);
            //     }
            //     else
            //     {
            //         item.DynamicInvoke(sampleObject);
            //         // System.Console.WriteLine(result1);
            //     }
            // }

            System.Console.WriteLine($"\tsample.Total is {sampleObject.Total}.");
        }

        // method 1
        public static void Multiply(SampleObject x)
        {
            x.Total = x.X * x.Y;
            Console.WriteLine($"In the Multiply Method");
        }

        //method 2
        public static void Subtract(SampleObject x)
        {
            x.Total = x.X - x.Y;
            Console.WriteLine($"In the Subtract Method");
        }

        //method 3
        public static void Divide(SampleObject x)
        {
            x.Total = x.X / x.Y;
            Console.WriteLine($"In the Divide Method");
        }
    }

    //now create a separate class with a method to add to the delegate.
    public class NewMethod
    {
        public void NewClassMethod(SampleObject x)
        {
            //x.Total = Math.Pow(x.X, x.Y);
            Console.WriteLine($"In the NewClassMethod Method");
        }
    }
}
