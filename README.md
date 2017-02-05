# DotSpinners
Console spinners in .NET

<img src="https://github.com/kleinrein/dot-spinners/blob/master/DotSpinners.Resources/DotSpinners.gif" width="600">

## Install
```posh
Install-Package DotSpinners 
```
[Nuget](https://www.nuget.org/packages/DotSpinners/1.0.0)

## Usage
The public API is small and supports method chaining.
```c#
new DotSpinner(SpinnerTypes.Dots).Start();

// Or

var spinner = new DotSpinner(SpinnerTypes.Classic);
spinner.Time(5);
spinner.Interval(50);
spinner.Start();

Thread.Sleep(4000);

spinner.Stop();
```

## License
[The MIT License (MIT)](https://opensource.org/licenses/MIT)
