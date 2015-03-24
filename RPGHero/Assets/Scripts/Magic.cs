using UnityEngine;
using System.Collections;

public class Magic : InventoryItem 
{
	protected float manaCost;

	public float ManaCost
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

	public Magic()
	{

	}

	public Magic(float costMana, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(item, id, name, imagePath, cost, purchase)
	{
		manaCost = costMana;
	}

	public virtual void DealDamage(Enemy enemy)
	{
		//enemy.EnemyHealth -= damage;
	}

	public float GetManaCost()
	{
		int amount = (Player.Instance.GetPlayerStats ().MagicStat - 50);
		float amountTakenOff = amount/200.0f;
		return (manaCost - amountTakenOff);
	}

	public override string ToString()
	{
		//int amount = (Player.Instance.GetPlayerStats ().MagicStat - 50);
		//float amountTakenOff = amount/200.0f;
		string result = base.ToString ();
		result += "Mana Cost: " + GetManaCost() + "\n";
		return result;
	}
}
