namespace GemDrop
{
	using UnityEngine;

	[RequireComponent(typeof(Animator))]
	public class DestroyOnAnimComplete : MonoBehaviour
	{
		[SerializeField] private float delay; // Seconds.

		void Start()
		{
			float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

			Destroy(this.gameObject, animTime + this.delay);
		}
	}
}