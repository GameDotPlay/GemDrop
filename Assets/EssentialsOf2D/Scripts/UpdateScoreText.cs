namespace GemDrop
{
    using UnityEngine;
	using TMPro;

    public class UpdateScoreText : MonoBehaviour
    {
        [SerializeField] private PlayerStats playerStats;

		private TMP_Text scoreText;

		private void Awake()
		{
			PlayerStats.OnScoreChanged += this.UpdateScoreTextUI;

			this.scoreText = GetComponent<TMP_Text>();
		}

		private void UpdateScoreTextUI(int newScore)
		{
			this.scoreText.text = newScore.ToString();
		}
	}
}
