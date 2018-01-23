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

		[SerializeField] private PaddleInputs ForwardKey;
		[SerializeField] private PaddleInputs BackwardsKey;
		[SerializeField] private PaddleInputs LeftKey;
		[SerializeField] private PaddleInputs RightKey;

		private bool isInversed;

		public bool PaddleLeftPressed
		{
			get
			{
				return !isInversed ?
					Input.GetKey((KeyCode)ForwardKey) :
					Input.GetKey((KeyCode)BackwardsKey);
			}
		}

		public bool PaddleRightPressed
		{
			get
			{
				return !isInversed ?
					Input.GetKey((KeyCode)BackwardsKey) :
					Input.GetKey((KeyCode)ForwardKey);
			}
			
		}

		public bool PaddleBackwardsPressed
		{
			get
			{
				return !isInversed ?
					Input.GetKey((KeyCode)LeftKey) :
					Input.GetKey((KeyCode)RightKey);
			}
		}

		public bool PaddleForwardPressed
		{
			get
			{
				return !isInversed ?
					Input.GetKey((KeyCode)RightKey) :
					Input.GetKey((KeyCode)LeftKey);
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
