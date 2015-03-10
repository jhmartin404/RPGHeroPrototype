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

		Debug.Log ("Creating Inventory");

		healthPotions = 2;
		manaPotions = 2;
		coins = 0;
		//TESTING LINES
		unequippedItems = new List<InventoryItem>();
		unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(5));
		unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(6));
		unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(7));
		unequippedItems.Add (InventoryItemDatabase.Instance.GetItemByID(8));
		equippedMagic1 = InventoryItemDatabase.Instance.GetItemByID (10) as Magic;
		equippedMagic2 = InventoryItemDatabase.Instance.GetItemByID (11) as Magic;
		equippedMeleeWeapon = (MeleeWeapon)InventoryItemDatabase.Instance.GetItemByID (1);
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

		StringBuilder builder = new StringBuilder();

		using (TextWriter textWriter = new StringWriter(builder))
		{
			JsonWriterSettings settings = JsonDataWriter.CreateSettings(true);
			settings.TypeHintName = "__type";//Store the type of the inventoryitem

			JsonWriter writer = new JsonWriter(textWriter,settings);
			writer.Write(inventory);
			
			Debug.Log( builder.ToString());
		}
		
		//JsonWriter writer = new JsonWriter(result, settings);
		
		//writer.Write(inventory);
		
		//Debug.Log(result.ToString());//Print out the resulting Json

		List<InventoryItem> items = Deserialize<List<InventoryItem>> (builder.ToString ());

		//Debug.Log ("Unequipped Items: " + items.GetUnequippedItems());
		//FrostBlastMagic items = Deserialize<FrostBlastMagic> (builder.ToString ());
		//Print out each item
		for(int i=0;i<items.Count;++i)
		{
			Debug.Log(items[i]);
		}
	}

	public static T Deserialize<T>(string json)
	{
		using (TextReader textReader = new StringReader(json))
		{
			var jsonReader = new JsonReader(textReader);
			return (T)jsonReader.Deserialize(typeof(T));
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
