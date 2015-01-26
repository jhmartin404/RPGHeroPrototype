using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour 
{
	private Common.ItemType itemType;
	private int itemID;
	private string itemName;
	private Sprite itemImage;
	private int itemCost;
	private bool purchasable;

	public InventoryItem()
	{

	}

	public InventoryItem(Common.ItemType item, int id, string name, int cost, bool purchase)
	{
		itemType = item;
		itemID = id;
		itemName = name;
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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
