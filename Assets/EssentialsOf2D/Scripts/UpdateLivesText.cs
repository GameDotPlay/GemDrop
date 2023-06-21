namespace GemDrop
{
	using UnityEngine;
	using TMPro;

	public class UpdateLivesText : MonoBehaviour
	{
		[SerializeField] private PlayerStats playerStats;

		private TMP_Text livesText;

		private void Awake()
		{
			PlayerStats.OnLivesChanged += this.UpdateLivesTextUI;

			this.livesText = GetComponent<TMP_Text>();
		}

		private void UpdateLivesTextUI(int newLives)
		{
			this.livesText.text = newLives.ToString();
		}
	}
}
