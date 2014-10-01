using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	/// <summary>
	/// Object speed
	/// </summary>
	public Vector2 speed = new Vector2(250, 250);
	
	/// <summary>
	/// Moving direction
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);

	private Vector2 movement;

	private static float health = 100;

	private bool onFire = false;
	private int fireDamage = 0;
	private float fireTime = 0.0f;

	// Use this for initialization
	void Start () 
	{

	}

	public static int getHealth()
	{
		return (int)(health<0 ? 0:health);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Camera.main.WorldToViewportPoint (this.transform.position).x < 0) 
		{
			direction.x = 1;
		}
		else if(Camera.main.WorldToViewportPoint (this.transform.position).x > 1)
		{
			direction.x = -1;
		}
			// 2 - Movement
			movement = new Vector2 (
				speed.x * direction.x * Time.deltaTime,
				speed.y * direction.y * Time.deltaTime);
		if(onFire)
		{
			health-= fireDamage*Time.deltaTime;
			fireTime += Time.deltaTime;
			if(fireTime>5.0f)
			{
				onFire = false;
				fireDamage = 0;
				fireTime = 0.0f;
			}
		}
		if(health<=0)
		{
			Destroy(gameObject);
		}
	}

	void FixedUpdate()
	{
		// Apply movement to the rigidbody
		rigidbody2D.velocity = movement;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "FireBall")
		{
			onFire = true;
			fireDamage++;
			Destroy(other.gameObject);
			if(health<=0)
			{
				Destroy(gameObject);
			}
		}
		else if(other.gameObject.tag == "Coin")
		{

		}

		else if(other.gameObject.tag == "Arrow")
		{
			health -= 30;
			Destroy(other.gameObject);
			if(health<=0)
			{
				Destroy(gameObject);
			}
		}
		else if(other.gameObject.tag == "Sword")
		{
			health -= 20;
			Destroy(other.gameObject);
			if(health<=0)
			{
				Destroy(gameObject);
			}
		}
	}
}
