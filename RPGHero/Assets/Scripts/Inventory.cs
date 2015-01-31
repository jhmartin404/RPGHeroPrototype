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
	private int coins;
	private int healthPotions;
	private int manaPotions;

	public int Coins
	{
		get
		{
			return coins;
		}
		set
		{
			coins = value;
		}
	}

	public int HealthPotions
	{
		get
		{
			return healthPotions;
		}
		set
		{
			healthPotions = value;
		}
	}

	public int ManaPotions
	{
		get
		{
			return manaPotions;
		}
		set
		{
			manaPotions = value;
		}
	}

	public MeleeWeapon EquippedMeleeWeapon
	{
		get
		{
			return equippedMeleeWeapon;
		}
		set
		{
			equippedMeleeWeapon = value;
		}
	}

	public Shield EquippedShield
	{
		get
		{
			return equippedShield;
		}
		set
		{
			equippedShield = value;
		}
	}

	public Inventory()
	{
		Sprite melee = Resources.Load<Sprite> ("BigSword");
		Sprite ranged = Resources.Load<Sprite> ("bow");
		Sprite shield = Resources.Load<Sprite> ("BigShield");
		healthPotions = 2;
		manaPotions = 2;
		coins = 0;
		//TESTING LINES
		unequippedItems = new InventoryItem[20];
		unequippedItems [0] = new InventoryItem ();
		unequippedItems [1] = new InventoryItem ();
		unequippedItemsCount = 2;
		equippedMagic1 = new Magic ();
		equippedMagic2 = new Magic ();
		equippedMeleeWeapon = new MeleeWeapon(10,5,ItemType.Weapon,1,"Steel Sword",melee,10,true);
		equippedRangedWeapon = new RangedWeapon(5,3,ItemType.Weapon,2,"Wooden Bow",ranged,7,true);
		equippedShield = new Shield(50,ItemType.Shield,3,"Metal Shield",shield,9,true);
		//Save();
	}

	//public void Load()
	//{
	//
	//}

	//public void Save()
	//{
	//	int[] items = new int[unequippedItemsCount];
	//
	//	for(int i = 0; i<unequippedItemsCount; ++i)
	//	{
	//		items[i] = unequippedItems[i].GetItemID();
	//	}
	//
	//	Dictionary<string, object> dictionary = new Dictionary<string, object>();
	//
	//	dictionary.Add ("MeleeWeapon", equippedMeleeWeapon.GetItemID());
	//	dictionary.Add ("RangedWeapon", equippedRangedWeapon.GetItemID());
	//	dictionary.Add ("Shield", equippedShield.GetItemID());
	//	dictionary.Add ("Magic1", equippedMagic1.GetItemID());
	//	dictionary.Add ("Magic2", equippedMagic2.GetItemID());
	//	dictionary.Add ("UnequippedItems", items);
	//	dictionary.Add ("HealthPotions", healthPotions);
	//	dictionary.Add ("ManaPotions", manaPotions);
	//	
	//	string saveJSON = Json.Serialize (dictionary);
	//	Debug.Log ("Save: " + saveJSON);
	//	//PlayerPrefs.SetString ("PlayerInventory", saveJSON);
	//}
}
