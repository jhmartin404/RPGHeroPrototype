using UnityEngine;
using System.Collections;

public class SpiderBoss : Boss 
{
	private Object spiderShot;
	private float spiderShotSpeed=50;
	public override void Start ()
	{
		base.Start ();
		spiderShot = Resources.Load ("Prefabs/SpiderBossShotPrefab") as Object;
	}
	protected override void OnSpecialAttack()
	{
		GameObject shot = Instantiate (spiderShot, transform.position, transform.rotation) as GameObject;
		Rigidbody2D shotRigidBody = shot.GetComponent<Rigidbody2D> ();
		if(shotRigidBody != null)
		{
			shotRigidBody.AddForce(Vector2.up * -spiderShotSpeed);
		}
		specialAttackTimer = 0.0f;
		fsm.PopState ();
	}
}
