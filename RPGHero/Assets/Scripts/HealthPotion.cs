using UnityEngine;
using System.Collections;

public class HealthPotion : InventoryItem 
{
	private float amount;
	
	public HealthPotion()
	{
		
	}
	
	public HealthPotion(float amt, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(item, id, name, imagePath, cost, purchase)
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
