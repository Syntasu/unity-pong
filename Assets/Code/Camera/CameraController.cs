using System.Collections.Generic;
using UnityEngine;

namespace UnityPong
{
	public class CameraController : MonoBehaviour
	{
		#region Data


		//public float s;
		public Transform CenterPoint;
		public Transform[] BoundPoints;

		private Camera camera;

		#endregion

		#region Init

		private void Update()
		{
			camera = GetComponent<Camera>();

		    float boundingBox = FindBoundingBox();
		    float fov = camera.fieldOfView;
		    float aspect = Mathf.Clamp(camera.aspect - 0.25f, 0.14f, 3.0f);

            float distance = Mathf.Abs((boundingBox * 2.0f) / Mathf.Tan(fov / 2.0f));

            transform.position = new Vector3(
		        CenterPoint.position.x,
		        1.5f * distance +  (distance/ aspect),
				CenterPoint.position.z
			);
		}

		private float FindBoundingBox()
		{
			float minX = float.MaxValue; //-55;
			float maxX = float.MinValue; //
			float minZ = float.MaxValue;
			float maxZ = float.MinValue;

			foreach (Transform point in BoundPoints)
			{
				Vector3 pos = point.position;

				if (pos.x < minX) minX = pos.x;
				if (pos.x > maxX) maxX = pos.x;

				if (pos.z < minZ) minZ = pos.z;
				if (pos.z > maxZ) maxZ = pos.z;
			}

			float x = Mathf.Abs(minX - maxX);
			float z = Mathf.Abs(minZ - maxZ);

			return Mathf.Max(x, z);
		}

		#endregion

	}
}
