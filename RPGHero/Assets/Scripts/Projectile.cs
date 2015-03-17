using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	protected Vector3 playerPosition;
	private Camera mainCamera;

	// Use this for initialization
	public virtual void Start () 
	{
		playerPosition = GameObject.Find ("ActionArea").transform.position;
		mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
	
	}

	void OnBecameInvisible()
	{
		if(mainCamera != null)
		{
			if(transform.position.y < playerPosition.y && transform.position.x <= playerPosition.x + mainCamera.orthographicSize  && transform.position.x >= playerPosition.x - mainCamera.orthographicSize)
			{
				OnPlayerHitWithProjectile();
			}
			else
			{
				OnDestroy ();
			}
		}
	}

	public virtual void OnPlayerHitWithProjectile()
	{
		OnDestroy ();
	}

	public virtual void OnPlayerBlockedProjectile()
	{
		OnDestroy ();
	}

	public virtual void OnDestroy()
	{
		Destroy (gameObject);
	}
}
