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

	public class StrikeTests {

		[Fact]
		public void Where_Strike_was_last() {
			Frame[] frames =
			{
				new Frame(0, 1),
				new Frame(1, 0),
				new Frame(10)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(12, result);
		}

		[Fact]
		public void Where_Bonus_was_lost() {
			Frame[] frames =
			{
				new Frame(0, 1),
				new Frame(10),
				new Frame(0, 0)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(11, result);
		}

		[Theory]
		[InlineData(1, 13)]
		[InlineData(2, 15)]
		[InlineData(3, 17)]
		[InlineData(10, 31)]
		public void Where_Bonus_was_delayed(byte delayed_roll, byte expected_result) {
			Frame[] frames =
			{
				new Frame(0, 1),
				new Frame(10),
				new Frame(0, delayed_roll)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(expected_result, result);
		}

		[Theory]
		[InlineData(1, 1, 15)]
		[InlineData(3, 2, 21)]
		[InlineData(1, 3, 19)]
		[InlineData(2, 8, 31)]
		public void Where_Bonus_comes_from_two_rolls(byte next_roll, byte delayed_roll, byte expected_result) {
			Frame[] frames =
			{
				new Frame(0, 1),
				new Frame(10),
				new Frame(next_roll, delayed_roll)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(expected_result, result);
		}
	}

	public class TenthFrameEdgeCases {

		[Fact]
		public void Where_Spare_happened() {
			Frame[] frames =
			{
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),

				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(2, 8, 3)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(9 * 2 + 10 + 2 * 3, result);
		}

		[Fact]
		public void Where_Strike_happened() {
			Frame[] frames =
			{
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),

				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(10, 3)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(9 * 2 + 10 + 2 * 3, result);
		}

		[Fact]
		public void Where_nothing_happened() {
			Frame[] frames =
			{
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),

				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1),
				new Frame(1, 1)
			};

			byte result = Calculator.CalculateScore(frames);

			Assert.Equal(10 * 2, result);
		}
	}
}
