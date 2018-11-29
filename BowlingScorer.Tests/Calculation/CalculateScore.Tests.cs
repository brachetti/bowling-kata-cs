using BowlingScorer.Calculation;
using Xunit;

namespace BowlingScorer.Tests.Calculation
{
	public class CalculateScore
	{
		[Fact]
		public void Should_recognize_new_game()
		{
			Frame[] emptyFrames = new Frame[0];
			byte result = Calculator.CalculateScore(emptyFrames);

			Assert.Equal<byte>(0, result);
		}
	}
}
