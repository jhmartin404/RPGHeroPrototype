using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelScript : MonoBehaviour 
{
	private PlayerStats playerStats;
	private Image healthBar;
	private Image staminaBar;
	private Image manaBar;
	private bool iconSelected;

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

	// Use this for initialization
	void Start () 
	{
		playerStats = Player.Instance.GetPlayerStats ();

		healthBar = GameObject.Find ("RemainingHealthBar").GetComponent<Image> ();
		healthBar.fillAmount = (float)(Player.Instance.Health/ (float)playerStats.HealthStat);

		staminaBar = GameObject.Find ("RemainingStaminaBar").GetComponent<Image> ();
		staminaBar.fillAmount = (float)(Player.Instance.Stamina/ (float)playerStats.MaxStamina);

		manaBar = GameObject.Find ("RemainingManaBar").GetComponent<Image> ();
		manaBar.fillAmount = (float)(Player.Instance.Mana/ (float)playerStats.MaxMana);
	}
	
	// Update is called once per frame
	void Update () 
	{
		healthBar.fillAmount = (float)(Player.Instance.Health/ (float)playerStats.HealthStat);
		staminaBar.fillAmount = (float)(Player.Instance.Stamina/ (float)playerStats.MaxStamina);
		manaBar.fillAmount = (float)(Player.Instance.Mana/ (float)playerStats.MaxMana);

		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.LoadLevel("LevelSelectScene");
			}
		}
	}

	public void addCoin()
	{
		Player.Instance.GetPlayerInventory().Coins++;
	}
}
