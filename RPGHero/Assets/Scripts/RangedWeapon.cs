using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon 
{
	private int rangedCost;
	
	public int RangedCost
	{
		get
		{
			return rangedCost;
		}
		set
		{
			rangedCost = value;
		}
	}
	
	public RangedWeapon(int stamCost, int damage, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(damage, item, id, name, image, cost, purchase)
	{
		rangedCost = stamCost;
	}
}
