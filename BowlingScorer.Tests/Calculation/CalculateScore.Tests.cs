using BowlingScorer.Calculation;
using Xunit;

namespace BowlingScorer.Tests.Calculation
{
	public class EasySteps
	{
		[Fact]
		public void Should_recognize_new_game()
		{
			Frame[] emptyFrames = new Frame[0];
			byte result = Calculator.CalculateScore(emptyFrames);

			Assert.Equal<byte>(0, result);
		}

		[Fact]
		public void Should_calculate_game_without_a_score()
		{
			Frame[] frames = {
				new Frame(0, 0),
				new Frame(0, 0),
				new Frame(0, 0)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal<byte>(0, result);
		}

		[Fact]
		public void Should_calculate_bad_games1()
		{
			Frame[] frames =
			{
				new Frame(1, 0),
				new Frame(0, 1),
				new Frame(1, 0)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(3, result);
		}
	}
}
