﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterScene : MonoBehaviour 
{
	public Text levelText;
	public Text healthText;
	public Text luckText;
	public Text meleeText;
	public Text rangedText;
	public Text magicText;
	public Image expBar;

	private PlayerStats playerStats;
	private bool levelingUp;
	private int skillPoints;
	private int defaultPointsAmount = 10;
	private Object confirmButton;
	private Object plusButton;
	private Object minusButton;
	private Object remainingSkillPoints;
	private int startingHealth;
	private int startingLuck;
	private int startingMelee;
	private int startingRanged;
	private int startingMagic;

	// Use this for initialization
	void Start () 
	{
		levelingUp = false;
		confirmButton = Resources.Load ("Prefabs/ConfirmButton");
		plusButton = Resources.Load ("Prefabs/PlusButton");
		minusButton = Resources.Load ("Prefabs/MinusButton");
		remainingSkillPoints = Resources.Load ("Prefabs/RemainingSkillPoints");

		playerStats = Player.Instance.GetPlayerStats ();
		levelText.text = "Level " + playerStats.ExpLevel;

		healthText.text = "Health: " + playerStats.HealthStat;
		startingHealth = playerStats.HealthStat;

		luckText.text = "Luck: " + playerStats.LuckStat;
		startingLuck = playerStats.LuckStat;

		meleeText.text = "Melee: " + playerStats.MeleeStat;
		startingMelee = playerStats.LuckStat;

		rangedText.text = "Ranged: " + playerStats.RangedStat;
		startingRanged = playerStats.RangedStat;

		magicText.text = "Magic: " + playerStats.MagicStat;
		startingMagic = playerStats.MagicStat;

		expBar.fillAmount = (float)((float)playerStats.CurrentExp / (float)playerStats.NeededExp);
		SoundManager.Instance.PlayBackgroundMusic ("CharacterScene_BackgroundMusic");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Player.Instance.GetPlayerStats().LeveledUp() > 0 && !levelingUp)
		{
			Vector3 plusPosition = new Vector3(80,5,0);
			Vector3 minusPosition = new Vector3(100,5,0);
			levelingUp = true;
			//UNCOMMENT LATER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			skillPoints = defaultPointsAmount*Player.Instance.GetPlayerStats().LeveledUp();
			//skillPoints = 10;

			GameObject healthPlusButton = Instantiate(plusButton, plusPosition,Quaternion.identity) as GameObject;
			healthPlusButton.transform.SetParent(GameObject.Find ("HealthText").transform, false);
			healthPlusButton.GetComponent<Button>().onClick.AddListener(() => {IncreaseHealth();});

			GameObject healthMinusButton = Instantiate(minusButton, minusPosition,Quaternion.identity) as GameObject;
			healthMinusButton.transform.SetParent(GameObject.Find ("HealthText").transform, false);
			healthMinusButton.GetComponent<Button>().onClick.AddListener(() => {DecreaseHealth();});

			GameObject luckPlusButton = Instantiate(plusButton, plusPosition,Quaternion.identity) as GameObject;
			luckPlusButton.transform.SetParent(GameObject.Find ("LuckText").transform, false);
			luckPlusButton.GetComponent<Button>().onClick.AddListener(() => {IncreaseLuck();});
			
			GameObject luckMinusButton = Instantiate(minusButton, minusPosition,Quaternion.identity) as GameObject;
			luckMinusButton.transform.SetParent(GameObject.Find ("LuckText").transform, false);
			luckMinusButton.GetComponent<Button>().onClick.AddListener(() => {DecreaseLuck();});

			GameObject meleePlusButton = Instantiate(plusButton, plusPosition,Quaternion.identity) as GameObject;
			meleePlusButton.transform.SetParent(GameObject.Find ("MeleeText").transform, false);
			meleePlusButton.GetComponent<Button>().onClick.AddListener(() => {IncreaseMelee();});
			
			GameObject meleeMinusButton = Instantiate(minusButton, minusPosition,Quaternion.identity) as GameObject;
			meleeMinusButton.transform.SetParent(GameObject.Find ("MeleeText").transform, false);
			meleeMinusButton.GetComponent<Button>().onClick.AddListener(() => {DecreaseMelee();});

			GameObject rangedPlusButton = Instantiate(plusButton, plusPosition,Quaternion.identity) as GameObject;
			rangedPlusButton.transform.SetParent(GameObject.Find ("RangedText").transform, false);
			rangedPlusButton.GetComponent<Button>().onClick.AddListener(() => {IncreaseRanged();});
			
			GameObject rangedMinusButton = Instantiate(minusButton, minusPosition,Quaternion.identity) as GameObject;
			rangedMinusButton.transform.SetParent(GameObject.Find ("RangedText").transform, false);
			rangedMinusButton.GetComponent<Button>().onClick.AddListener(() => {DecreaseRanged();});

			GameObject magicPlusButton = Instantiate(plusButton, plusPosition,Quaternion.identity) as GameObject;
			magicPlusButton.transform.SetParent(GameObject.Find ("MagicText").transform, false);
			magicPlusButton.GetComponent<Button>().onClick.AddListener(() => {IncreaseMagic();});
			
			GameObject magicMinusButton = Instantiate(minusButton, minusPosition,Quaternion.identity) as GameObject;
			magicMinusButton.transform.SetParent(GameObject.Find ("MagicText").transform, false);
			magicMinusButton.GetComponent<Button>().onClick.AddListener(() => {DecreaseMagic();});

			GameObject remainingSkillPointsGameObject = Instantiate(remainingSkillPoints) as GameObject;
			remainingSkillPointsGameObject.transform.SetParent(GameObject.Find("Canvas").transform,false);
			remainingSkillPointsGameObject.name = "RemainingSkillPoints";
			UpdateLabels();

			GameObject confirmButtonGameObject = Instantiate(confirmButton) as GameObject;
			confirmButtonGameObject.transform.SetParent(GameObject.Find ("Canvas").transform, false);
			confirmButtonGameObject.GetComponent<Button>().onClick.AddListener(() => {CompleteLevelingUp();});
		}

		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				GoBack();
			}
		}
	}

	public void CompleteLevelingUp()
	{
		if(skillPoints>0)
		{
			Text points = GameObject.Find("RemainingSkillPoints").GetComponentInChildren<Text>();
			points.text = "Please assign \nremaining points.";
		}
		else
		{
			levelingUp = false;
			Player.Instance.GetPlayerStats().ResetLeveledUp();
			Player.Instance.Save();
			Button[] buttons = GameObject.FindObjectsOfType<Button> ();
			foreach(Button button in buttons)
			{
				if(button.name != "BackButton")
					Destroy(button.gameObject);
			}
			Destroy(GameObject.Find("RemainingSkillPoints"));
		}
	}

	public void UpdateLabels()
	{		
		healthText.text = "Health: " + playerStats.HealthStat;
		
		luckText.text = "Luck: " + playerStats.LuckStat;
		
		meleeText.text = "Melee: " + playerStats.MeleeStat;
		
		rangedText.text = "Ranged: " + playerStats.RangedStat;
		
		magicText.text = "Magic: " + playerStats.MagicStat;

		Text remainingSkillPointsLabel = GameObject.Find ("RemainingSkillPoints").GetComponentInChildren<Text> ();
		if(remainingSkillPointsLabel != null)
			remainingSkillPointsLabel.text = "Skill Points Remaing: " + skillPoints;
	}

	public void IncreaseHealth()
	{
		if(skillPoints > 0)
		{
			playerStats = Player.Instance.GetPlayerStats ();
			playerStats.HealthStat++;
			skillPoints--;
			UpdateLabels ();
		}
	}

	public void IncreaseLuck()
	{
		if(skillPoints>0)
		{
			playerStats = Player.Instance.GetPlayerStats ();
			playerStats.LuckStat++;
			skillPoints--;
			UpdateLabels ();
		}
	}

	public void IncreaseMelee()
	{
		if(skillPoints > 0)
		{
			playerStats = Player.Instance.GetPlayerStats ();
			playerStats.MeleeStat++;
			skillPoints--;
			UpdateLabels ();
		}
	}

	public void IncreaseRanged()
	{
		if(skillPoints > 0)
		{
			playerStats = Player.Instance.GetPlayerStats ();
			playerStats.RangedStat++;
			skillPoints--;
			UpdateLabels ();
		}
	}

	public void IncreaseMagic()
	{
		if(skillPoints > 0)
		{
			playerStats = Player.Instance.GetPlayerStats ();
			playerStats.MagicStat++;
			skillPoints--;
			UpdateLabels ();
		}
	}

	public void DecreaseHealth()
	{
		if(skillPoints < defaultPointsAmount*Player.Instance.GetPlayerStats().LeveledUp())
		{
			playerStats = Player.Instance.GetPlayerStats ();
			if(playerStats.HealthStat > startingHealth)
			{
				playerStats.HealthStat--;
				skillPoints++;
				UpdateLabels ();
			}
		}
	}
	
	public void DecreaseLuck()
	{
		if(skillPoints < defaultPointsAmount*Player.Instance.GetPlayerStats().LeveledUp())
		{
			playerStats = Player.Instance.GetPlayerStats ();
			if(playerStats.LuckStat > startingLuck)
			{
				playerStats.LuckStat--;
				skillPoints++;
				UpdateLabels ();
			}
		}
	}
	
	public void DecreaseMelee()
	{
		if(skillPoints < defaultPointsAmount*Player.Instance.GetPlayerStats().LeveledUp())
		{
			playerStats = Player.Instance.GetPlayerStats ();
			if(playerStats.MeleeStat > startingMelee)
			{
				playerStats.MeleeStat--;
				skillPoints++;
				UpdateLabels ();
			}
		}
	}
	
	public void DecreaseRanged()
	{
		if(skillPoints < defaultPointsAmount*Player.Instance.GetPlayerStats().LeveledUp())
		{
			playerStats = Player.Instance.GetPlayerStats ();
			if(playerStats.RangedStat > startingRanged)
			{
				playerStats.RangedStat--;
				skillPoints++;
				UpdateLabels ();
			}
		}
	}
	
	public void DecreaseMagic()
	{
		if(skillPoints < defaultPointsAmount*Player.Instance.GetPlayerStats().LeveledUp())
		{
			playerStats = Player.Instance.GetPlayerStats ();
			if(playerStats.MagicStat > startingMagic)
			{
				playerStats.MagicStat--;
				skillPoints++;
				UpdateLabels ();
			}
		}
	}

	public void GoBack()
	{
		Player.Instance.Save ();
		Application.LoadLevel("LevelSelectScene");
	}
}
