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
		if(!soundSource.isPlaying)
		{
			PlayEnemySoundEffect ((int)EnemySoundEffect.EnemyStand);
		}
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
		moveTimer -= Time.deltaTime;
	}
	protected override void OnStand()
	{
		if(!soundSource.isPlaying)
		{
			PlayEnemySoundEffect ((int)EnemySoundEffect.EnemyStand);
		}
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
		standTimer -= Time.deltaTime;
		if(standTimer <= 0)
		{
			standTimer = Random.Range(1,3);
			moveTimer = Random.Range(2,5);
			xDirection = Random.Range(0,1)*2-1;
			fsm.PopState();
		}
	}
}