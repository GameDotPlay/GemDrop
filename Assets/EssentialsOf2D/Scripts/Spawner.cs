namespace GemDrop
{
    using UnityEngine;

    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject spawnable;
        [SerializeField] private float speed;
        [SerializeField] private Direction direction;
        [SerializeField] private float spawnRate;
        [SerializeField] private float startDelay;

        private void Start()
        {
            InvokeRepeating(nameof(this.SpawnNew), this.startDelay, this.spawnRate);
        }

        private void SpawnNew()
        {
            GameObject newObject = Instantiate(this.spawnable, this.transform.position, Quaternion.identity);
            
            ScreenCrawl screenCrawlComponent = newObject.GetComponent<ScreenCrawl>();
            if (screenCrawlComponent != null)
            {
                screenCrawlComponent.crawlDirection = this.direction;
                screenCrawlComponent.speed = this.speed;
            }
        }
    }
}