using UnityEngine;
using System.Collections;

public class LevelSelectScript : MonoBehaviour {

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

	public void SelectLevelScene()
	{
		Application.LoadLevel("LevelScene");
	}
}
