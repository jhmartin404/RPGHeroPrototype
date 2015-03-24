using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon 
{
	private float rangedCost;
	
	public float RangedCost
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

	public float GetRangedCost()
	{
		int amount = (Player.Instance.GetPlayerStats ().MeleeStat - 50);
		float amountTakenOff = amount/200.0f;
		return (rangedCost - amountTakenOff);
	}

	public override string ToString()
	{
		//int amount = (Player.Instance.GetPlayerStats ().MeleeStat - 50);
		//float amountTakenOff = amount/200.0f;
		string result = base.ToString ();
		result += "Stamina Cost: " +GetRangedCost() + "\n";
		return result;
	}
}
