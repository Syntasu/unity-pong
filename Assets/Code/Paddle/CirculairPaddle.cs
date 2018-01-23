using UnityEngine;

namespace UnityPong
{
	public class CirculairPaddle : MonoBehaviour
	{
		private Vector3 beginPosition;
		private Vector3 offset;
		private float startAngle = 0;

		private PaddleInput paddleInput;
		private PlayingField playingField;

		public float Rotation = 0.0f;

		public void Awake()
		{
			paddleInput = GetComponent<PaddleInput>();
			playingField = FindObjectOfType<PlayingField>();

			beginPosition = transform.position;

			Vector3 relativePos = beginPosition + offset;
			transform.position = playingField.GetPosition(relativePos);
		}

		public void Update()
		{
			transform.LookAt(playingField.CenterPostion);

			if(paddleInput.PaddleRightPressed)
			{
				offset += transform.right * 0.5f;
			}

			if(paddleInput.PaddleLeftPressed)
			{
				offset -= transform.right * 0.5f;
			}

			print(AngleBetweenVector2(playingField.CenterPostion.position, transform.position) - 90.0f);

		}

		private float AngleBetweenVector2(Vector3 a, Vector3 b)
		{
			Vector3 diference = b - a;
			float sign = (b.y < a.y) ? -1.0f : 1.0f;
			return Vector3.Angle(Vector3.right, diference) * sign;
		}

		public void FixedUpdate()
		{
			Vector3 relativePos = beginPosition + offset;
			transform.position = playingField.GetPosition(relativePos);
		}



	}
}
