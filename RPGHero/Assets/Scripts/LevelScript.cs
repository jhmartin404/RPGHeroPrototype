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
	private GameObject weapon;
	private GameObject shield;

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

	void Awake()
	{
		weapon = Instantiate(Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon.MeleeWeaponPrefab,transform.position,Quaternion.identity) as GameObject;
		weapon.name = "Weapon";
		shield = Instantiate (Player.Instance.GetPlayerInventory ().EquippedShield.ShieldPrefab, transform.position, Quaternion.identity) as GameObject;
		shield.name = "Shield";
	}

	// Use this for initialization
	void Start () 
	{
		playerStats = Player.Instance.GetPlayerStats ();

		healthBar.fillAmount = (float)(Player.Instance.Health/ (float)playerStats.HealthStat);

		staminaBar.fillAmount = (float)(Player.Instance.Stamina/ (float)playerStats.MaxStamina);

		manaBar.fillAmount = (float)(Player.Instance.Mana/ (float)playerStats.MaxMana);

		enemySpawner = GameObject.Find ("EnemySpawner");
	}

	// Update is called once per frame
	void Update () 
	{
		healthBar.fillAmount = (float)(Player.Instance.Health/ (float)playerStats.HealthStat);
		staminaBar.fillAmount = (float)(Player.Instance.Stamina/ (float)playerStats.MaxStamina);
		manaBar.fillAmount = (float)(Player.Instance.Mana/ (float)playerStats.MaxMana);

		if(Player.Instance.Health <=0)
		{
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

	public void addCoin()
	{
		Player.Instance.GetPlayerInventory().Coins++;
	}

	public void GoBack()
	{
		Application.LoadLevel ("LevelSelectScene");
	}
}
