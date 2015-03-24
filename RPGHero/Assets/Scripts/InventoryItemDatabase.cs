using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Pathfinding.Serialization.JsonFx;

public class InventoryItemDatabase 
{
	[JsonIgnore]
	private static InventoryItemDatabase instance;
	[JsonMember]
	private Database data;

	private InventoryItemDatabase() 
	{
		LoadDatabase ();
	}
	
	public static InventoryItemDatabase Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new InventoryItemDatabase();
			}
			return instance;
		}
	}
	
	public void Serialize()
	{
		data = new Database();
		data.items.Add(new MeleeWeapon(8,"Prefabs/AxePrefab",4,WeaponType.Melee,ItemType.Weapon,1,"Steel Axe","SteelAxeImage",8,true));
		data.items.Add(new RangedWeapon(3,2,WeaponType.Ranged, ItemType.Weapon,2,"Weak Bow","bow",5,true));
		data.items.Add(new Shield(30,30,"Prefabs/Shield1Prefab", ItemType.Shield,3,"Weak Shield","BigShield",5,true));
		data.items.Add(new LightningMagic (5,10,ItemType.Magic,10,"Lightning Attack","lightningIcon",-1,false));
		data.items.Add (new FireBlastMagic (3.0f,5,10,ItemType.Magic,11,"Fire Blast","fireBlastIcon",-1,false));
		data.items.Add(new FrostBlastMagic (2.5f,0.5f,10,ItemType.Magic,5,"Frost Blast","iceBlastIcon",-1,false));
		data.items.Add(new MeleeWeapon(10,"Prefabs/SteelSwordPrefab",5,WeaponType.Melee,ItemType.Weapon,6,"Steel Sword","SteelSwordImage",10,true));
		data.items.Add(new RangedWeapon(5,3,WeaponType.Ranged,ItemType.Weapon,7,"Wooden Bow","bow",7,true));
		data.items.Add(new Shield(50,50,"Prefabs/Shield1Prefab",ItemType.Shield,8,"Metal Shield","BigShield",9,true));
		
		//Settings
		JsonWriterSettings settings = new JsonWriterSettings();
		settings.PrettyPrint = true;
		settings.TypeHintName = "__type";
		
		JsonReaderSettings rsettings = new JsonReaderSettings ();
		rsettings.TypeHintName = "__type";         
		
		//create and print a json string
		System.Text.StringBuilder output = new System.Text.StringBuilder();
		JsonWriter wr = new JsonWriter(output,settings);
		wr.Write(data);
		
		string json = output.ToString();    
		Debug.Log(json);
	}
	
	void LoadDatabase()
	{
		Serialize ();
		data = new Database ();
		TextAsset asset = (TextAsset)Resources.Load ("ItemDatabase");
		JsonReaderSettings rsettings = new JsonReaderSettings ();
		rsettings.TypeHintName = "__type";
		JsonReader reader = new JsonReader(asset.text,rsettings);           
		data = (Database)reader.Deserialize(typeof(Database));
	}

	public InventoryItem GetItemByID(int id)
	{
		for(int i = 0; i < data.items.Count; ++i)
		{
			if(data.items[i].GetItemID()==id)
				return data.items[i];
		}

		return null;
	}
}
