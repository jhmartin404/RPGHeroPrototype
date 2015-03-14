using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	protected Vector3 playerPosition;

	// Use this for initialization
	void Start () 
	{
		playerPosition = GameObject.Find ("ActionArea").transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnBecameInvisible()
	{
		if(transform.position.y < playerPosition.y && transform.position.x <= playerPosition.x + Camera.main.orthographicSize  && transform.position.x >= playerPosition.x - Camera.main.orthographicSize)
		{
			OnPlayerHitWithProjectile();
		}
		else
		{
			OnDestroy ();
		}
	}

	public virtual void OnPlayerHitWithProjectile()
	{
		Destroy (gameObject);
	}

	public virtual void OnDestroy()
	{
		Destroy (gameObject);
	}
}
