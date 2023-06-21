namespace GemDrop
{
	using UnityEngine;

	public class KillZone : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "Gem")
			{
				Gem gem = other.gameObject.GetComponent<Gem>();
				if (gem != null)
				{
					gem.Kill(this.gameObject.tag);
				}
			}

			if (other.gameObject.tag == "DynamicObstacle")
			{
				Destroy(other.transform.root.gameObject);
			}
		}
	}
}