namespace GemDrop
{
	using UnityEngine;

	public class ScreenCrawl : MonoBehaviour
	{
		public Direction crawlDirection;
		public float speed = 10.0f;

		private void Update()
		{
			this.transform.Translate(Vector3.left * (float)this.crawlDirection * this.speed * Time.deltaTime);
		}
	}
}