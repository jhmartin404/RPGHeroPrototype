using UnityEngine;
using System.Collections;

public class Prototype2EnemyScript : MonoBehaviour {

	private float speed = 5.0f;
	private float xDirection = 1.0f;
	private float yDirection = 0.0f;
	private float enemyYPostion;
	private float attackDistance = 4.5f;
	private float sizeChangeSpeed = 1.0f;
	
	private Vector2 movement;
	
	private static float health = 100;
	
	private bool onFire = false;
	private bool isAttacking = false;
	private int fireDamage = 0;
	private float fireTime = 0.0f;
	private float attackTimer = 0.0f;
	private Vector3 size = new Vector3(1,1,0);
	
	// Use this for initialization
	void Start () 
	{
		rigidbody2D.isKinematic = true;
		enemyYPostion = transform.position.y;
	}
	
	public static int getHealth()
	{
		return (int)(health<0 ? 0:health);
	}
	
	// Update is called once per frame
	void Update () 
	{
		attackTimer += Time.deltaTime;
		//Move left to right
		if(!isAttacking)
		{
			movement.y = 0;
			movement.x = xDirection * speed * Time.deltaTime;
		}
		//Move up down
		else if(isAttacking)
		{
			movement.x = 0;
			movement.y = yDirection * speed * Time.deltaTime;
			transform.localScale += size*sizeChangeSpeed*Time.deltaTime;
			if(transform.position.y <= enemyYPostion-attackDistance && sizeChangeSpeed >0)
			{
				yDirection = 1.0f;
				sizeChangeSpeed = -1.0f;
				Prototype2Layout.AttackPlayer(10);//remove 10 health from player
			}
			
			if(yDirection>= 0.0f && transform.position.y >= enemyYPostion && sizeChangeSpeed <0)
			{
				isAttacking = false;
				sizeChangeSpeed = 1.0f;
				attackTimer = 0;
			}
		}
		if (Camera.main.WorldToViewportPoint (this.transform.position).x < 0.15) 
		{
			xDirection = 1;
		}
		else if(Camera.main.WorldToViewportPoint (this.transform.position).x > 0.85)
		{
			xDirection = -1;
		}
		
		//if on fire cause damage over time
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
		//if dead - destroy gameobject
		if(health<=0)
		{
			Destroy(gameObject);
		}
		
		if(attackTimer > 5.0 && health>0 && !isAttacking)
		{
			isAttacking = true;
			yDirection = -1.0f;
		}
		
		transform.Translate (movement);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "FireBall")
		{
			Prototype2FireBallScript script = other.gameObject.GetComponent<Prototype2FireBallScript>();
			if(script.getIsThrown() || script.getIsTouched())
			{
				onFire = true;
				fireDamage++;
				Destroy(other.gameObject);
				if(health<=0)
				{
					Destroy(gameObject);
				}
			}
		}
		
		else if(other.gameObject.tag == "Arrow")
		{
			Prototype2ArrowScript script = other.gameObject.GetComponent<Prototype2ArrowScript>();
			if(script.getIsThrown())
			{
				health -= 30;
				Destroy(other.gameObject);
				if(health<=0)
				{
					Destroy(gameObject);
				}
			}
		}
		else if(other.gameObject.tag == "Sword")
		{
			Prototype2SwordScript script = other.gameObject.GetComponent<Prototype2SwordScript>();
			if(script.getIsThrown() || script.getIsTouched())
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
}
