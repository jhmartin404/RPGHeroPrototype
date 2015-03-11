using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Pathfinding.Serialization.JsonFx;

public class Inventory
{
	private MeleeWeapon equippedMeleeWeapon;
	private RangedWeapon equippedRangedWeapon;
	private Shield equippedShield;
	private Magic equippedMagic1;
	private Magic equippedMagic2;
	private List<InventoryItem> unequippedItems;
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

	public RangedWeapon EquippedRangedWeapon
	{
		get
		{
			return equippedRangedWeapon;
		}
		set
		{
			equippedRangedWeapon = value;
		}
	}

	public Magic EquippedMagic1
	{
		get
		{
			return equippedMagic1;
		}
		set
		{
			equippedMagic1 = value;
		}
	}

	public Magic EquippedMagic2
	{
		get
		{
			return equippedMagic2;
		}
		set
		{
			equippedMagic2 = value;
		}
	}

	public Inventory()
	{
		Debug.Log ("Creating Inventory");

		healthPotions = 2;
		manaPotions = 2;
		coins = 0;
		//TESTING LINES
		unequippedItems = new List<InventoryItem>();
		unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(10));
		unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(1));
		unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(7));
		unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(8));
		equippedMagic1 = InventoryItemDatabase.Instance.GetItemByID (11) as Magic;
		equippedMagic2 = InventoryItemDatabase.Instance.GetItemByID (5) as Magic;
		equippedMeleeWeapon = (MeleeWeapon)InventoryItemDatabase.Instance.GetItemByID (6);
		equippedRangedWeapon = (RangedWeapon)InventoryItemDatabase.Instance.GetItemByID (2);
		equippedShield = (Shield)InventoryItemDatabase.Instance.GetItemByID (3);
	}

	public List<InventoryItem> GetUnequippedItems()
	{
		return unequippedItems;
	}

	public void AddUnequippedItem(InventoryItem itm)
	{
		unequippedItems.Add(itm);
	}

	public void RemoveUnequippedItem(InventoryItem itm)
	{
		unequippedItems.Remove (itm);
	}

	public void RemoveUnequippedItem(int index)
	{
		unequippedItems.RemoveAt(index);
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
