using UnityEngine;

namespace UnityPong
{
	public class Ball : MonoBehaviour
	{
		#region Data

		public float InitialVelocity;
		public float BounceSpeedup;
			
		private float velocity;
		private  Vector3 direction;
		private Rigidbody ballBody;

		#endregion

		#region Init

		private void Awake()
		{
			ballBody = GetComponent<Rigidbody>();
			velocity = InitialVelocity;

			ResetBall();
		}

		private void ResetBall()
		{
			ballBody.velocity = Vector3.zero;

			Vector3 dir = Random.onUnitSphere;
			dir.y = 0.0f;

			direction = dir;
			ballBody.transform.localPosition = Vector3.zero;
		}

		#endregion

		#region Logic

		private void FixedUpdate()
		{
			//We need to clamp velocity, velocity >= 1000 could cause the ball to clip through the wall. 
			velocity = Mathf.Clamp(velocity, -1000, 1000);
			ballBody.velocity = (direction * velocity) * Time.deltaTime * 100.0f;
		}

		private Collider lastCollider = null;

		//TODO: Ball should bounce off the paddle based on the center position.
		//TODO: Paddle should be able to transfer his velocity to the ball.
		//TODO: Find out why OnCollisionEnter fires multiple times...
		private void OnCollisionEnter(Collision collision)
		{
			//Note: What could be potential implications of ignoring last collider? What a full on corner hit (i.e. > 2 colliders)?
			//TODO: Skipping colliders could become cumbersome if the collider is for instance circulair.....
			//Skip the last collider, since OnCollisionEnter can trigger multiple times for the same collider.
			if (lastCollider == collision.collider) return;

			if(collision.contacts.Length > 0)
			{
				direction = Vector3.Reflect(direction, collision.contacts[0].normal);
				direction.y = 0.0f;

				//TODO: Tagging system that does allow for auto-completed tags.
				if (collision.collider.tag == "Player")
				{
					velocity += BounceSpeedup;
				}

				lastCollider = collision.collider;
			}
		}

		#endregion
	}
}