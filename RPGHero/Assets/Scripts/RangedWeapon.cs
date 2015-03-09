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

	public RangedWeapon()
	{

	}
	
	public RangedWeapon(int stamCost, int damage, WeaponType weap, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(damage, weap, item, id, name, imagePath, cost, purchase)
	{
		rangedCost = stamCost;
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Stamina Cost: " +rangedCost + "\n";
		return result;
	}
}
