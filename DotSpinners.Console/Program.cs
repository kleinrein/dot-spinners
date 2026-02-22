using System;
using System.Threading;
using System.Threading.Tasks;
using DotSpinners;

// ── Helpers ──────────────────────────────────────────────────────────────────

static void Section(string title)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"  ── {title} ──");
    Console.ResetColor();
    Console.WriteLine();
}

static void Spin(SpinnerTypes type, int seconds, string? label = null, ConsoleColor? color = null)
{
    Console.Write($"  {type,-20} ");
    var spinner = new DotSpinner(type).Time(seconds);
    if (label   != null) spinner.Label(label);
    if (color   != null) spinner.Color(color.Value);
    spinner.Start();
    Console.WriteLine();
}

static async Task SimulateWork(int ms = 1800) =>
    await Task.Delay(ms).ConfigureAwait(false);

// ── Demo ──────────────────────────────────────────────────────────────────────

Console.Clear();
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("  ╔══════════════════════════════════╗");
Console.WriteLine("  ║         DotSpinners v2.0         ║");
Console.WriteLine("  ║    70+ spinners · fluent API     ║");
Console.WriteLine("  ╚══════════════════════════════════╝");
Console.ResetColor();

// ── Classic ───────────────────────────────────────────────────────────────────
Section("Classic");
Spin(SpinnerTypes.Classic,        2, "spinning...", ConsoleColor.Green);
Spin(SpinnerTypes.Classic2,       2, "working...",  ConsoleColor.Yellow);
Spin(SpinnerTypes.Spin,           2);

// ── Dots ──────────────────────────────────────────────────────────────────────
Section("Dots");
Spin(SpinnerTypes.Dots,           2, null, ConsoleColor.Cyan);
Spin(SpinnerTypes.Dots2,          2, null, ConsoleColor.Cyan);
Spin(SpinnerTypes.Dots3,          2, null, ConsoleColor.Cyan);
Spin(SpinnerTypes.DotsScrolling,  2);
Spin(SpinnerTypes.DotsRolling,    2);

// ── Braille ───────────────────────────────────────────────────────────────────
Section("Braille");
Spin(SpinnerTypes.Braille,        2, "loading...", ConsoleColor.Magenta);
Spin(SpinnerTypes.BrailleDots,    2, null,         ConsoleColor.Magenta);

// ── Bouncing ──────────────────────────────────────────────────────────────────
Section("Bouncing");
Spin(SpinnerTypes.Bounce,         2);
Spin(SpinnerTypes.BouncingBar,    2, null, ConsoleColor.Blue);
Spin(SpinnerTypes.BouncingBall,   2);
Spin(SpinnerTypes.Pong,           3);
Spin(SpinnerTypes.Shark,          3);

// ── Geometric ─────────────────────────────────────────────────────────────────
Section("Geometric");
Spin(SpinnerTypes.Arc,            2, null, ConsoleColor.Yellow);
Spin(SpinnerTypes.CircleHalves,   2);
Spin(SpinnerTypes.Triangle,       2);
Spin(SpinnerTypes.Box,            2);
Spin(SpinnerTypes.GrowHorizontal, 2, "growing...");
Spin(SpinnerTypes.GrowVertical,   2);
Spin(SpinnerTypes.LayerUp,        2);
Spin(SpinnerTypes.Aesthetic,      2, null, ConsoleColor.DarkCyan);

// ── Arrows ────────────────────────────────────────────────────────────────────
Section("Arrows");
Spin(SpinnerTypes.Arrow,          2, null, ConsoleColor.Green);
Spin(SpinnerTypes.Arrow2,         2);
Spin(SpinnerTypes.Arrow3,         2);

// ── Emoji / Nature ────────────────────────────────────────────────────────────
Section("Emoji & Nature");
Spin(SpinnerTypes.Clock,          3, "waiting...");
Spin(SpinnerTypes.Moon,           2);
Spin(SpinnerTypes.Earth,          3);
Spin(SpinnerTypes.Weather,        4);
Spin(SpinnerTypes.Mindblown,      3, "processing...");
Spin(SpinnerTypes.Speaker,        2);
Spin(SpinnerTypes.OrangePulse,    2);
Spin(SpinnerTypes.BluePulse,      2);

// ── Fun ───────────────────────────────────────────────────────────────────────
Section("Fun");
Spin(SpinnerTypes.HappyBeast,     2);
Spin(SpinnerTypes.Eyes,           2);
Spin(SpinnerTypes.Sand,           2);
Spin(SpinnerTypes.Balloon,        2, "inflating...");
Spin(SpinnerTypes.Loading,        3, null, ConsoleColor.White);
Spin(SpinnerTypes.Money,          2, "billing...", ConsoleColor.Green);

// ── Async API demo ────────────────────────────────────────────────────────────
Section("Async API — RunAsync");
Console.Write("  Fetching data          ");
await new DotSpinner(SpinnerTypes.Braille)
    .Label("fetching data...")
    .Color(ConsoleColor.Cyan)
    .RunAsync(async () => await SimulateWork(2000));
Console.WriteLine("  done.");

Console.Write("  Compiling              ");
await new DotSpinner(SpinnerTypes.GrowHorizontal)
    .Label("compiling...")
    .Color(ConsoleColor.Yellow)
    .RunAsync(async () => await SimulateWork(2000));
Console.WriteLine("  done.");

// ── Task-driven (legacy API) ───────────────────────────────────────────────────
Section("Task-driven (legacy)");
Console.Write("  Task-driven spinner    ");
new DotSpinner(SpinnerTypes.Classic, SimulateWork(2000)).Start();
Console.WriteLine("  done.");

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("  All demos complete.");
Console.ResetColor();
Console.WriteLine();
