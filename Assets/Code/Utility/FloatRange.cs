using System;
using UnityEngine;

namespace UnityPong
{
	/// <summary>
	///		Defines a range (float) with a min and max.
	///		Automatically clamp value, also can assign random value.
	/// </summary>
	[Serializable]
	public class FloatRange
	{
		public float Min;
		public float Max;
		public bool Random = false;

		[SerializeField] private float value;

		public float Value
		{
			get
			{
				if (Random)
				{
					value = UnityEngine.Random.Range(Min, Max);
				}

				return Mathf.Clamp(value, Min, Max);
			}

			set
			{
				this.value = Mathf.Clamp(value, Min, Max);
			}
		}
	}
}
