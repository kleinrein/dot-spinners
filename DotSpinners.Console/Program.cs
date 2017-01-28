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
            System.Console.Title = "Loading..";

            for (int i = 37; i <= 32767; i += 200)
            {
                System.Console.Beep(i, 100);
            }

            System.Console.WriteLine("Loading data ..");
            
            var spinner = new DotSpinner(SpinnerTypes.PercentageFive);
            spinner.StartSpinning();

        }
    }
}
