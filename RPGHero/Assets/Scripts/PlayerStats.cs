using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class PlayerStats 
{
	private int gameLevel;
	private int expLevel;
	private int currentExp;
	private int neededExp;
	private int maxStamina;
	private int maxMana;
	private int healthStat;
	private int wisdomStat;
	private int meleeStat;
	private int rangedStat;
	private int magicStat;
	private int leveledUp;

	public int GameLevel
	{
		get
		{
			return gameLevel;
		}
		set
		{
			gameLevel = value;
		}
	}

	public int ExpLevel
	{
		get
		{
			return expLevel;
		}
		set
		{
			expLevel = value;
		}
	}

	public int CurrentExp
	{
		get
		{
			return currentExp;
		}
		set
		{
			currentExp = value;
		}
	}

	public int NeededExp
	{
		get
		{
			return neededExp;
		}
		set
		{
			neededExp = value;
		}
	}

	public int MaxStamina
	{
		get
		{
			return maxStamina;
		}
		set
		{
			maxStamina = value;
		}
	}

	public int MaxMana
	{
		get
		{
			return maxMana;
		}
		set
		{
			maxMana = value;
		}
	}

	public int HealthStat
	{
		get
		{
			return healthStat;
		}
		set
		{
			healthStat = value;
		}
	}

	public int WisdomStat
	{
		get
		{
			return wisdomStat;
		}
		set
		{
			wisdomStat = value;
		}
	}

	public int MeleeStat
	{
		get
		{
			return meleeStat;
		}
		set
		{
			meleeStat = value;
		}
	}

	public int RangedStat
	{
		get
		{
			return rangedStat;
		}
		set
		{
			rangedStat = value;
		}
	}

	public int MagicStat
	{
		get
		{
			return magicStat;
		}
		set
		{
			magicStat = value;
		}
	}

	public PlayerStats()
	{
		if(PlayerPrefs.HasKey("PlayerStats"))
		{
			Load();
		}
		else
		{
			gameLevel = 1;
			expLevel = 1;
			leveledUp = 0;
			currentExp = 0;
			neededExp = 200;
			maxStamina = 200;
			maxMana = 200;
			healthStat = 200;
			wisdomStat = 50;
			meleeStat = 50;
			rangedStat = 50;
			magicStat = 50;
		}
	}

	public bool CheckLevelUp()
	{
		if(currentExp>=neededExp)
		{
			expLevel++;
			currentExp = currentExp - neededExp;
			neededExp *=expLevel;
			leveledUp++;
			return true;
		}
		else
		{
			return false;
		}
	}

	public int LeveledUp()
	{
		return leveledUp;
	}

	public void ResetLeveledUp()
	{
		leveledUp = 0;
	}

	private void Load()
	{
		string playerStats = PlayerPrefs.GetString("PlayerStats");
		Debug.Log("Load PlayerStats: " + playerStats);

		Dictionary<string, int> dict = new Dictionary<string, int>();
		JsonReader reader = new JsonReader(playerStats);           
		dict = (Dictionary<string,int>)reader.Deserialize(typeof(Dictionary<string,int>));

		gameLevel = dict["gameLevel"];

		expLevel = dict["expLevel"];

		currentExp = dict["currentExp"];

		neededExp = dict["neededExp"];

		leveledUp = dict ["leveledUp"];
	
		maxStamina = dict["maxStamina"];
	
		maxMana = dict["maxMana"];
	
		healthStat = dict["healthStat"];

		wisdomStat = dict["wisdomStat"];
	
		meleeStat = dict["meleeStat"];
	
		rangedStat = dict["rangedStat"];
	
		magicStat = dict["magicStat"];
	}

	public void Save()
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();

		dictionary.Add ("gameLevel", gameLevel);
		dictionary.Add ("expLevel", expLevel);
		dictionary.Add ("currentExp", currentExp);
		dictionary.Add ("neededExp", neededExp);
		dictionary.Add ("leveledUp", leveledUp);
		dictionary.Add ("maxStamina", maxStamina);
		dictionary.Add ("maxMana", maxMana);
		dictionary.Add ("healthStat", healthStat);
		dictionary.Add ("wisdomStat", wisdomStat);
		dictionary.Add ("meleeStat", meleeStat);
		dictionary.Add ("rangedStat", rangedStat);
		dictionary.Add ("magicStat", magicStat);

		//create and print a json string
		System.Text.StringBuilder output = new System.Text.StringBuilder();
		JsonWriter wr = new JsonWriter(output);
		wr.Write(dictionary);
		
		string json = output.ToString();     
		Debug.Log("SavedPlayerStats: " + json);
		PlayerPrefs.SetString ("PlayerStats", json);
	}
}
