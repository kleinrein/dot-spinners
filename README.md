<img src="https://raw.githubusercontent.com/kleinrein/dot-spinners/master/DotSpinners.Resources/DotSpinners.png" width="150">

[![NuGet](https://img.shields.io/nuget/dt/DotSpinners.svg)]()
[![NuGet](https://img.shields.io/nuget/v/DotSpinners.svg)]()

# Dot Spinners
Console spinners for .NET — **71 spinners**, fluent API, async support, color and label support.

<img src="https://github.com/kleinrein/dot-spinners/blob/master/DotSpinners.Resources/DotSpinners.gif" width="600">

## Install
```posh
Install-Package DotSpinners
```
[NuGet](https://www.nuget.org/packages/DotSpinners/)

## Quick start

```csharp
// Spin while an async task runs (recommended)
await new DotSpinner(SpinnerTypes.Braille)
    .Label("Loading...")
    .Color(ConsoleColor.Cyan)
    .RunAsync(async () => await DoWorkAsync());

// Spin for a fixed time
new DotSpinner(SpinnerTypes.Dots).Time(3).Start();

// Spin until a Task completes (legacy / synchronous)
new DotSpinner(SpinnerTypes.Classic, DoWorkAsync()).Start();

// Random spinner, centered
new DotSpinner().Center().Time(2).Start();
```

## API

All methods return `DotSpinner` for chaining.

| Method | Description |
|---|---|
| `Time(int seconds)` | Run for N seconds (no-op when a task is provided) |
| `Interval(int ms)` | Override the spinner's default frame delay |
| `Label(string text)` | Show a text label to the right of the spinner |
| `Color(ConsoleColor)` | Set the spinner frame color |
| `LabelColor(ConsoleColor)` | Set the label text color |
| `Center()` | Center the spinner horizontally |
| `Random()` | Switch to a random spinner type |
| `Start()` | Start spinning (blocking) |
| `Stop()` | Stop the spinner |
| `StartAsync(CancellationToken)` | Start spinning asynchronously |
| `RunAsync(Func<Task>)` | Run work and spin until it completes, then stop |
| `RunAsync<T>(Func<Task<T>>)` | Same as above, returns the work result |

## Spinner types

71 spinners across these categories:

| Category | Examples |
|---|---|
| Classic | `Classic`, `Classic2`, `Spin` |
| Dots | `Dots`, `Dots2`, `Dots3`, `DotsScrolling`, `DotsRolling`, `DotsSimple`, `DotsShifter` |
| Braille | `Braille`, `BrailleDots` |
| Bouncing | `Bounce`, `BouncingBar`, `BouncingBall`, `Ball`, `Pong`, `Shark`, `Ping`, `Sand` |
| Geometric | `Arc`, `CircleHalves`, `Triangle`, `Box`, `GrowHorizontal`, `GrowVertical`, `LayerUp`, `Aesthetic`, `Noise` |
| Arrows | `Arrow`, `Arrow2`, `Arrow3` |
| Toggles | `Toggle`, `Toggle2`, `Toggle3`, `Toggle4`, `Toggle5` |
| Symbols | `Stars`, `Stars2`, `Star`, `Brackets`, `Pulse`, `HashFest`, `Matrix`, `Cross`, `Pipe`, `Hamburger` |
| Text | `Loading`, `Unspecified` |
| Progress | `Flag`, `Bar`, `PercentageFive`, `RugCrawler` |
| Clock & Emoji | `Clock`, `Moon`, `Earth`, `Weather`, `Christmas`, `Mindblown`, `Speaker`, `OrangePulse`, `BluePulse` |
| Fun | `HappyBeast`, `Eyes`, `Ooh`, `Balloon`, `Flippie` |
| Currency | `Money` |
| Misc | `Plus`, `Crocodiles` |

Full frame data: [`DotSpinners/spinners.txt`](DotSpinners/spinners.txt)

## License
[The MIT License (MIT)](https://opensource.org/licenses/MIT)
