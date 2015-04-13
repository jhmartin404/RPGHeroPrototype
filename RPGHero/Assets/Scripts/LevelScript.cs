using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelScript : MonoBehaviour 
{
	private PlayerStats playerStats;
	public Image healthBar;
	public Image staminaBar;
	public Image manaBar;
	private bool iconSelected;
	private GameObject enemySpawner;
	private GameObject iconSpawner;
	private GameObject weapon;
	private GameObject shield;
	private bool playerLost;
	private bool playerWon;

	public bool IconSelected
	{
		get
		{
			return iconSelected;
		}
		set
		{
			iconSelected = value;
		}
	}

	public bool PlayerWon
	{
		get
		{
			return playerWon;
		}
		set
		{
			playerWon = value;
		}
	}

	void Awake()
	{
		string section = "Section1";
		if(Player.Instance.CurrentLevel<=5)
		{
			section = "Section1";
		}
		else if(Player.Instance.CurrentLevel<=10)
		{
			section = "Section2";
		}
		else if(Player.Instance.CurrentLevel<=15)
		{
			section = "Section3";
		}
		else if(Player.Instance.CurrentLevel<=20)
		{
			section = "Section4";
		}

		GameObject.Find ("Sky").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (section + "Sky");
		GameObject.Find ("Ground").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (section + "Ground");
		GameObject.Find ("Background").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (section + "Background");
		weapon = Instantiate(Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon.MeleeWeaponPrefab,transform.position,Quaternion.identity) as GameObject;
		weapon.name = "Weapon";
		shield = Instantiate (Player.Instance.GetPlayerInventory ().EquippedShield.ShieldPrefab, transform.position, Quaternion.identity) as GameObject;
		shield.name = "Shield";
	}

	// Use this for initialization
	void Start () 
	{
		playerLost = false;
		playerWon = false;
		playerStats = Player.Instance.GetPlayerStats ();
		//To make the game tougher change these to saved values instead of player stat values
		Player.Instance.Health = playerStats.HealthStat;
		Player.Instance.Mana = playerStats.MaxMana;
		Player.Instance.Stamina = playerStats.MaxStamina;

		healthBar.fillAmount = (float)(Player.Instance.Health / (float)playerStats.HealthStat);

		staminaBar.fillAmount = (float)(Player.Instance.Stamina/ (float)playerStats.MaxStamina);

		manaBar.fillAmount = (float)(Player.Instance.Mana / (float)playerStats.MaxMana);

		enemySpawner = GameObject.Find ("EnemySpawner");
		iconSpawner = GameObject.Find ("IconSpawner");
		if(SoundManager.Instance != null)
			SoundManager.Instance.PlayBackgroundMusic ("Level_Scene_BackgroundMusic");
	}

	// Update is called once per frame
	void Update () 
	{
		healthBar.fillAmount = (float)(Player.Instance.Health/ (float)playerStats.HealthStat);
		staminaBar.fillAmount = (float)(Player.Instance.Stamina/ (float)playerStats.MaxStamina);
		manaBar.fillAmount = (float)(Player.Instance.Mana/ (float)playerStats.MaxMana);

		if(Player.Instance.Health <= 0  && !playerLost)
		{
			playerLost = true;
			enemySpawner.GetComponent<EnemySpawner>().NotifyPlayerDied();
		}

		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				GoBack();
			}
		}
	}

	public void GoBack()
	{
		if(!playerLost && playerWon)
		{
			Player.Instance.GetPlayerInventory().Coins += Player.Instance.TemporaryCoins++;
			if(playerStats.GameLevel == Player.Instance.CurrentLevel)
			{
				playerStats.GameLevel++;
			}
		}
		Player.Instance.ExperienceCollected = 0;
		enemySpawner.GetComponent<EnemySpawner>().RemoveMethods();
		iconSpawner.GetComponent<IconSpawner>().RemoveMethods();
		Player.Instance.TemporaryCoins = 0;
		Player.Instance.Save();
		Application.LoadLevel ("LevelSelectScene");
	}
}
