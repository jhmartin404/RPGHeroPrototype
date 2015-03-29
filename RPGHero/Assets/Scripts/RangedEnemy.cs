using UnityEngine;
using System.Collections;

public class RangedEnemy : Enemy 
{

	private Object rangedShot;
	public string rangedShotPrefab;
	public float rangedShotSpeed = 80;
	public override void Start ()
	{
		base.Start ();
		rangedShot = Resources.Load (rangedShotPrefab);
	}

	protected override void OnAttack()
	{
		GameObject shot = Instantiate (rangedShot, transform.position, transform.rotation) as GameObject;
		Rigidbody2D shotRigidBody = shot.GetComponent<Rigidbody2D> ();
		if(shotRigidBody != null)
		{
			PlayEnemySoundEffect((int)EnemySoundEffect.EnemyAttack);
			shotRigidBody.AddForce(Vector2.up * -rangedShotSpeed);
		}
		attackTimer = 0;
		fsm.PopState ();
	}
}
