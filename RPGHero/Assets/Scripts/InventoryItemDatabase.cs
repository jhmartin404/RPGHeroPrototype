using UnityEngine;
using System.Collections;
using System.Xml;

public class InventoryItemDatabase 
{
	private static InventoryItemDatabase instance;

	private InventoryItem[] items;
	private int itemCount;

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
		XmlDocument database = new XmlDocument ();
		database.LoadXml (asset.text);
		//Debug.Log (database.ToString ());
		XmlNodeList inventoryList = database.GetElementsByTagName ("InventoryItem");

		foreach(XmlNode inventoryItem in inventoryList)
		{
			foreach(XmlNode node in inventoryItem)
			{
				if(node.Name == "itemID") 
				{ 
					Debug.Log("itemID: " + node.InnerText); 
				} 
				else if(node.Name == "itemType") 
				{ 
					Debug.Log("itemType: " + node.InnerText); 
				} 
				else if(node.Name == "itemName") 
				{ 
					Debug.Log("itemName: " + node.InnerText); 
				}
				else if(node.Name == "itemImage") 
				{ 
					Debug.Log("itemImage: " + node.InnerText); 
				}
				else if(node.Name == "itemCost") 
				{ 
					Debug.Log("itemCost: " + node.InnerText); 
				}
				else if(node.Name == "purchasable") 
				{ 
					Debug.Log("purchasable: " + node.InnerText); 
				}
			}

			//items[itemCount] = new InventoryItem();
			itemCount++;
		}
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
