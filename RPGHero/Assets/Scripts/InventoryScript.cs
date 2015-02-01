using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour 
{
	private Inventory playerInventory;
	private ItemType filter;
	private Text coinsText;
	private Text healthPotionText;
	private Text manaPotionText;
	public GameObject[] inventorySlots;
	private InventoryItem[] unequippedItems;
	private int slotCount;

	// Use this for initialization
	void Start () 
	{
		playerInventory = Player.Instance.GetPlayerInventory ();
		unequippedItems = playerInventory.GetUnequippedItems ();
		coinsText = GameObject.Find ("CoinCount").GetComponent<Text>();
		coinsText.text = "" + playerInventory.Coins;
		healthPotionText = GameObject.Find ("HealthPotionCount").GetComponent<Text>();
		healthPotionText.text = "" + playerInventory.HealthPotions;
		manaPotionText = GameObject.Find ("ManaPotionCount").GetComponent<Text>();
		manaPotionText.text = "" + playerInventory.ManaPotions;
		filter = ItemType.Weapon;

		//inventorySlots = GameObject.FindGameObjectsWithTag ("InventorySlot");
		slotCount = 0;
		for(int i=0;i<playerInventory.UnequippedItemsCount;++i)
		{
			if(unequippedItems[i].GetItemType() == filter)
			{
				inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
				slotCount++;
			}
		}
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

	public void SetFilterToWeapon()
	{
		filter = ItemType.Weapon;
		slotCount = 0;
		ResetBoard ();
		for(int i=0;i<playerInventory.UnequippedItemsCount;++i)
		{
			if(unequippedItems[i].GetItemType() == filter)
			{
				inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
				slotCount++;
			}
		}
	}

	public void SetFilterToShield()
	{
		filter = ItemType.Shield;
		slotCount = 0;
		ResetBoard ();
		for(int i=0;i<playerInventory.UnequippedItemsCount;++i)
		{
			if(unequippedItems[i].GetItemType() == filter)
			{
				inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
				slotCount++;
			}
		}
	}

	public void SetFilterToMagic()
	{
		filter = ItemType.Magic;
		slotCount = 0;
		ResetBoard ();
		for(int i=0;i<playerInventory.UnequippedItemsCount;++i)
		{
			if(unequippedItems[i].GetItemType() == filter)
			{
				inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
				slotCount++;
			}
		}
	}

	public void ResetBoard()
	{
		foreach(GameObject obj in inventorySlots)
		{
			obj.GetComponent<InventorySlot>().SetItem(null);
		}
	}
}
