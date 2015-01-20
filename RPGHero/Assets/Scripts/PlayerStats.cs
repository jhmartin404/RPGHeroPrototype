using UnityEngine;
using System.Collections;

public class PlayerStats 
{
	private int gameLevel;
	private int expLevel = 1;
	private int currentExp = 35;
	private int neededExp = 50;
	private int maxStamina;
	private int maxMana;
	private int healthStat = 100;
	private int luckStat = 65;
	private int meleeStat = 70;
	private int rangedStat= 35;
	private int magicStat = 55;

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
