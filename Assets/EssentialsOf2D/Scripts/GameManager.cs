namespace GemDrop
{
	using TMPro;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	/// <summary>
	/// This class manages the flow of the game, spawns new gems, keeps track of game over conditions, and loads the appropriate scenes for each scenario.
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		#region SerializedFields
		/// <summary>
		/// A <see cref="GameObject"/> that represents the player that can be spawned at runtime.
		/// </summary>
		[SerializeField] private GameObject gem;

		/// <summary>
		/// A <see cref="GameObject"/> that represents the end game UI. Will initialize as disabled and will be enabled when the game is over.
		/// </summary>
		[SerializeField] private GameObject endGameUI;

		/// <summary>
		/// A <see cref="GameObject"/> that contains the paused game UI./>
		/// </summary>
		[SerializeField] private GameObject pauseGamePopup;

		/// <summary>
		/// Reference to scriptable object for player stats.
		/// </summary>
		[SerializeField] private PlayerStats playerStats;

		/// <summary>
		/// Reference to scriptable object for persistent info.
		/// </summary>
		[SerializeField] private PersistentInfo persistentInfo;
		#endregion

		#region PrivateFields

		/// <summary>
		/// Represents the pause state of the game.
		/// </summary>
		private bool gamePaused = false;

		#endregion

		#region MonoMethods

		/// <summary>
		/// Subscribe to Gem.OnDestroyed so we can modify score/lives and check for game over.
		/// </summary>
		private void Start()
		{
			Gem.OnDestroyed += this.GemDestroyed;
			this.InitializeNewScene();
		}

		/// <summary>
		/// Gets called every animation frame.
		/// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab)) 
			{	
				this.TogglePause();
			}
        }

        /// <summary>
        /// Unsubscribe to any events if this object gets disabled.
        /// </summary>
        private void OnDisable()
		{
			Gem.OnDestroyed -= this.GemDestroyed;
		}

        #endregion

        #region Public

		/// <summary>
		/// Toggles the pause state of the game and enables/disables the pause game popup UI.
		/// </summary>
        public void TogglePause()
        {
			if (this.endGameUI.activeSelf == false)
			{
                this.gamePaused = !this.gamePaused;

                if (!this.gamePaused)
                {
                    Time.timeScale = 1;
                    this.persistentInfo.controlEnabled = true;
                    this.pauseGamePopup.SetActive(false);
                }
                else
                {
                    Time.timeScale = 0;
                    this.persistentInfo.controlEnabled = false;
                    this.pauseGamePopup.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Reload the main scene, which starts a new game.
        /// </summary>
        public void Retry()
		{
			this.persistentInfo.currentSceneNumber = 1;
			this.playerStats.InitializeStats();
			SceneManager.LoadScene(this.persistentInfo.currentSceneNumber);
		}

		/// <summary>
		/// Load the MainMenu scene.
		/// </summary>
		public void LoadMainMenu()
		{
			this.persistentInfo.currentSceneNumber = 0;
			this.playerStats.InitializeStats();
			SceneManager.LoadScene(this.persistentInfo.currentSceneNumber = 0);
		}
		#endregion

		#region Private
		/// <summary>
		/// Initializes a new scene.
		/// </summary>
		private void InitializeNewScene()
		{
            // Unpause the game if it was paused.
            Time.timeScale = 1;

            // Disable the game over UI.
            this.endGameUI.SetActive(false);

			// Initialize player stats.
			this.playerStats.InitializeStats();

			// Spawn first player gem at PlayerStart.
			this.SpawnGem(this.gem, this.persistentInfo.playerStart);
		}

		/// <summary>
		/// Called when a gem is destroyed either by dying or going into the goal.
		/// </summary>
		/// <param name="tag">The gameObject.tag of the object that killed the gem.</param>
		private void GemDestroyed(string tag)
		{
			switch(tag)
			{
				case "KillZone":
					this.playerStats.ModifyLives(-1);
					break;

				case "Goal":
					this.playerStats.ModifyScore(1);
					break;

				default:
					break;
			}

			// Check game conditions
			if (this.playerStats.CurrentLives < 1)
			{
				// GAME OVER!
				this.EndGame("GAME OVER!");
				return;
			}

			if (this.playerStats.CurrentScore >= this.persistentInfo.scoreToProgress)
			{
                this.persistentInfo.currentSceneNumber++;
                if (this.persistentInfo.currentSceneNumber > this.persistentInfo.numberOfScenes - 1)
                {
					// YOU WIN!
                    this.EndGame("YOU WIN!");
                    return;
                }

                
				SceneManager.LoadScene(this.persistentInfo.currentSceneNumber);
				return;
			}

			this.SpawnGem(this.gem, this.persistentInfo.playerStart);
		}

		private void EndGame(string message)
		{
            Time.timeScale = 0;
            this.persistentInfo.controlEnabled = false;
            this.endGameUI.GetComponentInChildren<TMP_Text>().text = message;
            this.endGameUI.SetActive(true);
        }

		/// <summary>
		/// Spawns a new Gem object.
		/// </summary>
		/// <param name="objectToSpawn">The GameObject to spawn.</param>
		/// <param name="position">The position to spawn the new item at.</param>
		private void SpawnGem(GameObject objectToSpawn, Vector3 position)
		{
			Instantiate(objectToSpawn, position, Quaternion.identity);
		}

		#endregion
	}
}