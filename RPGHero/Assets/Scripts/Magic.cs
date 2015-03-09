using UnityEngine;
using System.Collections;

public class Magic : InventoryItem 
{
	protected int manaCost;

	public int ManaCost
	{
		get
		{
			return manaCost;
		}
		set
		{
			manaCost = value;
		}
	}

	public Magic(int costMana, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(item, id, name, imagePath, cost, purchase)
	{
		manaCost = costMana;
	}

	public virtual void DealDamage(Enemy enemy)
	{
		//enemy.EnemyHealth -= damage;
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Mana Cost: " +manaCost + "\n";
		return result;
	}
}
