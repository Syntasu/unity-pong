using UnityEngine;

namespace UnityPong
{
	[RequireComponent(typeof(PaddleInput))]
	public class Paddle : MonoBehaviour
	{
		#region Data

		public float MovementSpeed;
	    public FloatRange DistanceRestriction;
	    public FloatRange AngleRestriction;

		public FloatRange Angle;
		public FloatRange Distance;

	    private PlayingField playingField;
	    private PaddleInput paddleInput;
	    private Vector3 lastPosition;

		public Vector3 Direction
        {
            get
            {
                return (transform.position - lastPosition);
            }
        }

		#endregion

		#region Init

		private void Awake()
		{
			playingField = FindObjectOfType<PlayingField>();
			paddleInput = GetComponent<PaddleInput>();

			Distance.Min = playingField.Radius + DistanceRestriction.Min;
			Distance.Max = playingField.Radius + DistanceRestriction.Max;
		}

		#endregion

		#region Logic 

		private void Update()
	    {
			float angleMovement = (5.0f * MovementSpeed) * Time.deltaTime;
			float distanceMovement = (3.0f * MovementSpeed) * Time.deltaTime;

			if (paddleInput.LeftPressed &&
				Angle.Value > AngleRestriction.Min)
			{
				Angle.Value -= angleMovement;
			}

			if (paddleInput.RightPressed &&
				Angle.Value < AngleRestriction.Max)
			{
				Angle.Value += angleMovement;
			}

			if (paddleInput.ForwardsPressed &&
				Distance.Value > playingField.Radius + DistanceRestriction.Min)
			{
				Distance.Value -= distanceMovement;
			}

			if (paddleInput.BackwardsPressed &&
				Distance.Value < playingField.Radius + DistanceRestriction.Max)
			{
				Distance.Value += distanceMovement;
			}

            lastPosition = transform.position;
	    }

	    private void FixedUpdate()
	    {
			
	        float angle = paddleInput.IsInverted ? -Angle.Value : Angle.Value;
	        float distance = (playingField.Radius + Distance.Value);

            Vector3 position = new Vector3(
	            playingField.Center.x + Mathf.Sin(angle * Mathf.Deg2Rad) * distance,
	            transform.position.y,
                playingField.Center.z + Mathf.Cos(angle * Mathf.Deg2Rad) * distance
            );

	        Vector3 lookAt = playingField.CenterTransform.position;
	        lookAt.y = transform.localScale.y / 2;

	        transform.position = position;
            transform.LookAt(lookAt);
	    }

		#endregion
	}
}