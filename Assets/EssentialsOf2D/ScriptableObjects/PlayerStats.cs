namespace GemDrop
{
	using System;
	using UnityEngine;

	[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
	public class PlayerStats : ScriptableObject
    {
		public static Action<int> OnScoreChanged;
		public static Action<int> OnLivesChanged;

		[SerializeField] private int score;
		[SerializeField] private int lives;
		[SerializeField] private int startingScore = 0;
		[SerializeField] private int startingLives = 3;

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

		public void InitializeStats()
		{
			this.CurrentLives = this.startingLives;
			this.CurrentScore = this.startingScore;
			PlayerStats.OnScoreChanged?.Invoke(this.CurrentScore);
			PlayerStats.OnLivesChanged?.Invoke(this.CurrentLives);
		}

		public int CurrentScore { get; private set; }

		public int CurrentLives { get; private set; }
	}
}