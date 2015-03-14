using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterScene : MonoBehaviour 
{
	private PlayerStats playerStats;
	public Text levelText;
	public Text healthText;
	public Text luckText;
	public Text meleeText;
	public Text rangedText;
	public Text magicText;
	public Image expBar;
	//public AudioSource source;

	// Use this for initialization
	void Start () 
	{
		playerStats = Player.Instance.GetPlayerStats ();
		levelText.text = "Level " + playerStats.ExpLevel;

		healthText.text = "Health: " + playerStats.HealthStat;

		luckText.text = "Luck: " + playerStats.LuckStat;

		meleeText.text = "Melee: " + playerStats.MeleeStat;

		rangedText.text = "Ranged: " + playerStats.RangedStat;

		magicText.text = "Magic: " + playerStats.MagicStat;

		expBar.fillAmount = (float)((float)playerStats.CurrentExp / (float)playerStats.NeededExp);
		SoundManager.Instance.PlayBackgroundMusic ("CharacterScene_BackgroundMusic");
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
