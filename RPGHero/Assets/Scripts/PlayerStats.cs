﻿using UnityEngine;
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

	public int LuckStat
	{
		get
		{
			return luckStat;
		}
		set
		{
			luckStat = value;
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
			//Load();
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

	public bool CheckLevelUp()
	{
		if(currentExp>=neededExp)
		{
			expLevel++;
			currentExp = currentExp - neededExp;
			neededExp *=expLevel;
			return true;
		}
		else
		{
			return false;
		}
	}

	//public void Load()
	//{
	//	Debug.Log("Loading PlayerStats");
	//	
	//	string playerStats = PlayerPrefs.GetString("PlayerStats");
	//	Debug.Log("Load: " + playerStats);
	//	
	//	Dictionary<string, object> dict = new Dictionary<string, object>();
	//	dict = Json.Deserialize(playerStats) as Dictionary<string, object>;
	//	
	//	object obj = dict["gameLevel"];
	//	gameLevel = (int)(long)obj;
	//	
	//	obj = dict["expLevel"];
	//	expLevel = (int)(long)obj;
	//	
	//	obj = dict["currentExp"];
	//	currentExp = (int)(long)obj;
	//	
	//	obj = dict["neededExp"];
	//	neededExp = (int)(long)obj;
	//
	//	obj = dict["maxStamina"];
	//	maxStamina = (int)(long)obj;
	//
	//	obj = dict["maxMana"];
	//	maxMana = (int)(long)obj;
	//
	//	obj = dict["healthStat"];
	//	healthStat = (int)(long)obj;
	//
	//	obj = dict["luckStat"];
	//	luckStat = (int)(long)obj;
	//
	//	obj = dict["meleeStat"];
	//	meleeStat = (int)(long)obj;
	//
	//	obj = dict["rangedStat"];
	//	rangedStat = (int)(long)obj;
	//
	//	obj = dict["magicStat"];
	//	magicStat = (int)(long)obj;
	//}

	//public void Save()
	//{
	//	Dictionary<string, object> dictionary = new Dictionary<string, object>();
	//	
	//	dictionary.Add ("gameLevel", gameLevel);
	//	dictionary.Add ("expLevel", expLevel);
	//	dictionary.Add ("currentExp", currentExp);
	//	dictionary.Add ("neededExp", neededExp);
	//	dictionary.Add ("maxStamina", maxStamina);
	//	dictionary.Add ("maxMana", maxMana);
	//	dictionary.Add ("healthStat", healthStat);
	//	dictionary.Add ("luckStat", luckStat);
	//	dictionary.Add ("meleeStat", meleeStat);
	//	dictionary.Add ("rangedStat", rangedStat);
	//	dictionary.Add ("magicStat", magicStat);
	//	
	//	string saveJSON = Json.Serialize (dictionary);
	//	Debug.Log ("Save PlayerStats: " + saveJSON);
	//	PlayerPrefs.SetString ("PlayerStats", saveJSON);
	//}
}
