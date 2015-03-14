using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour 
{
	private Button levelButton;

	void Start()
	{
		SoundManager.Instance.PlayBackgroundMusic ("Level_Select_Scene_BackgroundMusic");
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
		SoundManager.Instance.PlayUISound ("Level_Start");
		Player.Instance.CurrentLevel = int.Parse(level.name);
		Application.LoadLevel("LevelScene");
	}
}
