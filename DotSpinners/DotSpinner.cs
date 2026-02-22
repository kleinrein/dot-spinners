using DotSpinners.Models;
using DotSpinners.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotSpinners
{
    /// <summary>
    /// DotSpinner — animated console spinner with fluent API.
    /// </summary>
    public class DotSpinner
    {
        // ── Public properties ────────────────────────────────────────────────
        public TextAlignment TextAlignment { get; set; }

        // ── Private state ────────────────────────────────────────────────────
        private readonly List<Spinner> _spinners = new List<Spinner>();
        private Spinner? _spinner;

        private int    _time;
        private int?   _interval;
        private string? _label;
        private ConsoleColor? _color;
        private ConsoleColor? _labelColor;

        private volatile bool _active;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly Random    _rand = new Random();
        private readonly Task? _task;
        private readonly object _lock;

        // ── Constructors ─────────────────────────────────────────────────────

        /// <summary>Picks a random spinner. Spins until <see cref="Stop"/> is called or <paramref name="task"/> completes.</summary>
        public DotSpinner(Task? task = null, object? lockObj = null)
        {
            _lock = lockObj ?? this;
            _task = task;
            LoadSpinners();
            Random();
        }

        /// <summary>Uses the specified <paramref name="spinnerType"/>.</summary>
        public DotSpinner(SpinnerTypes spinnerType, Task? task = null, object? lockObj = null)
            : this(task, lockObj)
        {
            var spinner = _spinners.FirstOrDefault(e => e.Name == spinnerType);
            if (spinner == null)
            {
                throw new ArgumentException($"Unknown spinner type: {spinnerType}", nameof(spinnerType));
            }

            _spinner = spinner;
        }

        // ── Fluent configuration ─────────────────────────────────────────────

        /// <summary>Run for a fixed number of seconds (ignored when a task is provided).</summary>
        public DotSpinner Time(int seconds)
        {
            _time = seconds;
            return this;
        }

        /// <summary>Override the spinner's default frame interval (milliseconds).</summary>
        public DotSpinner Interval(int ms)
        {
            _interval = ms;
            return this;
        }

        /// <summary>Center the spinner horizontally in the console window.</summary>
        public DotSpinner Center()
        {
            TextAlignment = TextAlignment.Center;
            return this;
        }

        /// <summary>Pick a random spinner type.</summary>
        public DotSpinner Random()
        {
            _spinner = _spinners[_rand.Next(_spinners.Count)];
            return this;
        }

        /// <summary>Display a label to the right of the spinner frame.</summary>
        public DotSpinner Label(string text)
        {
            _label = text;
            return this;
        }

        /// <summary>Set the color of the spinner frame characters.</summary>
        public DotSpinner Color(ConsoleColor color)
        {
            _color = color;
            return this;
        }

        /// <summary>Set the color of the label text.</summary>
        public DotSpinner LabelColor(ConsoleColor color)
        {
            _labelColor = color;
            return this;
        }

        // ── Start / Stop ─────────────────────────────────────────────────────

        /// <summary>
        /// Start the spinner synchronously (blocks until complete).
        /// </summary>
        public DotSpinner Start()
        {
            bool savedCursorVisible = false;
            try
            {
                try
                {
                    savedCursorVisible = Console.CursorVisible;
                    Console.CursorVisible = false;
                }
                catch (PlatformNotSupportedException)
                {
                    // Cursor visibility is not supported on this platform (e.g., Linux)
                }

                _active = true;
                int counter = -1;

                if (_time != 0) _stopwatch.Start();

                while (!_task?.IsCompleted ?? _active)
                {
                    PrintFrame(ref counter);

                    if (_task == null && _time != 0 && _stopwatch.Elapsed.TotalSeconds >= _time)
                        Stop();

                    Thread.Sleep(_interval ?? _spinner!.Interval);
                }

                return this;
            }
            finally
            {
                ClearLine();
                try
                {
                    Console.CursorVisible = savedCursorVisible;
                }
                catch (PlatformNotSupportedException)
                {
                    // Cursor visibility is not supported on this platform
                }
            }
        }

        /// <summary>
        /// Start the spinner asynchronously. Awaiting this method waits until the spinner stops.
        /// Pass a <see cref="CancellationToken"/> to cancel from outside.
        /// </summary>
        public async Task<DotSpinner> StartAsync(CancellationToken cancellationToken = default)
        {
            bool savedCursorVisible = false;
            try
            {
                try
                {
                    savedCursorVisible = Console.CursorVisible;
                    Console.CursorVisible = false;
                }
                catch (PlatformNotSupportedException)
                {
                    // Cursor visibility is not supported on this platform (e.g., Linux)
                }

                _active = true;
                int counter = -1;

                if (_time != 0) _stopwatch.Start();

                while (!cancellationToken.IsCancellationRequested &&
                       (!_task?.IsCompleted ?? _active))
                {
                    PrintFrame(ref counter);

                    if (_task == null && _time != 0 && _stopwatch.Elapsed.TotalSeconds >= _time)
                        Stop();

                    await Task.Delay(_interval ?? _spinner!.Interval, cancellationToken)
                              .ConfigureAwait(false);
                }

                return this;
            }
            catch (OperationCanceledException)
            {
                return this;
            }
            finally
            {
                ClearLine();
                try
                {
                    Console.CursorVisible = savedCursorVisible;
                }
                catch (PlatformNotSupportedException)
                {
                    // Cursor visibility is not supported on this platform
                }
            }
        }

        /// <summary>
        /// Run <paramref name="work"/> while displaying this spinner, then stop automatically.
        /// Returns the result of <paramref name="work"/>.
        /// </summary>
        public async Task<T> RunAsync<T>(Func<Task<T>> work)
        {
            using var cts = new CancellationTokenSource();
            var spinTask  = StartAsync(cts.Token);
            T result;
            try
            {
                result = await work().ConfigureAwait(false);
            }
            finally
            {
                cts.Cancel();
                await spinTask.ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        /// Run <paramref name="work"/> while displaying this spinner, then stop automatically.
        /// </summary>
        public async Task RunAsync(Func<Task> work)
        {
            using var cts = new CancellationTokenSource();
            var spinTask  = StartAsync(cts.Token);
            try
            {
                await work().ConfigureAwait(false);
            }
            finally
            {
                cts.Cancel();
                await spinTask.ConfigureAwait(false);
            }
        }

        /// <summary>Stop the spinner.</summary>
        public DotSpinner Stop()
        {
            _active = false;
            return this;
        }

        // ── Private helpers ──────────────────────────────────────────────────

        private void PrintFrame(ref int counter)
        {
            counter++;
            if (counter >= _spinner!.Sequence.Length)
                counter = 0;

            lock (_lock)
            {
                SetAlignment(counter);

                string frame = _spinner!.Sequence[counter];
                string output = string.IsNullOrEmpty(_label)
                    ? frame
                    : frame + " " + _label;

                // Write spinner frame (optionally coloured)
                if (_color.HasValue)
                {
                    ConsoleColor prev = Console.ForegroundColor;
                    Console.ForegroundColor = _color.Value;
                    Console.Write(frame);
                    Console.ForegroundColor = prev;
                }
                else
                {
                    Console.Write(frame);
                }

                // Write label (optionally coloured)
                if (!string.IsNullOrEmpty(_label))
                {
                    Console.Write(" ");
                    if (_labelColor.HasValue)
                    {
                        ConsoleColor prev = Console.ForegroundColor;
                        Console.ForegroundColor = _labelColor.Value;
                        Console.Write(_label);
                        Console.ForegroundColor = prev;
                    }
                    else
                    {
                        Console.Write(_label);
                    }
                }

                // Move cursor back to start of output
                int written = output.Length;
                Console.SetCursorPosition(
                    Math.Max(0, Console.CursorLeft - written),
                    Console.CursorTop);
            }
        }

        private void ClearLine()
        {
            try
            {
                lock (_lock)
                {
                    int width = _spinner?.Sequence?.Length > 0
                        ? (_spinner!.Sequence[0].Length + (_label?.Length ?? 0) + (_label != null ? 1 : 0))
                        : 0;
                    if (width > 0)
                    {
                        Console.Write(new string(' ', width));
                        Console.SetCursorPosition(Math.Max(0, Console.CursorLeft - width), Console.CursorTop);
                    }
                }
            }
            catch { /* best-effort */ }
        }

        private void LoadSpinners()
        {
            var text  = Resources.spinners;
            var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var parts = line.Split(',');
                if (parts.Length < 3) continue;
                _spinners.Add(new Spinner(
                    parts[0],
                    int.Parse(parts[1]),
                    parts.Skip(2).ToArray()));
            }
        }

        private void SetAlignment(int counter)
        {
            if (TextAlignment == TextAlignment.Center)
            {
                int frameLen = _spinner!.Sequence[counter].Length
                    + (_label != null ? 1 + _label.Length : 0);
                Console.SetCursorPosition(
                    Math.Max(0, (Console.WindowWidth - frameLen) / 2),
                    Console.CursorTop);
            }
        }
    }
}
