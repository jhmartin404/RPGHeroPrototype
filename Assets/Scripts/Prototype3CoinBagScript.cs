﻿using UnityEngine;
using System.Collections;

public class Prototype3CoinBagScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Coin")
		{
			Prototype3Layout.addCoin();
			Destroy(other.gameObject);
		}
	}
}
