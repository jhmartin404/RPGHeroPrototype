using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class InventoryItemDatabase 
{
	private static InventoryItemDatabase instance;

	private List<InventoryItem> items;

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

	void LoadDatabase()
	{
		TextAsset asset = (TextAsset)Resources.Load ("ItemDatabase");
		JsonReader reader = new JsonReader (asset.text);
		reader.Deserialize ();
	}

	public InventoryItem GetItemByID(int id)
	{
		for(int i = 0; i < items.Count; ++i)
		{
			if(items[i].GetItemID()==id)
				return items[i];
		}

		return null;
	}
}
