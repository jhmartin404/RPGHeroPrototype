using UnityEngine;
using System.Collections;

public class StandingEnemy : Enemy 
{
	protected override void OnMove()
	{
		movement.y = 0;
		moveTimer -= Time.deltaTime;
	}
}
