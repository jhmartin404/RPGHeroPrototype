﻿using UnityEngine;
using System.Collections;

public class Prototype2ArrowScript : MonoBehaviour 
{
	private bool isTouched = false;
	private bool isThrown = false;
	private float speed = 3.0f;
	private float xDirection = 1.0f;
	private Vector2 startPosition;
	private Vector2 endPosition;
	private float fingerRadius = 0.3f;
	
	private Vector2 movement;
	
	// Use this for initialization
	void Start () 
	{
		rigidbody2D.isKinematic = true;
		movement.y = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0 && !isThrown)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !isTouched && !Layout.getIconSelected()))
			{
				Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Vector2 touchPos = new Vector2(wp.x, wp.y);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isTouched = true;
					startPosition = transform.position;
					rigidbody2D.isKinematic = true;
					Layout.setIconSelected(true);
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isTouched) 
			{				
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if(Vector2.Distance(startPosition,pos)<1)
					transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && isTouched)
			{
				isTouched = false;
				isThrown = true;
				endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				rigidbody2D.isKinematic = false;
				Layout.setIconSelected(false);
			}
		}
		
		if (!isTouched && !isThrown) 
		{
			movement.x = xDirection * speed * Time.deltaTime;
			transform.Translate(movement);
		}
		
		if(Camera.main.WorldToViewportPoint (transform.position).x > 1.5)
		{
			Destroy(gameObject);
		}
		
	}
	
	void LateUpdate()
	{
		if(isThrown)
		{
			rigidbody2D.velocity = (startPosition - endPosition).normalized*speed;
		}
	}
	
	public bool getIsThrown()
	{
		return isThrown;
	}
}
