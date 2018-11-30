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

		[Fact]
		public void Should_calculate_bad_games2()
		{
			Frame[] frames =
			{
				new Frame(0, 1),
				new Frame(1, 0),
				new Frame(1, 1)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(4, result);
		}
	}

	public class SpareTests {
		[Fact]
		public void Where_Spare_was_last() {
			Frame[] frames =
			{
				new Frame(0, 1),
				new Frame(1, 0),
				new Frame(1, 9)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(12, result);
		}

		[Fact]
		public void Where_Spare_means_no_bonus() {
			Frame[] frames =
			{
				new Frame(0, 1),
				new Frame(1, 9),
				new Frame(0, 0)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(11, result);
		}

		[Fact]
		public void Where_Spare_means_no_bonus2() {
			Frame[] frames =
			{
				new Frame(0, 1),
				new Frame(1, 9),
				new Frame(0, 1)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(12, result);
		}

		[Theory]
		[InlineData(2, 15)]
		[InlineData(3, 17)]
		[InlineData(10, 31)]
		public void Where_Spare_means_some_bonus(byte roll_after_spare, byte expected_score) {
			Frame[] frames =
			{
				new Frame(0, 1),
				new Frame(1, 9),
				new Frame(roll_after_spare, 0)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(expected_score, result);
		}
	}
}
