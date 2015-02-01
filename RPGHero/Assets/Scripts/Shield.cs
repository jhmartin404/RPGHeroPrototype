using UnityEngine;
using System.Collections;

public class Shield : InventoryItem 
{
	private int defence;
	
	public int Defence
	{
		get
		{
			return defence;
		}
		set
		{
			defence = value;
		}
	}
	
	public Shield(int def, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(item, id, name, image, cost, purchase)
	{
		defence = def;
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Defence: " +defence + "\n";
		return result;
	}
}
