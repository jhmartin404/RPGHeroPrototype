using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelScript : MonoBehaviour 
{
	private PlayerStats playerStats;
	private Image healthBar;
	private Image staminaBar;
	private Image manaBar;

	// Use this for initialization
	void Start () 
	{
		playerStats = Player.Instance.getPlayerStats ();

		healthBar = GameObject.Find ("RemainingHealthBar").GetComponent<Image> ();
		healthBar.fillAmount = (float)(Player.Instance.health/ (float)playerStats.GetHealthStat());

		staminaBar = GameObject.Find ("RemainingStaminaBar").GetComponent<Image> ();
		staminaBar.fillAmount = (float)(Player.Instance.stamina/ (float)playerStats.GetMaxStamina());

		healthBar = GameObject.Find ("RemainingManaBar").GetComponent<Image> ();
		healthBar.fillAmount = (float)(Player.Instance.mana/ (float)playerStats.GetMaxMana());
	}
	
	// Update is called once per frame
	void Update () 
	{
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
		Player.Instance.coins++;
	}
}
