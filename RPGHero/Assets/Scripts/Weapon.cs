using UnityEngine;
using System.Collections;

public class Weapon : InventoryItem 
{
	private int damage;

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

	public Weapon(int dam,ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(item, id, name, image, cost, purchase)
	{
		damage = dam;
	}
}
