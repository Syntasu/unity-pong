using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPong
{ 
	public class TestPlayingFieldObj : MonoBehaviour
	{
		private PlayingField playingField;

		public void Awake()
		{
			playingField = FindObjectOfType<PlayingField>();
		}

		public void Update()
		{
			transform.LookAt(playingField.CenterPostion);

			transform.position = playingField.GetPosition(gameObject);

			if(Input.GetKey(KeyCode.A))
			{
				transform.position += transform.right;
			}

			if (Input.GetKey(KeyCode.D))
			{
				transform.position -= transform.right;
			}
		}

	}


}