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

		[SerializeField] private PaddleInputs forwardKey = PaddleInputs.W;
		[SerializeField] private PaddleInputs backwardsKey = PaddleInputs.S;
		[SerializeField] private PaddleInputs leftKey = PaddleInputs.A;
		[SerializeField] private PaddleInputs rightKey =PaddleInputs.D;

		[SerializeField] private bool _isInverted;

		public bool LeftPressed
		{
			get
			{
				return !_isInverted ?
					Input.GetKey((KeyCode)leftKey) :
					Input.GetKey((KeyCode)rightKey);
			}
		}

		public bool RightPressed
		{
			get
			{
				return !_isInverted ?
					Input.GetKey((KeyCode)rightKey) :
					Input.GetKey((KeyCode)leftKey);
			}

		}

		public bool BackwardsPressed
		{
			get
			{
				return !_isInverted ?
					Input.GetKey((KeyCode)backwardsKey) :
					Input.GetKey((KeyCode)forwardKey);
			}
		}

		public bool ForwardsPressed
		{
			get
			{
				return !_isInverted ?
					Input.GetKey((KeyCode)forwardKey) :
					Input.GetKey((KeyCode)backwardsKey);
			}
		}

		public bool IsInverted
		{
			get
			{
				return _isInverted;
			}
		}


		#endregion

		#region Init

		public void Awake()
		{
			PlayingField playingField = FindObjectOfType<PlayingField>();
			Vector3 directionToCenter = (transform.position - playingField.Center).normalized;

			//Detect which side the paddle is on, used for the correct key mapping.
			_isInverted = directionToCenter.x < 0;
		}

		#endregion
	}
}
