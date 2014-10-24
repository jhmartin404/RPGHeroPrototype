﻿using UnityEngine;
using System.Collections;

public class Prototype10EnemyScript : MonoBehaviour 
{
	public GameObject attackScreen;//Screen to display when the enemy attacks the player (flashes a red screen)
	private float speed = 5.0f;//Speed enemy moves at
	private float xDirection = 1.0f;//Determines whether to move right or left
	private float yDirection = 0.0f;//Determines whether to move up or down
	private float enemyYPostion;//Enemy's initial position on y axis
	private float attackDistance = 4.5f;//Distance enemy will move to attack
	private float sizeChangeSpeed = 1.0f;

	private Vector2 movement;//Enemy's movement

	private static float health = 100;//Enemy's health

	private bool onFire = false;//determine if enemy is on fire
	private bool isAttacking = false;//determine if the enemy is attacking the player
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
				StartCoroutine(AttackedPlayer());//Flash red screen
				Prototype10Layout.AttackPlayer(10);//remove 10 health from player
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

	//Flash enemy's color, used to show when enemy is hit
	IEnumerator Flash(Color collideColor, Color normalColor)
	{
		renderer.material.color = collideColor;
		yield return new WaitForSeconds(0.1f);
		renderer.material.color = normalColor;
	}

	//Flash red screen, used to show that the player has been hit by the enemy
	IEnumerator AttackedPlayer()
	{
		Vector3 tempvect = new Vector3(transform.position.x, transform.position.y, 0);
		attackScreen.transform.position = tempvect;
		yield return new WaitForSeconds(0.1f);
		tempvect.Set(transform.position.x, transform.position.y, 10);
		attackScreen.transform.position = tempvect;
	}

	//used for the swords to attack the enemy
	public static void TakeDamage(float damage)
	{
		//Color normalColor = renderer.material.color;
		//Color collideColor = new Color (255, 0, 0, 255);
		health -= damage;
		//if(health<=0)
		//{
		//	Destroy(this.gameObject);
		//}
	}

	void OnGUI()
	{
		int buttonWidth = (int)(Screen.width * 0.3);
		int buttonHeight = (int)(Screen.height * 0.18);
		int healthWidth = (int)(Screen.width * 0.3);
		int healthHeight = (int)(Screen.height * 0.08);
		
		GUI.skin.label.fontSize = Screen.width / 20;
		
		Rect healthRect = new Rect (
			transform.position.x,
			transform.position.y,
			healthWidth,
			healthHeight);
		
		GUI.Label (healthRect, ""+getHealth());
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Color normalColor = renderer.material.color;
		Color collideColor = new Color (255, 0, 0, 255);
		//if hit with fireball
		if (other.gameObject.tag == "FireBall")
		{
			Prototype10FireBallScript script = other.gameObject.GetComponent<Prototype10FireBallScript>();
			if(script.getIsThrown() || script.getIsActive())
			{
				onFire = true;
				fireDamage++;
				StartCoroutine(Flash(collideColor,normalColor));
				Destroy(other.gameObject);
				if(health<=0)
				{
					Destroy(gameObject);
				}
			}
		}
		//if hit with an arrow
		else if(other.gameObject.tag == "Arrow")
		{
			Prototype10ArrowScript script = other.gameObject.GetComponent<Prototype10ArrowScript>();
			if(script.getIsThrown())
			{
				health -= 30;
				StartCoroutine(Flash(collideColor,normalColor));
				Destroy(other.gameObject);
				if(health<=0)
				{
					Destroy(gameObject);
				}
			}
		}
//		else if(other.gameObject.tag == "Sword")
//		{
//			Prototype9SwordScript script = other.gameObject.GetComponent<Prototype9SwordScript>();
//			if(script.getIsThrown() || script.getIsTouched())
//			{
//				health -= 20;
//				StartCoroutine(Flash(collideColor,normalColor));
//				Destroy(other.gameObject);
//				if(health<=0)
//				{
//					Destroy(gameObject);
//				}
//			}
//		}
	}
}
