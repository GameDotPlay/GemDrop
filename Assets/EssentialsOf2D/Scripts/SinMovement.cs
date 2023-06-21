namespace GemDrop
{
	using UnityEngine;

	public class SinMovement : MonoBehaviour
    {
		[SerializeField] private float incrementSpeed = 1.0f;
		[SerializeField] private float speed;
		[SerializeField] private float radianMultiplier = 1;

		private float counter = 0;

		private void Start()
		{
			this.counter = Random.Range(0.0f, 350.0f);
		}

		private void Update()
		{
			float rad = (this.counter % 360) * Mathf.Deg2Rad;
			this.transform.position += new Vector3(Mathf.Sin(rad * radianMultiplier) * speed * Time.deltaTime, 0, 0);
			this.counter += Time.deltaTime * incrementSpeed;
		}
	}
}