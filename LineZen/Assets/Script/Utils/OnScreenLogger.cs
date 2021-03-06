﻿using UnityEngine;
using System.Collections;

public class OnScreenLogger : MonoBehaviour
{

	public static OnScreenLogger Instance;
	public string streamingError = "";

	private Vector2 scrollPosition = Vector2.zero;
	bool isDebugging = false;

	void Awake ()
	{
		
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			DestroyImmediate (this);
		}
	}
	
	void Start ()
	{
		Application.RegisterLogCallback (HandleLog);
	}

	void HandleLog (string logString, string stackTrace, LogType type)
	{
		if (type == LogType.Error ||
			type == LogType.Exception ||
		//type == LogType.Warning ||
			type == LogType.Log) {
			//streamingError += "\n" + logString + "\n >>> " + stackTrace;
			streamingError = logString;
		}

		if (streamingError.Length > 10000) {
			streamingError = streamingError.Remove (0, 3000);
		}
	}
	
	void OnGUI ()
	{
		isDebugging = GUI.Toggle (new Rect (250, Screen.height - 25, 100, 25), isDebugging, "Debug");

		if (! isDebugging)
			return;

		GUILayout.BeginArea (new Rect (10, 10, 400, 300));
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (400), GUILayout.Height (300));
		GUILayout.TextField ("\nLogs\n : " + streamingError, "Label");
		GUILayout.EndScrollView ();
		GUILayout.EndArea ();
	}

}

