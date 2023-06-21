namespace GemDrop
{
	using UnityEngine;

	public class CollisionSounds : MonoBehaviour
	{
		[SerializeField] AudioClip collisionSound;
		
		private AudioSource audioSource;

		private void Start()
		{
			this.audioSource = GetComponent<AudioSource>();
			this.audioSource.clip = this.collisionSound;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (this.audioSource.clip != null)
			{
				this.audioSource.PlayOneShot(this.audioSource.clip);
			}
		}
	}
}