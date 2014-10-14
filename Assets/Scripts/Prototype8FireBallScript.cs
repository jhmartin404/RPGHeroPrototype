using UnityEngine;
using System.Collections;

public class Prototype8FireBallScript : MonoBehaviour 
{
	private bool isGrabbed;
	private bool isTouched = false;
	private bool isThrown = false;
	private float speed = 5.0f;
	private Vector2 movement;
	private float xDirection = 1.0f;
	private Vector2 startPosition;
	private Vector2 endPosition;
	private float fingerRadius = 0.5f;
	
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
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !isTouched && !Prototype8Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					startPosition = transform.position;
					rigidbody2D.isKinematic = true;
					Prototype8Layout.setIconSelected(true);
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed)
			{
				if(!isTouched)
					Destroy(gameObject);
				if(isTouched)
				{
					isThrown = true;
					isTouched = false;
				}
				endPosition = transform.position;
				rigidbody2D.isKinematic = false;
				Prototype8Layout.setIconSelected(false);
			}
		}
		
		if (!isTouched && !isThrown && !isGrabbed) 
		{
			movement.x = xDirection * speed * Time.deltaTime;
			transform.Translate(movement);
		}
		
		if(Camera.main.WorldToViewportPoint (transform.position).x > 2.0 || 
		   Camera.main.WorldToViewportPoint (transform.position).x < -1.5)
		{
			Destroy(gameObject);
		}
	}
	
	void LateUpdate()
	{
		if(isThrown)
		{
			rigidbody2D.velocity = (endPosition - startPosition).normalized*speed;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Color collisionColor = new Color (0, 0, 0, 255);
		if(other.gameObject.tag == "ActionArea")
		{
			isTouched=true;
			other.renderer.material.color = collisionColor;
		}
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
