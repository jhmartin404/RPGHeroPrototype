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
			Debug.Log("Flying Up");
			transform.position = Vector2.MoveTowards (transform.position, upPosition, step);
			//transform.position = Vector3.SmoothDamp(transform.position, actionAreaCenter, ref velocity, smoothTime);
		}
		else
		{
			Debug.Log("Flying Down");
			transform.position = Vector2.MoveTowards (transform.position, downPosition, step);
			//transform.position = Vector3.SmoothDamp(transform.position, enemyPosition, ref velocity, smoothTime);
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