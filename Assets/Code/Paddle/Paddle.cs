using UnityEngine;

namespace UnityPong
{
	[RequireComponent(typeof(PaddleInput))]
	public class Paddle : MonoBehaviour
	{
	    public FloatRange Angle;
	    public FloatRange Distance;

	    public FloatRange DistanceRestriction;
	    public FloatRange AngleRestriction;

	    public Vector3 Center;
	    public float Radius = 30.0f;

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

            Distance.Min = Radius + DistanceRestriction.Min;
            Distance.Max = Radius + DistanceRestriction.Max;
            Distance.Value = Radius;
        }

        private void Update()
	    {
	        if (paddleInput.LeftPressed && 
                Angle.Value > AngleRestriction.Min)
	        {
	            Angle.Value -= 0.5f;
	        }

	        if (paddleInput.RightPressed &&
                Angle.Value < AngleRestriction.Max)
	        {
                Angle.Value += 0.5f;
            }

            if (paddleInput.ForwardsPressed &&
                Distance.Value > Radius + DistanceRestriction.Min)
            {
                Distance.Value -= 0.1f;
            }

            if (paddleInput.BackwardsPressed &&
                Distance.Value < Radius + DistanceRestriction.Max)
            {
                Distance.Value += 0.1f;
            }

            lastPosition = transform.position;
	    }

	    private void FixedUpdate()
	    {
	        float angle = paddleInput.IsInverted ? -Angle.Value : Angle.Value;
	        float distance = (Radius + Distance.Value);

            Vector3 position = new Vector3(
	            Center.x + Mathf.Sin(angle * Mathf.Deg2Rad) * distance,
	            transform.position.y,
                Center.z + Mathf.Cos(angle * Mathf.Deg2Rad) * distance
            );

	        transform.position = position;

	        if (playingField != null)
	        {
	            Vector3 lookAt = playingField.CenterPosition.position;
	            lookAt.y = transform.localScale.y / 2;

                transform.LookAt(lookAt);
	        }
	    }
	}
}