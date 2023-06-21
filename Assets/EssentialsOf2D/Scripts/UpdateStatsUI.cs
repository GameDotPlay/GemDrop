namespace GemDrop
{
	using UnityEngine;
	using TMPro;

	public class UpdateStatsUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text livesText;
		[SerializeField] private TMP_Text scoreText;

		private void Awake()
		{
			PlayerStats.OnLivesChanged += this.UpdateLives;
			PlayerStats.OnScoreChanged += this.UpdateScore;
		}

		private void UpdateLives(int newLives)
		{
			this.livesText.text = newLives.ToString();
		}

		private void UpdateScore(int newScore)
		{
			this.scoreText.text = newScore.ToString();
		}
	}
}