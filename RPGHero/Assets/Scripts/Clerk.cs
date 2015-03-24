using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clerk 
{
	private List<List<InventoryItem>> itemsForSale;

	public Clerk()
	{
		itemsForSale = new List<List<InventoryItem>> ();
		for(int i = 0; i<10; i++)
		{
			itemsForSale.Add(new List<InventoryItem>());
			itemsForSale[i].Add(new HealthPotion(20,ItemType.Misc,12,"Health Potion","healthPotionUI",5,true));
			itemsForSale[i].Add(new ManaPotion(20, ItemType.Misc,13,"Mana Potion","manaPotionUI",5,true));
			itemsForSale[i].Add(new RepairHammer(15, ItemType.Misc,14,"Repair Hammer","RepairHammer",5,true));
		}
	}

	public List<InventoryItem> GetItems(int level)
	{
		if(level > 10)
		{
			level = 10;
		}
		return itemsForSale[level-1];
	}

	public bool AddItem(InventoryItem item)
	{
		if(itemsForSale[Player.Instance.GetPlayerStats().ExpLevel-1] != null)
		{
			itemsForSale[Player.Instance.GetPlayerStats().ExpLevel-1].Add(item);
			return true;
		}
		else
		{
			return false;
		}
	}
}
