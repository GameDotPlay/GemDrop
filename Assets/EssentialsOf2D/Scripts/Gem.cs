namespace GemDrop
{
	using System;
	using UnityEngine;

	[RequireComponent(typeof(AudioSource))]
	public class Gem : MonoBehaviour
	{
		public static event Action<string> OnDestroyed;

		[SerializeField] private AudioClip clinkSound;
		[SerializeField] private GameObject deathAnimation;
		[SerializeField] private GameObject goalParticles;

		private AudioSource audioSource;

		private void Start()
		{
			this.audioSource = GetComponent<AudioSource>();
		}

		public void Kill(string tag)
		{
			switch(tag)
			{
				case "KillZone":
					this.Die();
					break;

				case "Goal":
					this.Goal();
					break;

				default:
					this.Die();
					break;
			}

			OnDestroyed?.Invoke(tag);
			Destroy(this.gameObject);
		}

		private void Die()
		{
			this.SpawnAnimation(this.deathAnimation);
		}

		private void Goal()
		{
			this.SpawnParticles(this.goalParticles);
		}

		private void PlayClip(AudioClip clip)
		{
			if (this.audioSource != null && clip != null)
			{
				this.audioSource.PlayOneShot(clip);
			}
		}

		private void SpawnParticles(GameObject particles)
		{
			if (particles != null)
			{
				Instantiate(particles, this.transform.position, Quaternion.Euler(-90, 0, 0));
			}
		}

		private void SpawnAnimation(GameObject animObject)
		{
			if (animObject != null)
			{
				Instantiate(animObject, this.transform.position, Quaternion.identity);
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.tag != "KillZone" && collision.gameObject.tag != "Goal")
			{
				this.PlayClip(this.clinkSound);
			}
		}
	}
}