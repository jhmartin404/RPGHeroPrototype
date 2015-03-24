using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class MeleeWeapon : Weapon
{
	[JsonMember]
	private float meleeCost;
	private string meleeWeaponPrefabPath;
	private Object meleeWeaponPrefab;


	public float MeleeCost
	{
		get
		{
			return meleeCost;
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

	}

	public MeleeWeapon(float stamCost,string prefabPath, int damage, WeaponType weap, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(damage, weap, item, id, name, imagePath, cost, purchase)
	{
		meleeWeaponPrefabPath = prefabPath;
		meleeWeaponPrefab = Resources.Load (meleeWeaponPrefabPath);
		meleeCost = stamCost;
	}

	public float GetMeleeCost()
	{
		int amount = (Player.Instance.GetPlayerStats ().MeleeStat - 50);
		float amountTakenOff = amount/200.0f;
		return (meleeCost - amountTakenOff);
	}

	public override string ToString()
	{
		//int amount = (Player.Instance.GetPlayerStats ().MeleeStat - 50);
		//float amountTakenOff = amount/200.0f;
		string result = base.ToString ();
		result += "Stamina Cost: " + GetMeleeCost() + "\n";
		return result;
	}
}
