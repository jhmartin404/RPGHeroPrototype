using UnityEngine;
using System.Collections;

public class Shield : InventoryItem 
{
	private float defence;
	
	public float Defence
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
	
	public Shield(float def, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(item, id, name, image, cost, purchase)
	{
		defence = def;
	}

	public bool BlockDamage(float damage)
	{
		if(defence>=damage)
		{
			defence -= damage;
			return true;
		}
		else
		{
			return false;
		}
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Defence: " +defence + "\n";
		return result;
	}
}
