using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour 
{
	private InventoryItem selectedItem;
	private InventoryItem comparedItem;
	private Text coinsText;
	// Use this for initialization
	void Start () 
	{
		coinsText = GameObject.Find ("CoinCount").GetComponent<Text>();
		coinsText.text = "Coins: " + Player.Instance.coins;
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
}
