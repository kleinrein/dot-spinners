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
            System.Console.WriteLine("Loading data ..");

            new DotSpinner(SpinnerTypes.Classic).Center().Start();

            for (int i = 0; i < 10; i++)
            {
                new DotSpinner().Time(2).Start();
                System.Console.Clear();
            }
        }
    }
}
    