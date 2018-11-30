using System;
namespace BowlingScorer.Calculation
{
	public class Frame
	{
		public Frame(byte roll1) : this(roll1, null) { }

		public Frame(byte roll1, byte? roll2) : this(roll1, roll2, null) {}

		public Frame(byte roll1, byte? roll2, byte? roll3)
        {
            Roll1 = roll1;
            Roll2 = roll2;
            Roll3 = roll3;
			Rolls = new byte?[3] {
				roll1, roll2, roll3
			};
		}

        public byte Roll1 { get; }
        public byte? Roll2 { get; }

        public bool JustStrike => Roll1 == 10 && !Roll2.HasValue && !Roll3.HasValue;

        public byte? Roll3 { get; }

		public byte?[] Rolls { get; private set; }

		public byte? this[int index] => Rolls[index];
	}
}
