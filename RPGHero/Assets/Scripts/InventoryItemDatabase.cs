using UnityEngine;
using System.Collections;
using System.Xml;

public class InventoryItemDatabase 
{
	private InventoryItem[] items;
	private int itemCount;

	void LoadDatabase()
	{
		TextAsset asset = (TextAsset)Resources.Load ("ItemDatabase.xml");
		XmlDocument database = new XmlDocument ();
		database.LoadXml (asset.text);
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
