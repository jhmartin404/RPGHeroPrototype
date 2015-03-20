using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class Shield : InventoryItem 
{
	private float defence;
	private float maxDefence;
	[JsonMember]
	private string shieldPrefabPath;
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

	[JsonIgnore]
	public Object ShieldPrefab
	{
		get
		{
			if(shieldPrefab == null)
			{
				shieldPrefab = Resources.Load(shieldPrefabPath);
			}
			return shieldPrefab;
		}
		set
		{
			shieldPrefab = value;
		}
	}

	public Shield()
	{

	}
	
	public Shield(float def, string prefabPath, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(item, id, name, imagePath, cost, purchase)
	{
		defence = def;
		maxDefence = defence;
		shieldPrefabPath = prefabPath;
		shieldPrefab = Resources.Load(shieldPrefabPath);
	}

	public void HealShield()
	{
		defence = maxDefence;
	}

	public float BlockDamage(float damage)
	{
		if(defence >= damage)
		{
			defence -= damage;
			return 0;
		}
		else
		{
			damage -= defence;
			defence = 0;
			return damage;
		}
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Defence: " +defence + "\n";
		return result;
	}
}
