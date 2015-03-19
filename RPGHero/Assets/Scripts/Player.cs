using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class Player 
{
	private static Player instance;

	private float health;
	private float stamina;
	private float mana;
	private int temporaryCoins;
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

	public int TemporaryCoins
	{
		get
		{
			return temporaryCoins;
		}
		set
		{
			temporaryCoins = value;
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
		if(PlayerPrefs.HasKey("Player"))
		{
			Debug.Log("Loading Player");
			//just to initialize the Database *TEMPORARY*
			InventoryItemDatabase.Instance.GetItemByID(1);

			string player = PlayerPrefs.GetString("Player");
			Debug.Log("Load: " + player);

			Dictionary<string,float> dict = new Dictionary<string, float>();

			JsonReader reader = new JsonReader(player);           
			dict = (Dictionary<string,float>)reader.Deserialize(typeof(Dictionary<string,float>));

			health = dict["health"];

			stamina = dict["stamina"];

			mana = dict["mana"];

			currentLevel = 0;
			playerStats = new PlayerStats ();
			playerInventory = new Inventory ();

			//--------------------------------------------------------------------------------------------------------------------------------------------
			//HERE FOR TESTING, REMOVE WHEN NOT TESTING VERY IMPORTANT TO REMEMBER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//--------------------------------------------------------------------------------------------------------------------------------------------
			//health = playerStats.HealthStat;
			//mana = playerStats.MaxMana;
		}
		else
		{
			Debug.Log("Creating Player");
			//just to initialize the Database *TEMPORARY*
			InventoryItemDatabase.Instance.GetItemByID(1);
			playerStats = new PlayerStats ();
			playerInventory = new Inventory ();

			health = playerStats.HealthStat;
			stamina = playerStats.MaxStamina;
			mana = playerStats.MaxMana;
			currentLevel = 0;

		}
	}

	public void TakeDamage(Enemy enemy)
	{
		if(!IsDefending)
		{
			Health -= enemy.enemyAttackDamage;
		}
		else if(IsDefending)
		{
			float unblockedDamage = playerInventory.EquippedShield.BlockDamage(enemy.enemyAttackDamage);
			Health -= unblockedDamage;
		}
		Save ();
	}

	public void TakeDamageAmount(float damage)
	{
		if(!IsDefending)
		{
			Health -= damage;
		}
		else if(IsDefending)
		{
			float unblockedDamage = playerInventory.EquippedShield.BlockDamage(damage);
			Health -= unblockedDamage;
		}
	}

	public void AddExperience(int exp)
	{
		playerStats.CurrentExp += exp;
		playerStats.CheckLevelUp();
	}

	public void Save()
	{
		Dictionary<string, float> dictionary = new Dictionary<string, float>();
	
		dictionary.Add ("health", health);
		dictionary.Add ("stamina", stamina);
		dictionary.Add ("mana", mana);
		
		//create and print a json string
		System.Text.StringBuilder output = new System.Text.StringBuilder();
		JsonWriter wr = new JsonWriter(output);
		wr.Write(dictionary);
		
		string json = output.ToString();     
		Debug.Log("SavedPlayer: " + json);
		PlayerPrefs.SetString ("Player", json);

		playerStats.Save ();
		playerInventory.Save ();
	}
}
