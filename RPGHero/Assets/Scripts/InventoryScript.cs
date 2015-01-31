using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour 
{
	private InventoryItem selectedItem;
	private InventoryItem comparedItem;
	private ItemType filter;
	private Text coinsText;
	private Text healthPotionText;
	private Text manaPotionText;
	// Use this for initialization
	void Start () 
	{
		coinsText = GameObject.Find ("CoinCount").GetComponent<Text>();
		coinsText.text = "" + Player.Instance.getPlayerInventory().Coins;
		healthPotionText = GameObject.Find ("HealthPotionCount").GetComponent<Text>();
		healthPotionText.text = "" + Player.Instance.getPlayerInventory().HealthPotions;
		manaPotionText = GameObject.Find ("ManaPotionCount").GetComponent<Text>();
		manaPotionText.text = "" + Player.Instance.getPlayerInventory().ManaPotions;
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
