using UnityEngine;
using System.Collections;

public class FireBlastMagic : Magic 
{
	private float fireDuration;
	private float fireDamage;

	public FireBlastMagic(float duration, float dam, int costMana, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(costMana,item, id, name, imagePath, cost, purchase)
	{
		fireDuration = duration;
		fireDamage = dam;
	}

	public override void DealDamage(Enemy enemy)
	{
		enemy.TakeDamageOverTime (fireDamage, fireDuration);
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Fire Duration: " +fireDuration + "\n";
		result += "Damage: " +fireDamage + "\n";
		return result;
	}
}
