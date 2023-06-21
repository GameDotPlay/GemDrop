namespace GemDrop
{
	using System;

	public class PlayerStatsCore
	{
		public static Action<int> OnScoreChanged;
		public static Action<int> OnLivesChanged;

		public void ModifyScore(int delta)
		{
			this.CurrentScore += delta;
			PlayerStats.OnScoreChanged?.Invoke(this.CurrentScore);
		}

		public void ModifyLives(int delta)
		{
			this.CurrentLives += delta;
			PlayerStats.OnLivesChanged?.Invoke(this.CurrentLives);
		}

		public void InitializeStats(int startingLives, int startingScore)
		{
			this.CurrentLives = startingLives;
			this.CurrentScore = startingScore;
			PlayerStats.OnScoreChanged?.Invoke(this.CurrentScore);
			PlayerStats.OnLivesChanged?.Invoke(this.CurrentLives);
		}

		public int CurrentScore { get; set; }

		public int CurrentLives { get; set; }
	}
}