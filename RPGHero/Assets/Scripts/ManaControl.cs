﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaControl : MonoBehaviour 
{
	private Text manaPotions;
	private float disableTime;
	// Use this for initialization
	void Start () 
	{
		manaPotions = gameObject.GetComponentInChildren<Text> ();
		disableTime = 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		manaPotions.text ="" + Player.Instance.getPlayerInventory ().manaPotions;
	}

	public void UseManaPotion()
	{
		if(Player.Instance.getPlayerInventory ().manaPotions > 0)
		{
			Player.Instance.mana += 20;
			Player.Instance.getPlayerInventory().manaPotions -= 1;
			gameObject.GetComponent<Button>().interactable = false;
			Invoke("ReactivateControl",disableTime);
		}
	}

	private void ReactivateControl()
	{
		gameObject.GetComponent<Button> ().interactable = true;
	}
}