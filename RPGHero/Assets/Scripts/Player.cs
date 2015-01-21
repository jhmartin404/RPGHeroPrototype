using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class Player 
{
	private static Player instance;

	private float health;
	private int coins;
	private float stamina;
	private float mana;
	private PlayerStats playerStats;
	private Inventory playerInventory;

	private Player() 
	{
		Load ();
	}

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

	public PlayerStats getPlayerStats()
	{
		Save ();
		return playerStats;
	}

	public Inventory getPlayerInventory()
	{
		return playerInventory;
	}

	void Load()
	{
		//PlayerPrefs.DeleteAll ();
		if(PlayerPrefs.HasKey("Player"))
		{
			Debug.Log("Player Here");

			string player = PlayerPrefs.GetString("Player");
			Debug.Log("Load: " + player);

			Dictionary<string, object> dict = new Dictionary<string, object>();
			dict = Json.Deserialize(player) as Dictionary<string, object>;

			object obj = dict["health"];
			health = (float)(double)obj;

			obj = dict["coins"];
			coins = (int)(double)obj;

			obj = dict["stamina"];
			stamina = (float)(double)obj;

			obj = dict["mana"];
			mana = (float)(double)obj;

			playerStats = new PlayerStats ();
			playerInventory = new Inventory ();
		}
		else
		{
			Debug.Log("Player not there");
			playerStats = new PlayerStats ();
			playerInventory = new Inventory ();
		}
	}

	public void Save()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();

		dictionary.Add ("health", health+2.01);
		dictionary.Add ("coins", coins+2.01);
		dictionary.Add ("stamina", stamina+2.01);
		dictionary.Add ("mana", mana+2.01);

		string saveJSON = Json.Serialize (dictionary);
		Debug.Log ("Save: " + saveJSON);
		PlayerPrefs.SetString ("Player", saveJSON);
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
