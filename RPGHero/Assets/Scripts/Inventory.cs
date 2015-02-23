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
		Sprite melee = Resources.Load<Sprite> ("SteelSwordImage");
		Sprite ranged = Resources.Load<Sprite> ("bow");
		Sprite shield = Resources.Load<Sprite> ("BigShield");
		Sprite axe = Resources.Load<Sprite> ("SteelAxeImage");
		Sprite fireBlast = Resources.Load<Sprite> ("fireBlastIcon");
		Sprite iceBlast = Resources.Load<Sprite> ("iceBlastIcon");

		Object axePrefab = Resources.Load ("Prefabs/AxePrefab");
		Object steelSwordPrefab = Resources.Load ("Prefabs/SteelSwordPrefab");

		healthPotions = 2;
		manaPotions = 2;
		coins = 0;
		//TESTING LINES
		unequippedItems = new List<InventoryItem>();
		unequippedItems.Add(new MeleeWeapon(8,axePrefab,4,WeaponType.Melee,ItemType.Weapon,1,"Steel Axe",axe,8,true));
		unequippedItems.Add(new RangedWeapon(3,2,WeaponType.Ranged, ItemType.Weapon,2,"Weak Bow",ranged,5,true));
		unequippedItems.Add(new Shield(30,ItemType.Shield,3,"Weak Shield",shield,5,true));
		unequippedItems.Add(new FireBlastMagic (2.0f,2.5f,7,ItemType.Magic,4,"Weak Fire Blast",fireBlast,-1,false));
		equippedMagic1 = new FireBlastMagic (3.0f,5,10,ItemType.Magic,4,"Fire Blast",fireBlast,-1,false);
		equippedMagic2 = new FrostBlastMagic (2.5f,2.5f,10,ItemType.Magic,5,"Frost Blast",iceBlast,-1,false);
		equippedMeleeWeapon = new MeleeWeapon(10,steelSwordPrefab,5,WeaponType.Melee,ItemType.Weapon,6,"Steel Sword",melee,10,true);
		equippedRangedWeapon = new RangedWeapon(5,3,WeaponType.Ranged,ItemType.Weapon,7,"Wooden Bow",ranged,7,true);
		equippedShield = new Shield(50,ItemType.Shield,8,"Metal Shield",shield,9,true);
		//Save();
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
