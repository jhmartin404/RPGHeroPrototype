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
		levelText.text = "Level " + playerStats.GetExpLevel();

		healthText = GameObject.Find ("HealthText").GetComponent<Text>();
		healthText.text = "Health: " + playerStats.GetHealthStat();

		luckText = GameObject.Find ("LuckText").GetComponent<Text>();
		luckText.text = "Luck: " + playerStats.GetLuckStat();

		meleeText = GameObject.Find ("MeleeText").GetComponent<Text>();
		meleeText.text = "Melee: " + playerStats.GetMeleeStat();

		rangedText = GameObject.Find ("RangedText").GetComponent<Text>();
		rangedText.text = "Ranged: " + playerStats.GetRangedStat();

		magicText = GameObject.Find ("MagicText").GetComponent<Text>();
		magicText.text = "Magic: " + playerStats.GetMagicStat();

		expBar = GameObject.Find ("Exp").GetComponent<Image> ();
		expBar.fillAmount = (float)((float)playerStats.GetCurrentExp () / (float)playerStats.GetNeededExp ());
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
