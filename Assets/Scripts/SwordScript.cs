﻿using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour 
{
	public Transform center;
	public float degreesPerSecond = 65.0f;
	private bool isTouched = false;
	private bool isThrown = false;
	//private float speed = 5.0f;
	//private Vector2 startPosition;
	//private Vector2 endPosition;
	private float lifeTime = 10;
	private float timeElapsed;
	private bool halfLife = false;
	private float fingerRadius = 0.3f;
	
	private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		v = transform.position - center.position;
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
					//startPosition = transform.position;
					rigidbody2D.isKinematic = true;
					Layout.setIconSelected(true);
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isTouched) 
			{
				
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && isTouched)
			{
				isTouched = false;
				isThrown = true;
				//endPosition = transform.position;
				rigidbody2D.isKinematic = false;
				Layout.setIconSelected(false);
				Destroy(gameObject);
			}
		}
		
		if (!isTouched && !isThrown) 
		{
			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
			transform.position = center.position + v;
		}

		timeElapsed += Time.deltaTime;
		if(timeElapsed >= lifeTime/2 && !halfLife)
		{
			Color originalColor = renderer.material.color;
			renderer.material.color = new Color (originalColor.r, originalColor.g, originalColor.b, 0.5f);
			halfLife = true;
		}
		
		if(timeElapsed >= lifeTime)
		{
			Destroy(gameObject);
		}
	}
	
	void LateUpdate()
	{
		//if(isThrown)
		//{
		//	rigidbody2D.velocity = (endPosition - startPosition).normalized*speed;
		//}
	}

	public bool getIsThrown()
	{
		return isThrown;
	}

	public bool getIsTouched()
	{
		return isTouched;
	}
}
