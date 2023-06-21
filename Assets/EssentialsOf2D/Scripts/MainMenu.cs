namespace GemDrop
{
	using System.Collections;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private PersistentInfo persistentInfo;
		[SerializeField] private PlayerStats playerStats;
		[SerializeField] private GameObject fadeOut;

		private float fadeOutTime;

		private void Awake()
		{
			this.persistentInfo.controlEnabled = false;
			this.persistentInfo.numberOfScenes = SceneManager.sceneCountInBuildSettings;
			this.persistentInfo.currentSceneNumber = 0;
			this.playerStats.InitializeStats();
			Time.timeScale = 1.0f;
		}

		public void Play()
		{
			StartCoroutine(this.LoadNextScene());
		}

		private IEnumerator LoadNextScene()
		{
			this.persistentInfo.currentSceneNumber++;
			this.fadeOut.SetActive(true);
			this.fadeOutTime = this.fadeOut.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

			yield return new WaitForSeconds(this.fadeOutTime);
			SceneManager.LoadScene(this.persistentInfo.currentSceneNumber);
		}
	}
}