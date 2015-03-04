using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour 
{
	private Button levelButton;
	// Use this for initialization
	void Start () 
	{

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
		Application.LoadLevel("StoreScene");
	}

	public void SelectCharacterScene()
	{
		Application.LoadLevel("CharacterScene");
	}

	public void SelectInventoryScene()
	{
		Application.LoadLevel("InventoryScene");
	}

	public void SelectLevelScene(Button level)
	{
		Player.Instance.CurrentLevel = int.Parse(level.name);
		Debug.Log (Player.Instance.CurrentLevel);
		Application.LoadLevel("LevelScene");
	}
}
