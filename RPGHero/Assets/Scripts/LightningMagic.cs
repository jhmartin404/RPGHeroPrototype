﻿using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class LightningMagic : Magic 
{
	[JsonMember]
	private float lightningDamage;

	public LightningMagic()
	{

	}

	public LightningMagic(float dam, int costMana, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(costMana,item, id, name, imagePath, cost, purchase)
	{
		lightningDamage = dam;
	}
	
	public override void DealDamage(Enemy enemy)
	{
		enemy.TakeDamage(lightningDamage);
	}
	
	public override string ToString()
	{
		string result = base.ToString ();
		result += "Damage: " +lightningDamage + "\n";
		return result;
	}
}
