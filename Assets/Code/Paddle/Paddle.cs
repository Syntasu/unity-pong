using UnityEngine;

namespace UnityPong
{
	[RequireComponent(typeof(PaddleInput))]
	public class Paddle : MonoBehaviour
	{
		public float MovementSpeed;

	    public FloatRange DistanceRestriction;
	    public FloatRange AngleRestriction;

		public FloatRange Angle;
		public FloatRange Distance;

		private Vector3 center;
		private float radius = 30.0f;

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

        private void Awake()
	    {
	        playingField = FindObjectOfType<PlayingField>();
	        paddleInput = GetComponent<PaddleInput>();

            Distance.Min = radius + DistanceRestriction.Min;
            Distance.Max = radius + DistanceRestriction.Max;
            Distance.Value = radius;
        }

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
				Distance.Value > radius + DistanceRestriction.Min)
			{
				Distance.Value -= distanceMovement;
			}

			if (paddleInput.BackwardsPressed &&
				Distance.Value < radius + DistanceRestriction.Max)
			{
				Distance.Value += distanceMovement;
			}

            lastPosition = transform.position;
	    }

	    private void FixedUpdate()
	    {
			
	        float angle = paddleInput.IsInverted ? -this.Angle.Value : this.Angle.Value;
	        float distance = (radius + this.Distance.Value);

            Vector3 position = new Vector3(
	            center.x + Mathf.Sin(angle * Mathf.Deg2Rad) * distance,
	            transform.position.y,
                center.z + Mathf.Cos(angle * Mathf.Deg2Rad) * distance
            );

	        transform.position = position;

			//TODO: Is this null check really needed (i.e. Awake should run before FixedUpdate).
	        if (playingField != null)
	        {
	            Vector3 lookAt = playingField.CenterPosition.position;
	            lookAt.y = transform.localScale.y / 2;

                transform.LookAt(lookAt);
	        }
	    }
	}
}