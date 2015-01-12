using UnityEngine;
using System.Collections;

public class Player 
{
	private static Player instance;

	private float health;
	private float coins;
	private PlayerStats playerStats;

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
