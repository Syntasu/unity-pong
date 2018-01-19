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

		#endregion

		#region Init

		public void Awake()
		{
			paddleInput = GetComponent<PaddleInput>();
			centerPosition = transform.position;
		}

		#endregion

		#region Logic

		public void Update()
		{
			if(paddleInput.PaddleUpPressed)
			{
				movementFrame += transform.right;
			}

			if (paddleInput.PaddleDownPressed)
			{
				movementFrame -= transform.right;
			}

			if (paddleInput.PaddleForwardPressed)
			{
				movementFrame -= transform.forward;
			}

			if (paddleInput.PaddleBackwardsPressed)
			{
				movementFrame += transform.forward;
			}

			movementOffset += (movementFrame * Time.deltaTime * MovementSpeed) * 10.0f;
			movementFrame = Vector3.zero;
		
			XRestriction.Value = movementOffset.x;
			ZRestriction.Value = movementOffset.z;

			movementOffset = new Vector3(
				XRestriction.Value,
				movementOffset.y,
				ZRestriction.Value
			);

			//TODO: Paddle can escape if the movement speed is too high (i.e. dont multiply movementVector by speed.)
			transform.position = Vector3.Lerp(
				transform.position, 
				centerPosition + movementOffset, 
				MovementSmoothness * Time.deltaTime
			);
		}

		#endregion
	}
}
