using UnityEngine;
using System.Collections;

public enum ItemType
{
	Weapon,
	Shield,
	Magic,
	Misc
};

public class InventoryItem 
{
	private ItemType itemType;
	private int itemID;
	private string itemName;
	private Sprite itemImage;
	private int itemCost;
	private bool purchasable;

	public InventoryItem()
	{

	}

	public InventoryItem(ItemType item, int id, string name, Sprite image, int cost, bool purchase)
	{
		itemType = item;
		itemID = id;
		itemName = name;
		itemImage = image;
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
		return itemImage;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
