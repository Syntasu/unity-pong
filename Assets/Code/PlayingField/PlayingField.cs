using UnityEngine;

namespace UnityPong
{
	public class PlayingField : MonoBehaviour
	{
		public Transform CenterPostion;
		public FloatRange DistanceFromCenter;

		public Vector3 GetPosition(GameObject obj)
		{
			return GetPosition(obj.transform.position);
		}

		public Vector3 GetPosition(Vector3 position)
		{
			return (position - CenterPostion.position).normalized * DistanceFromCenter.Value;
		}
	}
}
