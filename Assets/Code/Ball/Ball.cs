using UnityEngine;

namespace UnityPong
{
	public class Ball : MonoBehaviour
	{
		#region Data

		public float velocity = 0.2f;
		public Vector3 direction;
		private Rigidbody ballBody;

		#endregion

		#region Init

		private void Awake()
		{
			ballBody = GetComponent<Rigidbody>();

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

		private void OnCollisionEnter(Collision collision)
		{
			//Note: What could be potential implications of ignoring last collider? What a full on corner hit (i.e. > 2 colliders)?
			//Skip the last collider, since OnCollisionEnter can trigger multiple times for the same collider.
			if (lastCollider == collision.collider) return;

			if(collision.contacts.Length > 0)
			{
				direction = Vector3.Reflect(direction, collision.contacts[0].normal);
				direction.y = 0.0f;

				//TODO: Dont hardcode the velocity increase
				//TODO: Dont increase velocity for walls.
				velocity += 5.0f;

				lastCollider = collision.collider;
			}
		}

		#endregion
	}
}