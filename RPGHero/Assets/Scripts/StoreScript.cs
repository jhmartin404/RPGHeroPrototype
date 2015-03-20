using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

enum StoreMode
{
	Buy,
	Sell
};

public class StoreScript : MonoBehaviour 
{
	private Inventory playerInventory;
	private List<InventoryItem> unequippedItems;
	private ItemType filter;
	public Text coinsText;
	public GameObject[] storeSlots;
	private int slotCount;
	private Clerk clerk;
	private StoreMode mode;

	// Use this for initialization
	void Start () 
	{
		clerk = new Clerk ();
		mode = StoreMode.Buy;
		SoundManager.Instance.PlayBackgroundMusic ("Store_Scene_BackgroundMusic");
		filter = ItemType.Misc;
		//playerInventory = Player.Instance.GetPlayerInventory ();
		unequippedItems = Player.Instance.GetPlayerInventory().GetUnequippedItems ();
		coinsText.text = "" + Player.Instance.GetPlayerInventory().Coins;
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

	void GoBack()
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

	public void SetFilterToMisc()
	{
		filter = ItemType.Misc;
		ResetBoard ();
	}

	public void ResetBoard()
	{
		foreach(GameObject obj in storeSlots)
		{
			obj.GetComponent<InventorySlot>().SetItem(null);
		}
		coinsText.text = "" + Player.Instance.GetPlayerInventory().Coins;
		slotCount = 0;
		if(mode == StoreMode.Sell)
		{
			unequippedItems = Player.Instance.GetPlayerInventory().GetUnequippedItems ();
			for(int i=0;i<unequippedItems.Count;++i)
			{
				if(unequippedItems[i].GetItemType() == filter)
				{
					storeSlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
					slotCount++;
				}
			}
		}
		else if(mode == StoreMode.Buy)
		{
			List<InventoryItem> items = clerk.GetItems(Player.Instance.GetPlayerStats().ExpLevel);
			for(int i=0;i<items.Count;++i)
			{
				if(items[i].GetItemType() == filter)
				{
					storeSlots[slotCount].GetComponent<InventorySlot>().SetItem(items[i]);
					slotCount++;
				}
			}
		}
	}
}
