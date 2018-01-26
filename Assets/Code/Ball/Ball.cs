using UnityEngine;

namespace UnityPong
{
	public class Ball : MonoBehaviour
	{
		#region Data

		public float InitialVelocity;
		public float BounceSpeedup;
		public float BounceSpeedupFromPaddle;
			
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
			ballBody.velocity = Random.onUnitSphere;

			Vector3 dir = transform.forward;
		
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

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.contacts.Length > 0)
			{
				Transform hitObj = collision.transform;

				Vector3 localPoint = hitObj.InverseTransformPoint(collision.contacts[0].point);
				float sum = localPoint.x + localPoint.z;

				Vector3 reflect = Vector3.Reflect(direction, collision.contacts[0].normal);
				Vector3 right = hitObj.transform.right;

				direction = (reflect + right * sum).normalized;
				direction.y = 0.0f;

				//TODO: Tagging system that does allow for auto-completed tags.
				if (collision.collider.tag == "Player")
				{
					Paddle paddle = collision.gameObject.GetComponent<Paddle>();
					direction = (direction + paddle.Direction) / 2;

					float paddleBounceFactor = paddle.Direction.magnitude * BounceSpeedupFromPaddle;
					velocity += BounceSpeedup * (0.5f + paddleBounceFactor);
				}
			}
		}




		#endregion
	}
}