﻿using UnityEngine;
using System.Collections;

public class StoreScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.LoadLevel("LevelSelectScene");
			}
		}
	}
}
