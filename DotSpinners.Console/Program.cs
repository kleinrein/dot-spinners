using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotSpinners.Console
{   
    class Program
    {
        static void Main(string[] args)
        {

            GifDemo();   
        }

        private static void DoWork()
        {
            Thread.Sleep(2000);
        }

        private static async Task DoIt()
        {
            await Task.Run(() => DoWork());
        }

        private static void GifDemo()
        {
            // Demos
            new DotSpinner(SpinnerTypes.Classic, DoIt()).Start();
            
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("DotsScrolling");
            new DotSpinner(SpinnerTypes.DotsScrolling).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Classic");
            new DotSpinner(SpinnerTypes.Classic).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Classic2");
            new DotSpinner(SpinnerTypes.Classic2).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Ping");
            new DotSpinner(SpinnerTypes.Ping).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Balloon");
            new DotSpinner(SpinnerTypes.Balloon).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Stars");
            new DotSpinner(SpinnerTypes.Stars).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Bar");
            new DotSpinner(SpinnerTypes.Bar).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Flag");
            new DotSpinner(SpinnerTypes.Flag).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("PercentageFive");
            new DotSpinner(SpinnerTypes.PercentageFive).Time(3).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("DotsSimple");
            new DotSpinner(SpinnerTypes.DotsSimple).Time(1).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("DotsRolling");
            new DotSpinner(SpinnerTypes.DotsRolling).Time(1).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("RugCrawler");
            new DotSpinner(SpinnerTypes.RugCrawler).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("RugCrawler");
            new DotSpinner(SpinnerTypes.RugCrawler).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("DotsShifter");
            new DotSpinner(SpinnerTypes.DotsShifter).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Eyes");
            new DotSpinner(SpinnerTypes.Eyes).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("HappyBeast");
            new DotSpinner(SpinnerTypes.HappyBeast).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Ooh");
            new DotSpinner(SpinnerTypes.Ooh).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Brackets");
            new DotSpinner(SpinnerTypes.Brackets).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("HashFest");
            new DotSpinner(SpinnerTypes.HashFest).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Pulse");
            new DotSpinner(SpinnerTypes.Pulse).Time(2).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Matrix");
            new DotSpinner(SpinnerTypes.Matrix).Time(3).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Flippie");
            new DotSpinner(SpinnerTypes.Flippie).Time(1).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Loading");
            new DotSpinner(SpinnerTypes.Loading).Time(3).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Unspecified");
            new DotSpinner(SpinnerTypes.Unspecified).Time(1).Start();
            System.Console.Clear();
            System.Console.WriteLine();

            System.Console.WriteLine("Money");
            new DotSpinner(SpinnerTypes.Money).Time(1).Start();
            System.Console.Clear();
            System.Console.WriteLine();
        }
        
    }
}
    