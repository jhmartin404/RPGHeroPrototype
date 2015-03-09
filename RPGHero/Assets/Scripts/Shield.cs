﻿using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class Shield : InventoryItem 
{
	private float defence;
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
		shieldPrefabPath = prefabPath;
		shieldPrefab = Resources.Load(shieldPrefabPath);
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
