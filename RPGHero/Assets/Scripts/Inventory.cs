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
		if(PlayerPrefs.HasKey("PlayerInventory"))
		{
			Load();
		}
		else
		{
			Debug.Log ("Creating Inventory");

			healthPotions = 2;
			manaPotions = 2;
			coins = 0;
			//Default Items
			unequippedItems = new List<InventoryItem>();
			unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(10));
			unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(1));
			unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(7));
			unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(3));
			equippedMagic1 = InventoryItemDatabase.Instance.GetItemByID (11) as Magic;
			equippedMagic2 = InventoryItemDatabase.Instance.GetItemByID (5) as Magic;
			equippedMeleeWeapon = (MeleeWeapon)InventoryItemDatabase.Instance.GetItemByID (6);
			equippedRangedWeapon = (RangedWeapon)InventoryItemDatabase.Instance.GetItemByID (2);
			equippedShield = (Shield)InventoryItemDatabase.Instance.GetItemByID (8);
		}
	}

	public List<InventoryItem> GetUnequippedItems()
	{
		return unequippedItems;
	}

	public void AddUnequippedItem(InventoryItem itm)
	{
		unequippedItems.Add(itm);
	}

	public bool RemoveUnequippedItem(InventoryItem itm)
	{
		return unequippedItems.Remove (itm);
	}

	public void RemoveUnequippedItem(int index)
	{
		unequippedItems.RemoveAt(index);
	}

	public void Load()
	{
		string playerInventory = PlayerPrefs.GetString("PlayerInventory");
		Debug.Log("Load PlayerInventory: " + playerInventory);
		
		Dictionary<string, int> dict = new Dictionary<string, int>();
		JsonReader reader = new JsonReader(playerInventory);           
		dict = (Dictionary<string,int>)reader.Deserialize(typeof(Dictionary<string,int>));

		EquippedMeleeWeapon = InventoryItemDatabase.Instance.GetItemByID(dict["MeleeWeapon"]) as MeleeWeapon;

		EquippedRangedWeapon = InventoryItemDatabase.Instance.GetItemByID(dict["RangedWeapon"]) as RangedWeapon;

		EquippedShield = InventoryItemDatabase.Instance.GetItemByID(dict["Shield"]) as Shield;
		equippedShield.Defence = dict ["ShieldDefence"];

		EquippedMagic1 = InventoryItemDatabase.Instance.GetItemByID(dict["Magic1"]) as Magic;

		EquippedMagic2 = InventoryItemDatabase.Instance.GetItemByID(dict["Magic2"]) as Magic;

		healthPotions = dict["HealthPotions"];

		manaPotions = dict["ManaPotions"];

		coins = dict["Coins"];

		int itemCount = dict["UnequippedItemCount"];
		unequippedItems = new List<InventoryItem>();

		for(int i = 0; i<itemCount; ++i)
		{
			InventoryItem item = InventoryItemDatabase.Instance.GetItemByID(dict["unequippedItem"+i]);
			if(item.GetType() == typeof(Shield))//if a shield then set the defence value
			{
				Shield shield = item as Shield;
				shield.Defence = dict["unequippedItem" + i + "Defence"];
				unequippedItems.Add(shield);
			}
			else
			{
				unequippedItems.Add(item);
			}
		}
	}

	public void Save()
	{
	
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
	
		dictionary.Add ("MeleeWeapon", equippedMeleeWeapon.GetItemID());
		dictionary.Add ("RangedWeapon", equippedRangedWeapon.GetItemID());
		dictionary.Add ("Shield", equippedShield.GetItemID());
		dictionary.Add ("ShieldDefence", (int)equippedShield.Defence);
		dictionary.Add ("Magic1", equippedMagic1.GetItemID());
		dictionary.Add ("Magic2", equippedMagic2.GetItemID());
		dictionary.Add ("UnequippedItemCount", unequippedItems.Count);
		dictionary.Add ("HealthPotions", healthPotions);
		dictionary.Add ("ManaPotions", manaPotions);
		dictionary.Add ("Coins", coins);

		
		for(int i = 0; i<unequippedItems.Count; ++i)
		{
			dictionary.Add("unequippedItem" + i,unequippedItems[i].GetItemID());
			if(unequippedItems[i].GetType() == typeof(Shield))//if a shield then save the defence value
			{
				Shield shield = unequippedItems[i] as Shield;
				dictionary.Add("unequippedItem" + i + "Defence",(int)shield.Defence);
			}
		}

		//create and print a json string
		System.Text.StringBuilder output = new System.Text.StringBuilder();
		JsonWriter wr = new JsonWriter(output);
		wr.Write(dictionary);
		
		string json = output.ToString();     
		Debug.Log("SavedPlayerInventory: " + json);
		PlayerPrefs.SetString ("PlayerInventory", json);
	}
}
