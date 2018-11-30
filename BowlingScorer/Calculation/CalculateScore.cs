using System;

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
				state = IncreaseScore(frame.Roll1, state);
				state = IncreaseScore(frame.Roll2, state);
				state = RealizeSpareSituation(frame, state);
			}

			return state.Score;
		}

        private static State RealizeSpareSituation(Frame frame, State state)
        {
            if (frame.Roll1 + frame.Roll2 == 10) {
				return new State {
					Score = state.Score,
					DoubleUp = (byte)(state.DoubleUp + 1)
				};
			}

			return state;
        }

        private static State IncreaseScore(byte? roll, State state)
		{
			return new State {
				Score = (byte)(state.Score + roll + (state.DoubleUp > 0 ? roll : 0)),
				DoubleUp = (byte)Math.Max(0, state.DoubleUp - 1)
			};
		}

		private class State
		{
			public byte Score { get; set; }
			public byte DoubleUp { get; set; }
		}
	}
}
