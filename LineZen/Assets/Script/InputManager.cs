using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance { get; private set; }

	// screen width and height constants
	public const float DEVICE_WIDTH = 1024f;
	public const float DEVICE_HEIGHT = 1024f;

	public enum InputState
	{
		None = 0,
		Begin ,
		Stationary,
		Move,
		End,
	}

    public struct InputSegment
    {
        public Vector3 initInputPos;
        public Vector3 currInputPos;
        public Vector3 lastInputPos;
        public Vector3 deltaInputPos;
    }

	public Action OnPositionChanged;
    
	public InputSegment pan;

	public InputState curState;

    public float presentScreenWidth;
    public float presentScreenHeight;
    public float ratioWidth;
    public float ratioHeight;
    
    public bool isPanningActive;
    
	private Vector3 tempPosition;


	void Awake () 
    {
		if (Instance == null) 
        {
			Instance = this;
		}
		else
		{
			DestroyImmediate (this);
		}
	}

	void Start()
	{
		presentScreenWidth = Screen.width;
		presentScreenHeight = Screen.height;
		ratioWidth = DEVICE_WIDTH / presentScreenWidth;
		ratioHeight = DEVICE_HEIGHT / presentScreenHeight;
	}
	
	void Update () 
	{
		#if UNITY_EDITOR
			MouseMovement();
		#else
			TouchMovement();
		#endif
	}
	
	public void TouchMovement()
	{
		if(Input.touchCount ==1)
		{
			Touch touch = Input.touches[0];

			switch (touch.phase)
			{
				case TouchPhase.Began :
				{
					curState = InputState.Begin;
					tempPosition = Input.touches[0].position;
					tempPosition.x = tempPosition.x * ratioWidth;
					tempPosition.y = tempPosition.y * ratioHeight;
                    pan.initInputPos = pan.currInputPos = tempPosition;
					pan.deltaInputPos.x = touch.deltaPosition.x * ratioWidth;
					pan.deltaInputPos.y = touch.deltaPosition.y * ratioHeight;
                    isPanningActive = true;
				}
				break;

				case TouchPhase.Moved :
				{
					curState = InputState.Move;
					tempPosition = Input.touches[0].position;
					tempPosition.x = tempPosition.x * ratioWidth;
					tempPosition.y = tempPosition.y * ratioHeight;
                    pan.currInputPos = tempPosition;
					pan.deltaInputPos.x = touch.deltaPosition.x * ratioWidth;
					pan.deltaInputPos.y = touch.deltaPosition.y * ratioHeight;
					OnPositionChanged ();
				}
				break;

                case TouchPhase.Canceled:
				case TouchPhase.Ended :
				{
					curState = InputState.End;
					tempPosition = Input.touches[0].position;
					tempPosition.x = tempPosition.x * ratioWidth;
					tempPosition.y = tempPosition.y * ratioHeight;
                    pan.initInputPos = pan.currInputPos = pan.deltaInputPos = pan.lastInputPos = Vector3.zero;
					isPanningActive = false;
				}
				    break;
				default : 
                    break;
			}
		}

	}
		
	public void MouseMovement()
	{
        //Panning.
        pan.lastInputPos = pan.currInputPos;
        if (Input.GetMouseButtonDown(0))
        {
			curState = InputState.Move;
            pan.lastInputPos.x = pan.initInputPos.x = Input.mousePosition.x * ratioWidth;
            pan.lastInputPos.y = pan.initInputPos.y = Input.mousePosition.y * ratioHeight;
            isPanningActive = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
			curState = InputState.End;
            pan.currInputPos = pan.lastInputPos = pan.initInputPos = Vector3.zero;
            isPanningActive = false;
        }
        if (Input.GetMouseButton(0))
        {
            pan.currInputPos.x = Input.mousePosition.x * ratioWidth;
            pan.currInputPos.y = Input.mousePosition.y * ratioHeight;
        }
        pan.deltaInputPos = pan.currInputPos - pan.lastInputPos;
		if (isPanningActive) 
		{
			Debug.Log ("position..."+pan.currInputPos.x);
			//OnPositionChanged ();
		}
	}    
}
