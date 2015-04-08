using UnityEngine;
using System.Collections;

public class SpiderBoss : Boss 
{
	private Object spiderShot;
	public float spiderShotSpeed = 50;
	public override void Start ()
	{
		base.Start ();
		spiderShot = Resources.Load ("Prefabs/SpiderBossShotPrefab");
	}
	protected override void OnSpecialAttack()
	{
		AudioSource.PlayClipAtPoint (GetSoundEffect ((int)EnemySoundEffect.EnemySpecialAttack), transform.position);
		Vector3 shotPosition = transform.position;
		shotPosition.z = -2.0f;
		GameObject shot = Instantiate (spiderShot, shotPosition, transform.rotation) as GameObject;
		Rigidbody2D shotRigidBody = shot.GetComponent<Rigidbody2D> ();
		if(shotRigidBody != null)
		{
			shotRigidBody.AddForce(Vector2.up * -spiderShotSpeed);
		}
		specialAttackTimer = 0.0f;
		fsm.PopState ();
	}
}
