using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class Inventory
{
	private MeleeWeapon equippedMeleeWeapon;
	private RangedWeapon equippedRangedWeapon;
	private Shield equippedShield;
	private Magic equippedMagic1;
	private Magic equippedMagic2;
	private InventoryItem[] unequippedItems;
	private int unequippedItemsCount;
	public int healthPotions;
	public int manaPotions;

	public Inventory()
	{
		healthPotions = 2;
		manaPotions = 2;
		//TESTING LINES
		//unequippedItems = new InventoryItem[20];
		//unequippedItems [0] = new InventoryItem ();
		//unequippedItems [1] = new InventoryItem ();
		//unequippedItemsCount = 2;
		//equippedMagic1 = new Magic ();
		//equippedMagic2 = new Magic ();
		//equippedMeleeWeapon = new MeleeWeapon ();
		//equippedRangedWeapon = new RangedWeapon ();
		//equippedShield = new Shield ();
		//Save();
	}

	public void Load()
	{

	}

	public void Save()
	{
		int[] items = new int[unequippedItemsCount];

		for(int i = 0; i<unequippedItemsCount; ++i)
		{
			items[i] = unequippedItems[i].GetItemID();
		}

		Dictionary<string, object> dictionary = new Dictionary<string, object>();

		dictionary.Add ("MeleeWeapon", equippedMeleeWeapon.GetItemID());
		dictionary.Add ("RangedWeapon", equippedRangedWeapon.GetItemID());
		dictionary.Add ("Shield", equippedShield.GetItemID());
		dictionary.Add ("Magic1", equippedMagic1.GetItemID());
		dictionary.Add ("Magic2", equippedMagic2.GetItemID());
		dictionary.Add ("UnequippedItems", items);
		dictionary.Add ("HealthPotions", healthPotions);
		dictionary.Add ("ManaPotions", manaPotions);
		
		string saveJSON = Json.Serialize (dictionary);
		Debug.Log ("Save: " + saveJSON);
		//PlayerPrefs.SetString ("PlayerInventory", saveJSON);
	}
}
