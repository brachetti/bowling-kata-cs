using System;
namespace BowlingScorer.Calculation
{
    public class Frame
    {
		public Frame()
		{
		}

		public Frame(int roll1, int? roll2, int? roll3)
        {
            this.roll1 = roll1;
            this.roll2 = roll2;
            this.roll3 = roll3;
        }

        public int roll1 { get; }
        public int? roll2 { get; }
        public int? roll3 { get; }
    }
}
