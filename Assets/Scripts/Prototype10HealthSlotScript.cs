using UnityEngine;
using System.Collections;

public class Prototype10HealthSlotScript : MonoBehaviour 
{
	private Color coolDownColor = new Color(0,255,0,255);
	private Color normalColor;
	private bool healthUsed;
//	private float coolDownTime = 2.0f;
	private Vector3 size = new Vector3(1,1,0);
	private Vector3 maxSize = new Vector3(1.05f,1.05f,0);
	private Vector3 minSize;
	private float sizeChangeSpeed = 0.2f;
	private int healthAmount = 10;
	private float fingerRadius = 0.5f;//Radius of finger
	// Use this for initialization
	void Start () 
	{
		healthUsed = false;
		normalColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetTouch(0).phase == TouchPhase.Began  && !healthUsed || (Input.GetTouch(0).phase == TouchPhase.Moved && !healthUsed && !Prototype10Layout.getIconSelected()))
		{
			Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
			{
				healthUsed = true;
				Prototype10Layout.increaseHealth(healthAmount);
				renderer.material.color = coolDownColor;
				Invoke("CoolDown",3);
			}			
		}

//		if(healthUsed)
//		{
//			//Grow then shrink coinbag when a coin is collected
//			coolDownTime -= Time.deltaTime;
//			if(coolDownTime<=0)
//			{
//				coolDownTime = 2.0f;
//				healthUsed = false;
//			}
//		}
	}

	void CoolDown ()
	{
		healthUsed = false;
		renderer.material.color = normalColor;
	}
	
//	void OnTriggerEnter2D(Collider2D other)
//	{
//		if (other.gameObject.tag == "Health")
//		{
//			//Add to coin count, set coinCollected to true and destroy the coin
//			Prototype10Layout.increaseHealth(healthAmount);
//			healthCollected = true;
//			Destroy(other.gameObject);
//		}
//	}
}
