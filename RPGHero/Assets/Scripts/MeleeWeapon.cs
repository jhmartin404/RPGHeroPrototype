using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon
{
	private int meleeCost;
	private Object meleeWeaponPrefab;

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

	public Object MeleeWeaponPrefab
	{
		get
		{
			return meleeWeaponPrefab;
		}
		set
		{
			meleeWeaponPrefab = value;
		}
	}

	public MeleeWeapon(int stamCost,Object prefab, int damage, WeaponType weap, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(damage, weap, item, id, name, image, cost, purchase)
	{
		meleeWeaponPrefab = prefab;
		meleeCost = stamCost;
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Stamina Cost: " +meleeCost + "\n";
		return result;
	}
}
