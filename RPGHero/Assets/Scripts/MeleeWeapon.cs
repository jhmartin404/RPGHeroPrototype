using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class MeleeWeapon : Weapon
{
	private int meleeCost;
	private string meleeWeaponPrefabPath;
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

	public string MeleeWeaponPrefabPath
	{
		get
		{
			return meleeWeaponPrefabPath;
		}
		set
		{
			meleeWeaponPrefabPath = value;
		}
	}

	[JsonIgnore]
	public Object MeleeWeaponPrefab
	{
		get
		{
			//return meleeWeaponPrefab;
			if(meleeWeaponPrefab == null)
			{
				meleeWeaponPrefab = Resources.Load (meleeWeaponPrefabPath);
			}
			return meleeWeaponPrefab;
		}
		set
		{
			meleeWeaponPrefab = value;
		}
	}

	public MeleeWeapon()
	{
		//meleeWeaponPrefab = Resources.Load (meleeWeaponPrefabPath);
	}

	public MeleeWeapon(int stamCost,string prefabPath, int damage, WeaponType weap, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(damage, weap, item, id, name, imagePath, cost, purchase)
	{
		meleeWeaponPrefabPath = prefabPath;
		meleeWeaponPrefab = Resources.Load (meleeWeaponPrefabPath);
		meleeCost = stamCost;
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Stamina Cost: " +meleeCost + "\n";
		return result;
	}
}
