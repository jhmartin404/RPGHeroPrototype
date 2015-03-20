using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public enum ItemType
{
	Weapon,
	Shield,
	Magic,
	Misc
};

public class InventoryItem 
{
	//[JsonMember]
	//private string itemClass;
	[JsonMember]
	private ItemType itemType;
	[JsonMember]
	private int itemID;
	[JsonMember]
	private string itemName;
	[JsonMember]
	private string itemImagePath;

	private Sprite itemImage;

	[JsonMember]
	private int itemCost;
	[JsonMember]
	private bool purchasable;

	public InventoryItem()
	{

	}

	public InventoryItem(ItemType item, int id, string name, string imagePath, int cost, bool purchase)
	{
		itemImagePath = imagePath;
		itemType = item;
		itemID = id;
		itemName = name;
		itemImage = Resources.Load<Sprite>(itemImagePath);
		itemCost = cost;
		purchasable = purchase;
	}

	public string GetItemName()
	{
		return itemName;
	}

	public int GetItemID()
	{
		return itemID;
	}

	public Sprite GetItemImage()
	{
		if(itemImage == null)
		{
			itemImage = Resources.Load<Sprite>(itemImagePath);
		}
		return itemImage;
	}

	public ItemType GetItemType()
	{
		return itemType;
	}

	public int GetItemCost()
	{
		return itemCost;
	}

	public override string ToString()
	{
		string result;
		result = itemName + "\n";
		if(purchasable)
			result += "Cost: " +itemCost + "\n";
		return result;
	}
}
