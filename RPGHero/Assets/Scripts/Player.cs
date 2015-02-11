using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class Player 
{
	private static Player instance;

	private float health;
	//private int coins;
	private float stamina;
	private float mana;
	private bool isDefending;
	private int currentLevel;
	private PlayerStats playerStats;
	private Inventory playerInventory;

	public float Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
		}
	}

	public float Stamina
	{
		get
		{
			return stamina;
		}
		set
		{
			stamina = value;
		}
	}

	public float Mana
	{
		get
		{
			return mana;
		}
		set
		{
			mana = value;
		}
	}

	public bool IsDefending
	{
		get
		{
			return isDefending;
		}
		set
		{
			isDefending = value;
		}
	}

	public int CurrentLevel
	{
		get
		{
			return currentLevel;
		}
		set
		{
			currentLevel = value;
		}
	}

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

	public PlayerStats GetPlayerStats()
	{
		return playerStats;
	}

	public Inventory GetPlayerInventory()
	{
		return playerInventory;
	}

	private void Load()
	{
		//PlayerPrefs.DeleteAll ();//For testing purposes
		if(PlayerPrefs.HasKey("Player"))
		{
			Debug.Log("Loading Player");

			string player = PlayerPrefs.GetString("Player");
			Debug.Log("Load: " + player);

			//Dictionary<string, object> dict = new Dictionary<string, object>();
			//dict = Json.Deserialize(player) as Dictionary<string, object>;

			//object obj = dict["health"];
			//health = (float)(double)obj;

			//object obj = dict["coins"];
			//coins = (int)(long)obj;

			//obj = dict["stamina"];
			//stamina = (float)(double)obj;

			//obj = dict["mana"];
			//mana = (float)(double)obj;


			//playerStats = new PlayerStats ();
			//playerInventory = new Inventory ();

			//health = 50;//playerStats.GetHealthStat();
			//stamina = playerStats.GetMaxStamina();
			//mana = 50;//playerStats.GetMaxMana();
		}
		else
		{
			Debug.Log("Creating Player");
			playerStats = new PlayerStats ();
			playerInventory = new Inventory ();

			health = playerStats.HealthStat;
			stamina = playerStats.MaxStamina;
			mana = playerStats.MaxMana;
			//playerInventory.Coins = 0;
			currentLevel = 0;
		}
	}

	//public void Save()
	//{
	//	Dictionary<string, object> dictionary = new Dictionary<string, object>();
	//
	//	//dictionary.Add ("health", health);
	//	dictionary.Add ("coins", coins);
		//dictionary.Add ("stamina", stamina);
		//dictionary.Add ("mana", mana);

	//	string saveJSON = Json.Serialize (dictionary);
	//	Debug.Log ("Save: " + saveJSON);
	//	PlayerPrefs.SetString ("Player", saveJSON);

	//	playerStats.Save ();
	//}
}
