﻿using UnityEngine;
using System.Collections;

public class Prototype7IconSpawner : MonoBehaviour 
{
	public GameObject swordPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;
	
	private float spawnTime = 0.5f;
	private float timeElapsed = 0.0f;
	private int iconChooser;
	private int rowChooser;
	private Vector3 firstRow;
	private Vector3 secondRow;
	private Vector3 rotateObject = new Vector3(0,0,180);
	
	// Use this for initialization
	void Start () 
	{
		firstRow = new Vector3 (8, -3.0f, 0);
		secondRow = new Vector3 (-8, -4.0f, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= spawnTime)
		{
			iconChooser = Random.Range(0,4);
			rowChooser = Random.Range(0,2);
			Vector3 position = (rowChooser==0) ? firstRow : secondRow;
			switch(iconChooser)
			{
			case 0:
				GameObject icon = Instantiate(swordPrefab, position, Quaternion.identity) as GameObject;
				if(position==firstRow)
				{
					icon.transform.Rotate(rotateObject);
				}
				break;
			case 1:
				GameObject icon2 = Instantiate(coinPrefab, position, Quaternion.identity) as GameObject;
				if(position==firstRow)
				{
					icon2.transform.Rotate(rotateObject);
				}
				break;
			case 2:
				GameObject icon3 = Instantiate(fireBallPrefab, position, Quaternion.identity) as GameObject;
				if(position==firstRow)
				{
					icon3.transform.Rotate(rotateObject);
				}
				break;
			case 3:
				GameObject icon4 = Instantiate(arrowPrefab, position, Quaternion.identity) as GameObject;
				if(position==firstRow)
				{
					icon4.transform.Rotate(rotateObject);
				}
				break;
			}
			timeElapsed = 0.0f;
		}
	}
}
