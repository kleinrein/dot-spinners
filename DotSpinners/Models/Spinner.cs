using System;

namespace DotSpinners
{
    public class Spinner
    {
        public SpinnerTypes Name { get; set; }
        public int Interval { get; set; }
        public string[] Sequence { get; set; }

        public Spinner(string name, int interval, string[] sequence)
        {
            Name = (SpinnerTypes)Enum.Parse(typeof(SpinnerTypes), name, true);
            Interval = interval;
            Sequence = sequence;
        }
    }
}