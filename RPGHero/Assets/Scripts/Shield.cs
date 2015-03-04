using UnityEngine;
using System.Collections;

public class Shield : InventoryItem 
{
	private float defence;
	private Object shieldPrefab;
	
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

	public Object ShieldPrefab
	{
		get
		{
			return shieldPrefab;
		}
		set
		{
			shieldPrefab = value;
		}
	}
	
	public Shield(float def, Object prefab, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(item, id, name, image, cost, purchase)
	{
		defence = def;
		shieldPrefab = prefab;
	}

	public bool BlockDamage(float damage)
	{
		if(defence >= damage)
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
