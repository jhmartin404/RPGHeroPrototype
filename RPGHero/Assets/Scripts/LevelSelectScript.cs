using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour 
{
	public Button[] levelButtons;
	public Sprite unlockedSprite;
	private Button levelButton;
	private bool notified;

	void Start()
	{
		notified = false;
		SoundManager.Instance.PlayBackgroundMusic ("Level_Select_Scene_BackgroundMusic");

		for(int i = 0; i<Player.Instance.GetPlayerStats().GameLevel; i++)
		{
			levelButtons[i].GetComponent<Image>().sprite = unlockedSprite;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(Player.Instance.GetPlayerStats().LeveledUp() > 0 && !notified)
		{
			notified = true;
			Text characterButton = GameObject.Find("CharacterButton").GetComponentInChildren<Text>();
			characterButton.text = "Level Up";
		}
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				Quit();
			}
		}
	}

	public void Quit()
	{
		Application.Quit();
	}
	
	public void SelectStoreScene()
	{
		SoundManager.Instance.PlayUISound ("Store_Select");
		Application.LoadLevel("StoreScene");
	}

	public void SelectCharacterScene()
	{
		SoundManager.Instance.PlayUISound ("Character_Select");
		Application.LoadLevel("CharacterScene");
	}

	public void SelectInventoryScene()
	{
		SoundManager.Instance.PlayUISound ("Inventory_Select");
		Application.LoadLevel("InventoryScene");
	}

	public void SelectLevelScene(Button level)
	{
		if(Player.Instance.GetPlayerStats().GameLevel >= int.Parse(level.name))
		{
			SoundManager.Instance.PlayUISound ("Level_Start");
			Player.Instance.CurrentLevel = int.Parse(level.name);
			LoadingScreen.Show();
			Application.LoadLevel("LevelScene");
		}
		else
		{
			SoundManager.Instance.PlayUISound("Locked_Sound");
		}
	}
}
