using UnityEngine;
using System.Collections;

public class FireBlastMagic : Magic 
{
	private float fireDuration;

	public FireBlastMagic(float duration, int costMana, int dam, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(costMana,dam,item, id, name, image, cost, purchase)
	{
		fireDuration = duration;
	}

	public override void DealDamage(Enemy enemy)
	{
		enemy.EnemyHealth -= damage;
	}
}
