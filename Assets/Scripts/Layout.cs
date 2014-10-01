using UnityEngine;
using System.Collections;

public class Layout : MonoBehaviour 
{
	private static int coins = 0;
	private static bool iconSelected = false;
	// Use this for initialization
	void Start () 
	{
	
	}

	public static void setIconSelected(bool icon)
	{
		iconSelected = icon;
	}

	public static bool getIconSelected()
	{
		return iconSelected;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
		}

		if(Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			iconSelected = false;
		}
	}

	public static void addCoin()
	{
		coins++;
	}

	void OnGUI()
	{
		int buttonWidth = (int)(Screen.width * 0.3);
		int buttonHeight = (int)(Screen.height * 0.18);
		int healthWidth = (int)(Screen.width * 0.3);
		int healthHeight = (int)(Screen.height * 0.08);

		GUI.skin.label.fontSize = Screen.width / 25;
		
		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
		Rect buttonRect = new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			5,
			buttonWidth,
			buttonHeight
			);
		
		GUI.Label (buttonRect, "Coins: " +coins);

		Rect healthRect = new Rect (
			Screen.width - Screen.width + 5,
			5,
			healthWidth,
			healthHeight);

		GUI.Label (healthRect, "HP: " + EnemyScript.getHealth ());
	}
}
