using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
	public class UIController : MonoBehaviour 
	{
		public void OnLeftClick()
		{
			BallController.Instance.LeftBallMovement ();
		}

		public void OnRightClick()
		{
			BallController.Instance.RightBallMovement ();
		}
	}
}
