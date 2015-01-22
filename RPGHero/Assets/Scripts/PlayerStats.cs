using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class PlayerStats 
{
	private int gameLevel;
	private int expLevel;
	private int currentExp;
	private int neededExp;
	private int maxStamina;
	private int maxMana;
	private int healthStat;
	private int luckStat;
	private int meleeStat;
	private int rangedStat;
	private int magicStat;

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
			currentExp = 0;
			neededExp = 100;
			maxStamina = 100;
			maxMana = 100;
			healthStat = 100;
			luckStat = 50;
			meleeStat = 50;
			rangedStat = 50;
			magicStat = 50;
		}
	}

	public void Load()
	{
		Debug.Log("Loading PlayerStats");
		
		string playerStats = PlayerPrefs.GetString("PlayerStats");
		Debug.Log("Load: " + playerStats);
		
		Dictionary<string, object> dict = new Dictionary<string, object>();
		dict = Json.Deserialize(playerStats) as Dictionary<string, object>;
		
		object obj = dict["gameLevel"];
		gameLevel = (int)(long)obj;
		
		obj = dict["expLevel"];
		expLevel = (int)(long)obj;
		
		obj = dict["currentExp"];
		currentExp = (int)(long)obj;
		
		obj = dict["neededExp"];
		neededExp = (int)(long)obj;

		obj = dict["maxStamina"];
		maxStamina = (int)(long)obj;

		obj = dict["maxMana"];
		maxMana = (int)(long)obj;

		obj = dict["healthStat"];
		healthStat = (int)(long)obj;

		obj = dict["luckStat"];
		luckStat = (int)(long)obj;

		obj = dict["meleeStat"];
		meleeStat = (int)(long)obj;

		obj = dict["rangedStat"];
		rangedStat = (int)(long)obj;

		obj = dict["magicStat"];
		magicStat = (int)(long)obj;
	}

	public void Save()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		
		dictionary.Add ("gameLevel", gameLevel);
		dictionary.Add ("expLevel", expLevel);
		dictionary.Add ("currentExp", currentExp);
		dictionary.Add ("neededExp", neededExp);
		dictionary.Add ("maxStamina", maxStamina);
		dictionary.Add ("maxMana", maxMana);
		dictionary.Add ("healthStat", healthStat);
		dictionary.Add ("luckStat", luckStat);
		dictionary.Add ("meleeStat", meleeStat);
		dictionary.Add ("rangedStat", rangedStat);
		dictionary.Add ("magicStat", magicStat);
		
		string saveJSON = Json.Serialize (dictionary);
		Debug.Log ("Save PlayerStats: " + saveJSON);
		PlayerPrefs.SetString ("PlayerStats", saveJSON);
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public int GetGameLevel()
	{
		return gameLevel;
	}

	public int GetExpLevel()
	{
		return expLevel;
	}

	public int GetCurrentExp()
	{
		return currentExp;
	}

	public int GetNeededExp()
	{
		return neededExp;
	}

	public int GetMaxStamina()
	{
		return maxStamina;
	}

	public int GetMaxMana()
	{
		return maxMana;
	}

	public int GetHealthStat()
	{
		return healthStat;
	}

	public int GetLuckStat()
	{
		return luckStat;
	}

	public int GetMeleeStat()
	{
		return meleeStat;
	}

	public int GetRangedStat()
	{
		return rangedStat;
	}

	public int GetMagicStat()
	{
		return magicStat;
	}


}
