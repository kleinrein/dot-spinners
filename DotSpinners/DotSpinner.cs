using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;
using DotSpinners.Models;
using DotSpinners.Properties;

namespace DotSpinners
{
    /// <summary>
    /// DotSpinner
    /// </summary>
    public class DotSpinner
    {
        // Properties
        public TextAlignment TextAlignment { get; set; }

        private List<Spinner> Spinners { get; } = new List<Spinner>();
        private Spinner Spinner { get; set; }

        // Fields
        private int _time = 0;
        private int? _interval;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private bool _active = false;
        private Random _rand = new Random();

        public DotSpinner()
        {
            LoadSpinners();
            Random();
        }

        public DotSpinner(SpinnerTypes spinnerType) : this()
        {
            this.Spinner = Spinners.FirstOrDefault(e => e.Name == spinnerType);
        }

        /// <summary>
        /// Set looping time for spin
        /// If time is not set, it will go on until Stop() is called
        /// </summary>
        /// <param name="time">Time in seconds</param>
        /// <returns></returns>
        public DotSpinner Time(int time)
        {
            this._time = time;
            return this;
        }

        /// <summary>
        /// Override default spinner interval
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public DotSpinner Interval(int interval)
        {
            this._interval = interval;
            return this;
        }

        /// <summary>
        /// Center spinner in console
        /// Defaults to false
        /// </summary>
        /// <returns></returns>
        public DotSpinner Center()
        {
            this.TextAlignment = TextAlignment.Center;
            return this;
        }

        /// <summary>
        /// Random spinner
        /// If no spinnertype is passed in constructor, random will be called
        /// </summary>
        /// <returns></returns>
        public DotSpinner Random()
        {
            this.Spinner = Spinners[_rand.Next(Spinners.Count)];
            return this;
        }

        /// <summary>
        /// Start spinner
        /// </summary>
        /// <returns></returns>
        public DotSpinner Start()
        {
            _active = true;
            int counter = -1;

            if (_time != 0) _stopwatch.Start();
            while (_active)
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

                // Stop if time has passed the time user set to wait
                if (_time != 0 && _stopwatch.Elapsed.Seconds >= _time) Stop();

                Thread.Sleep(_interval ?? Spinner.Interval);
            }
            
            return this;
        }

        /// <summary>
        /// Stop spinner
        /// </summary>
        /// <returns></returns>
        public DotSpinner Stop()
        {
            _active = false;
            return this;
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

        private void SetAlignment(int counter)
        {
            if (TextAlignment == TextAlignment.Center)
                Console.SetCursorPosition((Console.WindowWidth - Spinner.Sequence[counter].Length) / 2, Console.CursorTop);
        }
    }
}
