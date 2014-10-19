﻿using UnityEngine;
using System.Collections;

public class Prototype9ShieldScript : MonoBehaviour 
{
	public Transform center;//transform icon rotates around
	public float degreesPerSecond = 85.0f;//speed of rotation
	public GameObject shield;//reference to the shield, used for rendering the sword to indicate defending
//	private GameObject enemy;//reference to the enemy, used for determining the distance the enemy is at
	private bool isStationary = false;//has the user held the icon in spot
	private bool isActive = false;//is the icon in the action area
	private bool isGrabbed = false;//is the icon grabbed by the user
//	private bool leftSwipped = false;//did the user swipe left
//	private bool rightSwipped = false;//did the user swipe right
//	private bool successfulAttack = false;//was the attack successful
	private bool isUsed = false;//was the icon used
//	private float minSwipeDistance = 0.5f;//minimum distance the user must swipe from the starting position to be considered a swipe
	private float fingerRadius = 0.5f;
	//private Vector3 startPostion;//position the icon is held at
//	private float enemyStartPosition;//the enemy's starting position
	//private float barDisplay = 0.0f;
//	private float maxAttackDamage = 15;//maximum damage the player can cause
	private Vector2 newSize = new Vector2 (1.2f, 1.2f);
	
	private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		shield = GameObject.Find ("Shield");
//		enemy = GameObject.Find ("Prototype9Orc");
//		if(enemy != null)
//			enemyStartPosition = enemy.transform.position.y; 
		v = transform.position - center.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0 && !isUsed)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !Prototype9Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					rigidbody2D.isKinematic = true;
					Prototype9Layout.setIconSelected(true);
					transform.localScale = newSize;
					shield.renderer.enabled = true;//render the shield
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed && !isStationary) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Stationary && isActive && isGrabbed && !isStationary)
			{
				isStationary = true;
				Prototype9Layout.setDefending(true);
				//startPostion = transform.position;
				
			}
			else if((Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed) || Prototype9Layout.getDefences()>=2)
			{
				if(isActive)
				{
					isUsed = true;
					
				}
//				sword.transform.rotation = Quaternion.Euler(0,0,30);//rotate sword back to initial angle
				rigidbody2D.isKinematic = false;
				Prototype9Layout.setIconSelected(false);
				Prototype9Layout.setDefending(false);
				Prototype9Layout.setDefences(0);
				//if attack was successful then calculate the damage
//				if(successfulAttack && enemy!=null)
//				{
//					//determine how close the enemy is
//					float damage = maxAttackDamage*Mathf.Abs(enemy.transform.position.y-enemyStartPosition)/4.5f;
//					Prototype9EnemyScript.TakeDamage(damage);
//				}
				shield.renderer.enabled = false;//disable the renderer for the shield
				Destroy(gameObject);//destroy the sword icon
			}
		}
		
		if (!isUsed && !isGrabbed && !isStationary) 
		{
			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
			transform.position = center.position + v;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		//Color collisionColor = new Color (0, 0, 0, 255);
		if(other.gameObject.tag == "ActionArea")
		{
			isActive = true;
		}
	}
	
	public bool getIsUsed()
	{
		return isUsed;
	}
}
