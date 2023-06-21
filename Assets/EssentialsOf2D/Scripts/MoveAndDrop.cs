using Unity.VisualScripting;
using UnityEngine;

namespace GemDrop
{
	public class MoveAndDrop : MonoBehaviour
	{
		[SerializeField] private PersistentInfo persistentInfo;
		[SerializeField] private float moveSpeed = 10.0f;
		[SerializeField] private float gravityScale = 1f;
		[SerializeField] private float leftConstraint = -1.65f;
		[SerializeField] private float rightConstraint = 1.65f;
		[SerializeField] private float stationaryTimeToKill = 3.0f;

		private Rigidbody2D rb2d;
		private Gem gem;
		private float stationaryTime = 0.0f;
		private Vector3 lastPosition;

		private void Awake()
		{
			this.rb2d = GetComponent<Rigidbody2D>();
			this.gem = GetComponent<Gem>();
        }

        private void Start()
        {
            this.rb2d.gravityScale = this.gravityScale;
            this.rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
            this.persistentInfo.controlEnabled = true;
        }

        private void Update()
		{
            if (this.persistentInfo.controlEnabled)
            {
                this.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * this.moveSpeed * Time.deltaTime);

                // Player has dropped the gem and no longer has control.
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    this.rb2d.WakeUp();
                    this.persistentInfo.controlEnabled = false;
                    this.rb2d.constraints = RigidbodyConstraints2D.None;
                    this.rb2d.AddForce(Vector2.down);
                }
            }

			if (!this.persistentInfo.controlEnabled)
			{
				float velocity = (this.transform.position - lastPosition).magnitude;
				if (velocity < Mathf.Epsilon)
				{
					this.stationaryTime += Time.deltaTime;
				}
				else
				{
					this.stationaryTime = 0f;
				}

				if (this.stationaryTime > this.stationaryTimeToKill)
				{
					this.gem.Kill("KillZone");
				}
			}
		}

        private void LateUpdate()
		{
			if (this.transform.position.x < this.leftConstraint)
			{
				this.transform.position = new Vector3(this.leftConstraint, this.transform.position.y, this.transform.position.z);
			}
			else if (this.transform.position.x > this.rightConstraint)
			{
				this.transform.position = new Vector3(this.rightConstraint, this.transform.position.y, this.transform.position.z);
			}

			this.lastPosition = this.transform.position;
		}
	}
}