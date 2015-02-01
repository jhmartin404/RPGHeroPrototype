﻿using UnityEngine;
using System.Collections;

public class Magic : InventoryItem 
{
	private int manaCost;

	public Magic(int costMana,ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(item, id, name, image, cost, purchase)
	{
		manaCost = costMana;
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Mana Cost: " +manaCost + "\n";
		return result;
	}
}
