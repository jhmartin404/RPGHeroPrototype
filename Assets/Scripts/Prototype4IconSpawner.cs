﻿using UnityEngine;
using System.Collections;

public class Prototype4IconSpawner : MonoBehaviour 
{
	public GameObject swordPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;
	
	private float spawnTime = 1.0f;
	private float timeElapsed = 0.0f;
	private int iconChooser;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= spawnTime)
		{
			iconChooser = Random.Range(0,4);
			Vector3 position = new Vector3(Random.Range(-2.5f,2.5f),Random.Range(-3.0f,-1.4f),0);
			switch(iconChooser)
			{
			case 0:
				GameObject icon = Instantiate(swordPrefab, position, Quaternion.identity) as GameObject;
				break;
			case 1:
				GameObject icon2 = Instantiate(coinPrefab, position, Quaternion.identity) as GameObject;
				break;
			case 2:
				GameObject icon3 = Instantiate(fireBallPrefab, position, Quaternion.identity) as GameObject;
				break;
			case 3:
				GameObject icon4 = Instantiate(arrowPrefab, position, Quaternion.identity) as GameObject;
				break;
			}
			timeElapsed = 0.0f;
		}
	}
}
