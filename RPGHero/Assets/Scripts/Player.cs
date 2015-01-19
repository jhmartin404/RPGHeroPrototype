﻿using UnityEngine;
using System.Collections;

public class Player 
{
	private static Player instance;

	private float health;
	private float coins;
	private float stamina;
	private float mana;
	private PlayerStats playerStats;
	private Inventory playerInventory;

	private Player() {}

	public static Player Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new Player();
			}
			return instance;
		}
	}

	public float getHealth()
	{
		return health;
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
