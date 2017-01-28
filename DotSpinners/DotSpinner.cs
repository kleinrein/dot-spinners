using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DotSpinners.Models;
using DotSpinners.Properties;

namespace DotSpinners
{
    public class DotSpinner
    {
        // Spinners
        public List<Spinner> Spinners { get; set; } = new List<Spinner>();
        public Spinner Spinner { get; set; }

        // Appearance properties
        public TextAlignment TextAlignment { get; set; }

        public DotSpinner(SpinnerTypes spinnerType)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            // Load spinners from txt file
            LoadSpinners();
            

            Spinner = Spinners.FirstOrDefault(e => e.Name == spinnerType);
        }

        private void LoadSpinners()
        {

            var spinnersText = Resources.spinners;
            var lines = spinnersText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var splitByComma = line.Split(',');
                Spinners.Add(new Spinner(splitByComma[0], int.Parse(splitByComma[1]),
                    splitByComma.ToList().Skip(2).ToArray()));
            }
        }

        public DotSpinner StartSpinning()
        {
            var counter = -1;
            while (true)
            {
                counter++;

                // Loop spinner when it has reach it's last frame
                if (counter >= Spinner.Sequence.Length)
                    counter = 0;

                // Align text if center is on
                SetAlignment(counter);

                // Write and set cursor position
                Console.Write(Spinner.Sequence[counter]);
                Console.SetCursorPosition(Console.CursorLeft - Spinner.Sequence[counter].Length, Console.CursorTop);
                Thread.Sleep(Spinner.Interval);
            }
        }

        private void SetAlignment(int counter)
        {
            if (TextAlignment == TextAlignment.Center)
                Console.SetCursorPosition((Console.WindowWidth - Spinner.Sequence[counter].Length) / 2, Console.CursorTop);
        }
    }
}
