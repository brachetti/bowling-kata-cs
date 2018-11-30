using System;
using System.Linq;

namespace BowlingScorer.Calculation
{
	public class Calculator
	{
		public Calculator()
		{
		}

		public static byte CalculateScore(Frame[] frames)
		{
			State state = new State { Score = 0 };
			Console.WriteLine(state);

			foreach (Frame frame in frames)
			{
				state = increaseScore(frame.Roll1, state);
				state = increaseScore(frame.Roll2, state);
			}

			return state.Score;
		}

		private static State increaseScore(byte? roll, State state)
		{
			return new State { 
				Score = (byte)(state.Score + roll),
				DoubleUp = state.DoubleUp
			};
		}

		private class State
		{
			public byte Score { get; set; }
			public byte DoubleUp { get; set; }
		}
	}
}
