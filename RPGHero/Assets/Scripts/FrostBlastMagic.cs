using UnityEngine;
using System.Collections;

public class FrostBlastMagic : Magic 
{
	private float frostDuration;
	private float speedDecrease;
	
	public FrostBlastMagic(float duration, float decrease, int costMana, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(costMana,item, id, name, image, cost, purchase)
	{
		frostDuration = duration;
		speedDecrease = decrease;
	}
	
	public override void DealDamage(Enemy enemy)
	{
		enemy.SlowDown (speedDecrease, frostDuration);
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Frost Duration: " +frostDuration + "\n";
		result += "Speed Decrease: " +speedDecrease + "\n";
		return result;
	}
}
