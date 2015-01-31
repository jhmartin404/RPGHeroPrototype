using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon
{
	private int meleeCost;

	public int MeleeCost
	{
		get
		{
			return meleeCost;
		}
		set
		{
			meleeCost = value;
		}
	}

	public MeleeWeapon(int stamCost, int damage, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(damage, item, id, name, image, cost, purchase)
	{
		meleeCost = stamCost;
	}
}
