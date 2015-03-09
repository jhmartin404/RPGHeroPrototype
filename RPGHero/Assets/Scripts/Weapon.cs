using UnityEngine;
using System.Collections;

public enum WeaponType
{
	Melee,
	Ranged
};

public class Weapon : InventoryItem 
{
	private int damage;
	private WeaponType weaponType;

	public int Damage
	{
		get
		{
			return damage;
		}
		set
		{
			damage = value;
		}
	}

	public WeaponType WpnType
	{
		get
		{
			return weaponType;
		}
		set
		{
			weaponType = value;
		}
	}

	public Weapon()
	{

	}

	public Weapon(int dam,WeaponType weap, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(item, id, name, imagePath, cost, purchase)
	{
		damage = dam;
		weaponType = weap;
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Damage: " +damage + "\n";
		return result;
	}

	public virtual void DealDamage(Enemy enemy)
	{
		enemy.TakeDamage (damage);
		//enemy.EnemyHealth -= damage;
	}
}
