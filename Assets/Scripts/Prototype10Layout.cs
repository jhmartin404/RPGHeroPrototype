using UnityEngine;
using System.Collections;

public class Prototype10Layout : MonoBehaviour 
{
	private static int coins = 0;//player's coin count
	private static bool iconSelected = false;//detect if an icon has been selected
	private static bool defending = false;//detect if we are using a shield
	private static int playerHealth = 100;//player's health
	//private static int defences = 0;

	// Use this for initialization
	void Start () 
	{
		//disable the sword and swield on start
		GameObject.Find ("Sword").renderer.enabled = false;
		GameObject.Find ("Shield").renderer.enabled = false;
	}

//	public static void setDefences(int defs)
//	{
//		defences = defs;
//	}
//	
//	public static int getDefences()
//	{
//		return defences;
//	}

	public static void setDefending(bool def)
	{
		defending = def;
	}
	
	public static bool getDefending()
	{
		return defending;
	}

	public static void setIconSelected(bool icon)
	{
		iconSelected = icon;
	}

	public static bool getIconSelected()
	{
		return iconSelected;
	}

	public static int getCoins()
	{
		return coins;
	}

	public static void setCoins(int amount)
	{
		coins = amount;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//move back to menu if user presses back button
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.LoadLevel("MenuScene");
			}
		}

		if(Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			iconSelected = false;
		}
	}

	//Add coin
	public static void addCoin()
	{
		coins++;
	}

	public static void increaseHealth(int amount)
	{
		playerHealth += amount;
	}

	//Attack player
	public static void AttackPlayer(int damage)
	{
		if(!defending)
			playerHealth -= damage;
		else if(defending)
		{
			playerHealth -= damage/2;
		}
	}

	//GUI for the player
	void OnGUI()
	{
		int buttonWidth = (int)(Screen.width * 0.3);
		int buttonHeight = (int)(Screen.height * 0.18);
		int healthWidth = (int)(Screen.width * 0.3);
		int healthHeight = (int)(Screen.height * 0.08);

		GUI.skin.label.fontSize = Screen.width / 20;
		

		Rect buttonRect = new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			5,
			buttonWidth,
			buttonHeight
			);
		
		GUI.Label (buttonRect, "Coins: " +coins);

		Rect playerHealthRect = new Rect (
			Screen.width-300,
			5,
			healthWidth,
			healthHeight
			);

		GUI.Label (playerHealthRect, "HP: " + playerHealth);
	}
}
