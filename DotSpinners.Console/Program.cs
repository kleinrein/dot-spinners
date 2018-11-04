using System.Threading;
using System.Threading.Tasks;

namespace DotSpinners.Console
{
    static class Program
    {
        private static void Main(string[] args)
        {

            GifDemo();
        }

        private static void DoWork()
        {
            for (int i = 0; i < 20; i++)
            {
                //System.Console.WriteLine("Test " + i + " " + System.Console.CursorTop);
                //System.Console.WriteLine("Test2222222222222222222 " + i + " " + System.Console.CursorTop);
                Thread.Sleep(100);
            }
        }

        private static async Task DoIt()
        {
            await Task.Run(() => DoWork()).ConfigureAwait(false);
        }

        private static void Spin(SpinnerTypes type, int time)
        {
            System.Console.WriteLine(type.ToString());
            new DotSpinner(type).Time(time).Start();
            System.Console.Clear();
            System.Console.WriteLine();
        }

        private static void GifDemo()
        {
            // Demos
            new DotSpinner(SpinnerTypes.Classic, DoIt()).Start();

            System.Console.Clear();
            System.Console.WriteLine();

            Spin(SpinnerTypes.Dots2, 4);
            Spin(SpinnerTypes.Plus, 4);
            Spin(SpinnerTypes.Crocodiles, 4);
        }

    }
}
