using UnityEngine;
using System.Collections;

public class FrostBlastMagic : Magic 
{
	private float frostDuration;
	private float speedDecreaseRate;
	
	public FrostBlastMagic(float duration, float decreaseRate, int costMana, ItemType item, int id, string name, Sprite image, int cost, bool purchase) : base(costMana,item, id, name, image, cost, purchase)
	{
		frostDuration = duration;
		speedDecreaseRate = decreaseRate;
	}
	
	public override void DealDamage(Enemy enemy)
	{
		enemy.SlowDown (speedDecreaseRate, frostDuration);
	}

	public override string ToString()
	{
		string result = base.ToString ();
		result += "Frost Duration: " +frostDuration + "\n";
		result += "Speed Decrease Rate: " +speedDecreaseRate + "\n";
		return result;
	}
}
