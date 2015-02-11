using UnityEngine;
using System.Collections;

public class Magic : InventoryItem 
{
	private int manaCost;
	private int damage;

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

	public Magic(int costMana, int dam, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(item, id, name, image, cost, purchase)
	{
		manaCost = costMana;
		damage = dam;
	}

	public virtual void DealDamage(GameObject enemy)
	{
		Enemy damagedEnemy = enemy.GetComponent<Enemy>();
		
		if(damagedEnemy != null)
		{
			enemy.GetComponent<Enemy>().EnemyHealth -= damage;
		}
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Mana Cost: " +manaCost + "\n";
		return result;
	}
}
