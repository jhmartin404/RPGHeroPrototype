using UnityEngine;
using System.Collections;

public class FlyingEnemy : Enemy 
{
	protected override void OnMove()
	{
		movement.y = 0;
		//movement.x = xDirection * enemySpeed * Time.deltaTime;
		
		//if (Camera.main.WorldToViewportPoint (this.transform.position).x < 0.15) 
		//{
		//	xDirection = 1;
		//}
		//else if(Camera.main.WorldToViewportPoint (this.transform.position).x > 0.85)
		//{
		//	xDirection = -1;
		//}
	}
}