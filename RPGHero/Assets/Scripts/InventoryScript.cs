using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour 
{
	private Inventory playerInventory;
	private ItemType filter;
	public Text coinsText;
	public Text healthPotionText;
	public Text manaPotionText;
	public GameObject[] inventorySlots;
	private List<InventoryItem> unequippedItems;
	private int slotCount;

	// Use this for initialization
	void Start () 
	{
		playerInventory = Player.Instance.GetPlayerInventory ();
		unequippedItems = playerInventory.GetUnequippedItems ();
		coinsText.text = "" + playerInventory.Coins;
		healthPotionText.text = "" + playerInventory.HealthPotions;
		manaPotionText.text = "" + playerInventory.ManaPotions;
		filter = ItemType.Weapon;
		SoundManager.Instance.PlayBackgroundMusic ("Store_Scene_BackgroundMusic");

		ResetBoard ();
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
		Player.Instance.Save();
		Application.LoadLevel("LevelSelectScene");
	}

	public void SetFilterToWeapon()
	{
		filter = ItemType.Weapon;
		ResetBoard ();
	}

	public void SetFilterToShield()
	{
		filter = ItemType.Shield;
		ResetBoard ();
	}

	public void SetFilterToMagic()
	{
		filter = ItemType.Magic;
		ResetBoard ();
	}

	public void ResetBoard()
	{
		foreach(GameObject obj in inventorySlots)
		{
			obj.GetComponent<InventorySlot>().SetItem(null);
		}
		slotCount = 0;
		for(int i=0;i<unequippedItems.Count;++i)
		{
			if(unequippedItems[i].GetItemType() == filter)
			{
				inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
				slotCount++;
			}
		}
	}
}
