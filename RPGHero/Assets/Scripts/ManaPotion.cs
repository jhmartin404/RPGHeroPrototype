using UnityEngine;
using System.Collections;

public class ManaPotion : InventoryItem 
{
	private float amount;

	public ManaPotion()
	{

	}

	public ManaPotion(float amt, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(item, id, name, imagePath, cost, purchase)
	{
		amount = amt;
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Amount: " + amount + "\n";
		return result;
	}
}
