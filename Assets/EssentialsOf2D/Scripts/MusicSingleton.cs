namespace GemDrop
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [RequireComponent(typeof(AudioSource))]
    public class MusicSingleton : MonoBehaviour
    {
        public static MusicSingleton Instance { get; private set; }
        public static AudioSource musicInstance { get; private set; } 

        private void Awake()
        {
            SceneManager.activeSceneChanged += this.KillIfMainMenu;

            if (Instance != null && Instance != this)
            {
                Destroy(transform.root.gameObject);
            }
            else
            {
                Instance = this;
            }

            if (musicInstance != null && Instance != this)
            {
                Destroy(transform.root.gameObject);
            }
            else
            {
                musicInstance = GetComponent<AudioSource>();
            }

            DontDestroyOnLoad(gameObject);
        }

        private void KillIfMainMenu(Scene current, Scene next)
        {
            if (next.buildIndex == 0)
            {
                musicInstance.Stop();
                return;
            }
            else
            {
                if (!musicInstance.isPlaying)
                {
                    musicInstance.Play();
                }
            }
        }
    }
}