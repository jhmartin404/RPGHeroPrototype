using UnityEngine;
using System.Collections;

public class InventoryItemDatabase 
{
	private InventoryItem[] items;
	private int itemCount;

	void LoadDatabase()
	{

	}

	public InventoryItem GetItemByID(int id)
	{
		for(int i = 0; i<itemCount; ++i)
		{
			if(items[i].GetItemID()==id)
				return items[i];
		}

		return null;
	}
}
