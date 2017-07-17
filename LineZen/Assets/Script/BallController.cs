using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour 
{
	public static BallController Instance{ get; private set;}

	public enum MovementStaus
	{
		None = 0,
		Straight,
		Left,
		Right,
		Stop
	}

	private float zMovementTime = 5f;
	private float xMovementTime = 0.2f;
	private float zOffset = 15f;
	private float xOffset = 0.5f;

	private int leanTweenId;

	private MovementStaus curMovementStatus;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate (this);
		}
	}

	void Start () 
	{
		curMovementStatus = MovementStaus.Straight;
		StraightBallMoveMent ();
	}

	private void StraightBallMoveMent()
	{
		leanTweenId = LeanTween.moveLocalZ (this.gameObject, this.transform.position.z + zOffset , zMovementTime).setOnComplete(OnComplete).id;	
	}

	public void LeftBallMovement()
	{
		StopBallMovement ();
		leanTweenId = LeanTween.moveLocalX (this.gameObject, this.transform.position.x - xOffset , xMovementTime).setOnComplete(OnComplete).id;	
	}

	public void RightBallMovement()
	{
		StopBallMovement ();
		leanTweenId = LeanTween.moveLocalX (this.gameObject, this.transform.position.x + xOffset , xMovementTime).setOnComplete(OnComplete).id;	
	}

	private void OnComplete()
	{
		StopBallMovement ();
		StraightBallMoveMent ();
	}

	private void StopBallMovement()
	{
		LeanTween.cancel (leanTweenId);
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.LogError ("other..." + other.gameObject.tag);
	}
		

}
