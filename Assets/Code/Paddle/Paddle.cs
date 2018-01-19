using UnityEngine;

namespace UnityPong
{
	[RequireComponent(typeof(PaddleInput))]
	public class Paddle : MonoBehaviour
	{
		#region Data

		public FloatRange MovementSpeed;
		public FloatRange MovementSmoothness;

		public FloatRange XRestriction;
		public FloatRange ZRestriction;

		private PaddleInput paddleInput;
		private Vector3 centerPosition;

		private Vector3 movementVector = Vector3.zero;

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
				movementVector += transform.right;
			}

			if (paddleInput.PaddleDownPressed)
			{
				movementVector -= transform.right;
			}

			if (paddleInput.PaddleForwardPressed)
			{
				movementVector -= transform.forward;
			}

			if (paddleInput.PaddleBackwardsPressed)
			{
				movementVector += transform.forward;
			}

			//TODO: Seems like the restrictions get affected by movement speed.
			XRestriction.Value = movementVector.x;
			ZRestriction.Value = movementVector.z;

			movementVector = new Vector3(
				XRestriction.Value,
				movementVector.y,
				ZRestriction.Value
			);

			//TODO: Paddle can escape if the movement speed is too high (i.e. dont multiply movementVector by speed.)
			transform.position = Vector3.Lerp(
				transform.position, 
				centerPosition + (movementVector * MovementSpeed.Value), 
				MovementSmoothness.Value * Time.deltaTime
			);
		}

		#endregion
	}
}
