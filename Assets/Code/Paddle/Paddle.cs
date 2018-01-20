using UnityEngine;

namespace UnityPong
{
	[RequireComponent(typeof(PaddleInput))]
	public class Paddle : MonoBehaviour
	{
		#region Data

		public float MovementSpeed;
		public float MovementSmoothness;

		public FloatRange XRestriction;
		public FloatRange ZRestriction;

		private PaddleInput paddleInput;
		private Vector3 centerPosition;

		private Vector3 movementFrame = Vector3.zero;
		private Vector3 movementOffset = Vector3.zero;

		public Vector3 Direction
		{
			get
			{
				return (transform.position - lastPosition);
			}
		}

		#endregion

		#region Init

		public void Awake()
		{
			paddleInput = GetComponent<PaddleInput>();
			centerPosition = transform.position;
		}

		#endregion

		#region Logic

		Vector3 lastPosition = Vector3.zero;

		public void Update()
		{
			if(paddleInput.PaddleUpPressed)
			{
				movementFrame += transform.right * Time.deltaTime;
			}

			if (paddleInput.PaddleDownPressed)
			{
				movementFrame -= transform.right * Time.deltaTime;
			}

			if (paddleInput.PaddleForwardPressed)
			{
				movementFrame -= transform.forward * Time.deltaTime;
			}

			if (paddleInput.PaddleBackwardsPressed)
			{
				movementFrame += transform.forward * Time.deltaTime;
			}

			Debug.DrawLine(transform.position, transform.position + (Direction * 10));

			lastPosition = transform.position;
		}

		public void FixedUpdate()
		{
			movementOffset += (movementFrame * MovementSpeed * 10.0f);
		    movementFrame = Vector3.zero;

			XRestriction.Value = movementOffset.x;
			ZRestriction.Value = movementOffset.z;

			movementOffset = new Vector3(
				XRestriction.Value,
				movementOffset.y,
				ZRestriction.Value
			);

			transform.position = Vector3.Lerp(
				transform.position,
				centerPosition + movementOffset,
				MovementSmoothness * Time.fixedDeltaTime
			);
		}

		#endregion
	}
}
