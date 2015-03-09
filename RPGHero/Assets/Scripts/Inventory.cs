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
		//Sprite melee = Resources.Load<Sprite> ("SteelSwordImage");
		//Sprite ranged = Resources.Load<Sprite> ("bow");
		//Sprite shield = Resources.Load<Sprite> ("BigShield");
		//Sprite axe = Resources.Load<Sprite> ("SteelAxeImage");
		//Sprite lightning = Resources.Load<Sprite> ("lightningIcon");
		//Sprite fireBlast = Resources.Load<Sprite> ("fireBlastIcon");
		//Sprite iceBlast = Resources.Load<Sprite> ("iceBlastIcon");

		//Object axePrefab = Resources.Load ("Prefabs/AxePrefab");
		//Object steelSwordPrefab = Resources.Load ("Prefabs/SteelSwordPrefab");
		//Object shield1Prefab = Resources.Load ("Prefabs/Shield1Prefab");

		healthPotions = 2;
		manaPotions = 2;
		coins = 0;
		//TESTING LINES
		unequippedItems = new List<InventoryItem>();
		unequippedItems.Add(new MeleeWeapon(8,"Prefabs/AxePrefab",4,WeaponType.Melee,ItemType.Weapon,1,"Steel Axe","SteelAxeImage",8,true));
		unequippedItems.Add(new RangedWeapon(3,2,WeaponType.Ranged, ItemType.Weapon,2,"Weak Bow","bow",5,true));
		unequippedItems.Add(new Shield(30,"Prefabs/Shield1Prefab", ItemType.Shield,3,"Weak Shield","BigShield",5,true));
		unequippedItems.Add(new FireBlastMagic (2.0f,2.5f,7,ItemType.Magic,4,"Weak Fire Blast","fireBlastIcon",-1,false));
		equippedMagic1 = new FireBlastMagic (3.0f,5,10,ItemType.Magic,11,"Fire Blast","fireBlastIcon",-1,false);
		//equippedMagic1 = new LightningMagic (5,10,ItemType.Magic,10,"Lightning Attack","lightningIcon",-1,false);
		equippedMagic2 = new FrostBlastMagic (2.5f,0.5f,10,ItemType.Magic,5,"Frost Blast","iceBlastIcon",-1,false);
		equippedMeleeWeapon = new MeleeWeapon(10,"Prefabs/SteelSwordPrefab",5,WeaponType.Melee,ItemType.Weapon,6,"Steel Sword","SteelSwordImage",10,true);
		equippedRangedWeapon = new RangedWeapon(5,3,WeaponType.Ranged,ItemType.Weapon,7,"Wooden Bow","bow",7,true);
		equippedShield = new Shield(50,"Prefabs/Shield1Prefab",ItemType.Shield,8,"Metal Shield","BigShield",9,true);
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

	public void Serialize()
	{
		//Serialize the Inventory
		List<InventoryItem> inventory = new List<InventoryItem> ();
		inventory.Add (equippedMagic1);
		inventory.Add (EquippedMagic2);
		inventory.Add (equippedMeleeWeapon);
		inventory.Add (equippedRangedWeapon);
		inventory.Add (equippedShield);
		inventory.AddRange (unequippedItems);

		StringBuilder result = new StringBuilder(string.Empty);
		
		JsonWriterSettings settings = JsonDataWriter.CreateSettings(true);
		settings.TypeHintName = "__type";//Store the type of the inventoryitem
		
		JsonWriter writer = new JsonWriter(result, settings);
		
		writer.Write(inventory);
		
		Debug.Log(result.ToString());//Print out the resulting Json

		//Deserialize as a list of InventoryItems
		List<InventoryItem> items = JsonReader.Deserialize<List<InventoryItem>> (result.ToString());

		//Print out each item
		for(int i=0;i<items.Count;++i)
		{
			Debug.Log(items[i]);
		}
	}

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
