﻿using UnityEngine;
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
		//coinsText = GameObject.Find ("CoinCount").GetComponent<Text>();
		coinsText.text = "" + playerInventory.Coins;
		//healthPotionText = GameObject.Find ("HealthPotionCount").GetComponent<Text>();
		healthPotionText.text = "" + playerInventory.HealthPotions;
		//manaPotionText = GameObject.Find ("ManaPotionCount").GetComponent<Text>();
		manaPotionText.text = "" + playerInventory.ManaPotions;
		filter = ItemType.Weapon;

		//inventorySlots = GameObject.FindGameObjectsWithTag ("InventorySlot");
		ResetBoard ();
		//slotCount = 0;
		//for(int i=0;i<unequippedItems.Count;++i)
		//{
		//	if(unequippedItems[i].GetItemType() == filter)
		//	{
		//		inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
		//		slotCount++;
		//	}
		//}
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
		//slotCount = 0;
		ResetBoard ();
		//for(int i=0;i<unequippedItems.Count;++i)
		//{
		//	if(unequippedItems[i].GetItemType() == filter)
		//	{
		//		inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
		//		slotCount++;
		//	}
		//}
	}

	public void SetFilterToShield()
	{
		filter = ItemType.Shield;
		//slotCount = 0;
		ResetBoard ();
		//for(int i=0;i<unequippedItems.Count;++i)
		//{
		//	if(unequippedItems[i].GetItemType() == filter)
		//	{
		//		inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
		//		slotCount++;
		//	}
		//}
	}

	public void SetFilterToMagic()
	{
		filter = ItemType.Magic;
		//slotCount = 0;
		ResetBoard ();
		//for(int i=0;i<unequippedItems.Count;++i)
		//{
		//	if(unequippedItems[i].GetItemType() == filter)
		//	{
		//		inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
		//		slotCount++;
		//	}
		//}
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
