using UnityEngine;
using System.Collections;

public class ArrowProjectile : Projectile 
{
	public float damage;

	public override void OnPlayerHitWithProjectile()
	{
		Player.Instance.TakeDamageAmount (damage);
		base.OnPlayerHitWithProjectile ();
	}
	
	public override void OnPlayerBlockedProjectile()
	{
		Player.Instance.TakeDamageAmount (damage);
		base.OnPlayerBlockedProjectile ();
	}


}
