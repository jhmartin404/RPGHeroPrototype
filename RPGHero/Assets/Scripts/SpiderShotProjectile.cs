using UnityEngine;
using System.Collections;

public class SpiderShotProjectile : Projectile 
{
	public float effectDuration;
	private IconSpawner iconSpawner;
	private Vector3 spiderBossWebSpawnPoint = new Vector3(0.0f,-2.5f,-2);
	private Object spiderBossWebPrefab;

	public override void Start () 
	{
		base.Start ();
		iconSpawner = GameObject.Find ("IconSpawner").GetComponent<IconSpawner>();
		spiderBossWebPrefab = Resources.Load ("Prefabs/SpiderBossWebPrefab");
	}

	public override void OnPlayerHitWithProjectile()
	{
		StartCoroutine (SpiderSpecialAttack ());
	}

	IEnumerator SpiderSpecialAttack()
	{
		if(iconSpawner.GetState()== IconSpawnerState.Running)
			iconSpawner.SetState (IconSpawnerState.Paused);
		GameObject web = Instantiate (spiderBossWebPrefab, spiderBossWebSpawnPoint, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds (effectDuration);
		if(iconSpawner.GetState() != IconSpawnerState.Stopped)
			iconSpawner.SetState (IconSpawnerState.Running);
		Destroy (web);
		OnDestroy ();
	}

}
