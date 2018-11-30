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

			for (int i = 0, framesLength = frames.Length; i < framesLength; i++)
			{
				Frame frame = frames[i];
				state = IncreaseScore(frame.Roll1, state);
				state = RealizeStrike(frame, state);

				if (!frame.JustStrike)
				{
					state = IncreaseScore(frame.Roll2, state);
					state = RealizeSpareSituation(frame, state);
					state = IncreaseScoreIfLastFrame(frame.Roll3, state, i == 9);
				}
            }

            return state.Score;
        }

		private static State IncreaseScoreIfLastFrame(byte? roll3, State state, bool last_frame)
		{
			if (!last_frame)
			{
				return state;
			}
			return IncreaseScore(roll3, state);
		}

		private static State RealizeStrike(Frame frame, State state)
        {
            if (frame.Roll1 == 10)
            {
                return new State
                {
                    Score = state.Score,
                    DoubleUp = (byte)(state.DoubleUp + 2)
                };
            }
            return state;
        }

        private static State RealizeSpareSituation(Frame frame, State state)
        {
            if (
                frame.Roll1 < 10 &&
                frame.Roll1 + frame.Roll2.GetValueOrDefault(0) == 10
            )
            {
                return new State
                {
                    Score = state.Score,
                    DoubleUp = (byte)(state.DoubleUp + 1)
                };
            }

            return state;
        }

        private static State IncreaseScore(byte? roll, State state)
        {
            return new State
            {
                Score = (byte)(state.Score + roll.GetValueOrDefault(0) + (state.DoubleUp > 0 ? roll.GetValueOrDefault(0) : 0)),
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
