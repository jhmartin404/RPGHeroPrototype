using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour 
{
	public Transform center;
	public float degreesPerSecond = 65.0f;
	private bool isTouched = false;
	private bool isThrown = false;
	private float speed = 5.0f;
	private Vector2 startPosition;
	private Vector2 endPosition;
	
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
				if (collider2D == Physics2D.OverlapPoint(touchPos))
				{
					isTouched = true;
					startPosition = transform.position;
					rigidbody2D.isKinematic = true;
					Layout.setIconSelected(true);
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isTouched) 
			{
				
				// Get movement of the finger since last frame
				//Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				
				// Move object across XY plane
				//transform.Translate (touchDeltaPosition.x * speed, 
				//                 touchDeltaPosition.y * speed, 0);
				
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
				Destroy(gameObject);
				//v = transform.position - center.position;
			}
		}
		
		if (!isTouched && !isThrown) 
		{
			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
			transform.position = center.position + v;
		}
	}
	
	void LateUpdate()
	{
		if(isThrown)
		{
			rigidbody2D.velocity = (endPosition - startPosition).normalized*speed;
		}
	}
}
