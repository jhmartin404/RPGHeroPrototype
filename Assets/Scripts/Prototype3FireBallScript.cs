using UnityEngine;
using System.Collections;

public class Prototype3FireBallScript : MonoBehaviour 
{

	private bool isTouched = false;
	private bool isThrown = false;
	private float speed = 3.0f;
	private Vector2 movement;
	private float xDirection = 0.0f;
	private float yDirection = 0.0f;
	private Vector2 startPosition;
	private Vector2 endPosition;
	private float fingerRadius = 0.3f;
	private int movementChoice;
	
	// Use this for initialization
	void Start () 
	{
		rigidbody2D.isKinematic = true;
		movementChoice = Random.Range (0, 3);
		xDirection = (movementChoice == 0) ? 0.0f : (movementChoice > 1) ? 1.0f : -1.0f;
		yDirection = (movementChoice == 0) ? -1.0f : 0.0f;
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
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && isTouched)
			{
				isTouched = false;
				isThrown = true;
				endPosition = transform.position;
				rigidbody2D.isKinematic = false;
				Layout.setIconSelected(false);
			}
		}
		
		if (!isTouched && !isThrown) 
		{
			movement.x = xDirection * speed * Time.deltaTime;
			movement.y = yDirection * speed * Time.deltaTime;
			transform.Translate(movement);
		}
		
		if(Camera.main.WorldToViewportPoint (transform.position).x > 1.5 || Camera.main.WorldToViewportPoint(transform.position).x < -1.0 || 
		   Camera.main.WorldToViewportPoint(transform.position).y <-0.5)
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
	
	public bool getIsThrown()
	{
		return isThrown;
	}
	
	public bool getIsTouched()
	{
		return isTouched;
	}
}
