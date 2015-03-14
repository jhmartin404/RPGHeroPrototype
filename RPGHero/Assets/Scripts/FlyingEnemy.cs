using UnityEngine;
using System.Collections;

public class FlyingEnemy : Enemy 
{
	private float flyingAmount = 0.5f;
	private Vector3 upPosition;
	private Vector3 downPosition;
	private bool flyUp;
	public override void Start ()
	{
		base.Start ();
		upPosition = transform.position;
		upPosition.y += flyingAmount;
		downPosition = transform.position;
		downPosition.y -= flyingAmount;
	}

	protected override void OnMove()
	{

		float step = enemySpeed/2 * Time.deltaTime;
		if(flyUp)
		{
			transform.position = Vector2.MoveTowards (transform.position, upPosition, step);
		}
		else
		{
			transform.position = Vector2.MoveTowards (transform.position, downPosition, step);
		}
		
		if(Vector2.Distance(transform.position,upPosition)<step && flyUp)
		{
			flyUp = false;
		}
		else if(Vector2.Distance(transform.position,downPosition)<step && !flyUp)
		{
			flyUp = true;
		}
	}
}