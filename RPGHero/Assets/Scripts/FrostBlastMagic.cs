using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

public class FrostBlastMagic : Magic 
{
	[JsonMember]
	private float frostDuration;
	[JsonMember]
	private float speedDecreaseRate;

	public FrostBlastMagic()
	{

	}

	public FrostBlastMagic(float duration, float decreaseRate, int costMana, ItemType item, int id, string name, string imagePath, int cost, bool purchase) : base(costMana,item, id, name, imagePath, cost, purchase)
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
