using UnityEngine;

namespace UnityPong
{
	public class PlayingField : MonoBehaviour
	{
		public Transform CenterTransform;
		public FloatRange DistanceFromCenter;
		public float Radius;

		public Vector3 Center
		{
			get
			{
				return CenterTransform.transform.position;
			}
		}

		public Vector3 GetPosition(GameObject obj)
		{
			return GetPosition(obj.transform.position);
		}

		public Vector3 GetPosition(Vector3 position)
		{
			return (position - Center.normalized) * DistanceFromCenter.Value;
		}

	}
}
