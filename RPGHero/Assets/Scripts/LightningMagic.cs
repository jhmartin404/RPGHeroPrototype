using UnityEngine;
using System.Collections;

public class LightningMagic : Magic 
{
	private float lightningDamage;
	public LightningMagic(float dam, int costMana, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(costMana,item, id, name, image, cost, purchase)
	{
		lightningDamage = dam;
	}
	
	public override void DealDamage(Enemy enemy)
	{
		enemy.TakeDamage(lightningDamage);
	}
	
	public override string ToString()
	{
		string result = base.ToString ();
		result += "Damage: " +lightningDamage + "\n";
		return result;
	}
}
