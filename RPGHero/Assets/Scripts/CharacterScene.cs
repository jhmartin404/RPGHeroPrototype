using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterScene : MonoBehaviour 
{
	private PlayerStats playerStats;
	private Text levelText;
	private Text healthText;
	private Text luckText;
	private Text meleeText;
	private Text rangedText;
	private Text magicText;
	private Image expBar;

	// Use this for initialization
	void Start () 
	{
		playerStats = Player.Instance.getPlayerStats ();
		levelText = GameObject.Find ("LevelText").GetComponent<Text>();
		levelText.text = "Level " + playerStats.ExpLevel;

		healthText = GameObject.Find ("HealthText").GetComponent<Text>();
		healthText.text = "Health: " + playerStats.HealthStat;

		luckText = GameObject.Find ("LuckText").GetComponent<Text>();
		luckText.text = "Luck: " + playerStats.LuckStat;

		meleeText = GameObject.Find ("MeleeText").GetComponent<Text>();
		meleeText.text = "Melee: " + playerStats.MeleeStat;

		rangedText = GameObject.Find ("RangedText").GetComponent<Text>();
		rangedText.text = "Ranged: " + playerStats.RangedStat;

		magicText = GameObject.Find ("MagicText").GetComponent<Text>();
		magicText.text = "Magic: " + playerStats.MagicStat;

		expBar = GameObject.Find ("Exp").GetComponent<Image> ();
		expBar.fillAmount = (float)((float)playerStats.CurrentExp / (float)playerStats.NeededExp);
	}
	
	// Update is called once per frame
	void Update () 
	{
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
		Application.LoadLevel("LevelSelectScene");
	}
}
