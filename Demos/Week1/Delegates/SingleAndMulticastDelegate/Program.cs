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

            pc -= Subtract;//take one method out of the list.

            NewMethod nm = new NewMethod();
            pc += nm.NewClassMethod;
            SampleObject sampleO = new SampleObject();
            sampleO.X = 10.0;
            sampleO.Y = 100.0;
            sampleO.Total = 0;

            pc(sampleO);

            System.Console.WriteLine(sampleO.Total);//gives the result of Divide() only

            System.Console.WriteLine("\nStarting ForEach loop.\n");
            //double result1 = 0;
            foreach (Delegate item in pc.GetInvocationList())
            {
                if (sampleO.Total == 0)
                {
                    item.DynamicInvoke(sampleO);
                    //System.Console.WriteLine(result1);
                }
                else
                {
                    item.DynamicInvoke(sampleO);
                    // System.Console.WriteLine(result1);
                }
            }

            System.Console.WriteLine($"\tsample.Total is {sampleO.Total}.");
        }

        // method 1
        public static void Multiply(SampleObject x)
        {
            x.Total = x.X * x.Y;
        }

        //method 2
        public static void Subtract(SampleObject x)
        {
            x.Total = x.X - x.Y;
        }

        //method 3
        public static void Divide(SampleObject x)
        {
            x.Total = x.X / x.Y;
        }
    }

    //now create a separate class with a method to add to the delegate.
    public class NewMethod
    {
        public void NewClassMethod(SampleObject x)
        {
            x.Total = Math.Pow(x.X, x.Y);
        }
    }
}
