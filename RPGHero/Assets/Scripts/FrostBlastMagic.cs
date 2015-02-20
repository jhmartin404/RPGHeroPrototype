using UnityEngine;
using System.Collections;

public class FrostBlastMagic : Magic 
{
	private float frostDuration;
	
	public FrostBlastMagic(float duration, int costMana, int dam, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(costMana,dam,item, id, name, image, cost, purchase)
	{
		frostDuration = duration;
	}
	
	public override void DealDamage(Enemy enemy)
	{
		enemy.EnemyHealth -= damage;
	}
}
