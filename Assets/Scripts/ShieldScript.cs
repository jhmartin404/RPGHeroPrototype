﻿using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Coin")
		{
			Layout.addCoin();
			Destroy(other.gameObject);
		}
	}
}
