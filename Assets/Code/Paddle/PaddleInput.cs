using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPong
{
	/// <summary>
	///		All the inputs that a paddle can use/receive.
	///		PaddleInputs get casted to KeyCode (hence the integer representation).
	///		This replaces the abomination called the KeyCode enum.
	/// </summary>
	public enum PaddleInputs : int
	{
		W = 119,
		S = 115,
		A = 97,
		D = 100, 

		UpArrow = 273,
		DownArrow = 274,
		LeftArrow = 276,
		RightArrow = 275
	}

	//TODO: Ideally we want to have input maps for WASD and arrow keys (incl. inversed WASD/Arrow keys)
	public class PaddleInput : MonoBehaviour
	{
		#region Data

		[SerializeField] private PaddleInputs PaddleUpKey;
		[SerializeField] private PaddleInputs PaddleDownKey;
		[SerializeField] private PaddleInputs PaddleDownLeft;
		[SerializeField] private PaddleInputs PaddleDownRight;

		private bool isInversed;

		public bool PaddleUpPressed
		{
			get
			{
				return !isInversed ?
					Input.GetKey((KeyCode)PaddleUpKey) :
					Input.GetKey((KeyCode)PaddleDownKey);
			}
		}

		public bool PaddleDownPressed
		{
			get
			{
				return !isInversed ?
					Input.GetKey((KeyCode)PaddleDownKey) :
					Input.GetKey((KeyCode)PaddleUpKey);
			}
			
		}

		public bool PaddleBackwardsPressed
		{
			get
			{
				return !isInversed ?
					Input.GetKey((KeyCode)PaddleDownLeft) :
					Input.GetKey((KeyCode)PaddleDownRight);
			}
		}

		public bool PaddleForwardPressed
		{
			get
			{
				return !isInversed ?
					Input.GetKey((KeyCode)PaddleDownRight) :
					Input.GetKey((KeyCode)PaddleDownLeft);
			}
		}

		#endregion

		#region Init

		public void Awake()
		{
			//Detect which side the paddle is on, used for the correct key mapping.
			isInversed = transform.rotation.y > 0;
		}

		#endregion

	}
}
