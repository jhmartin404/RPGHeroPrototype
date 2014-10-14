using UnityEngine;
using System.Collections;

public class Prototype8ArrowScript : MonoBehaviour 
{
	public GameObject lineRenderer;
	private LineRenderer render;
	private bool isGrabbed = false;
	private bool startThrow = false;
	private bool isActive = false;
	private bool isThrown = false;
	private float speed = 5.0f;
	private float arrowSpeed = 10.0f;
	private float xDirection = 1.0f;
	private Vector3 leftSide = new Vector3(-0.8f,-2.5f,0);
	private Vector3 rightSide = new Vector3 (0.6f,-2.5f,0);
	private Vector2 startPosition;
	private Vector2 endPosition;
	private float fingerRadius = 0.5f;
	
	private Vector2 movement;
	
	// Use this for initialization
	void Start () 
	{
		render = lineRenderer.GetComponent<LineRenderer>();
		rigidbody2D.isKinematic = true;
		movement.y = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0 && !isThrown)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !isGrabbed && !isActive && !Prototype8Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					//isTouched = true;
					//startPosition = transform.position;
					isGrabbed=true;
					rigidbody2D.isKinematic = true;
					Prototype8Layout.setIconSelected(true);
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Stationary && isActive)
			{
				startThrow = true;
				startPosition = transform.position;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isActive && startThrow) 
			{
				//isGrabbed=false;
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if(Vector2.Distance(startPosition,pos)<1)
					transform.position = pos;
				if(!render.enabled)
					render.enabled = true;
				render.SetPosition(0,leftSide);
				render.SetPosition(1,transform.position);
				render.SetPosition(2,rightSide);
				render.SetPosition(3,transform.position);
				//Gizmos.DrawLine(leftSide,transform.position);
				//Gizmos.DrawLine(rightSide,transform.position);
				//UnityEditor.Handles.DrawLine(leftSide,transform.position);
				//UnityEditor.Handles.DrawLine(rightSide,transform.position);
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}

			else if(Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed)
			{
				render.enabled = false;
				if(!isActive)
					Destroy(gameObject);
				if(isActive)
				{
					isThrown = true;
					isActive = false;
					isGrabbed = false;
				}
				endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				rigidbody2D.isKinematic = false;
				Prototype8Layout.setIconSelected(false);
			}
		}
		
		if (!isActive && !isThrown && !isGrabbed) 
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

	//void onDrawGizmos()
	//{
	//	if(startThrow)
	//	{
	//		Gizmos.color = Color.blue;
	//		Gizmos.DrawLine(leftSide,transform.position);
	//		Gizmos.DrawLine(rightSide,transform.position);
	//	}
	//}
	
	void LateUpdate()
	{
		if(isThrown)
		{
			rigidbody2D.velocity = (startPosition - endPosition).normalized*arrowSpeed;
			//Debug.Log ("RIGHT HERE");
			//Debug.Log(rigidbody2D.velocity);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Color collisionColor = new Color (0, 0, 0, 255);
		if(other.gameObject.tag == "ActionArea" && !isActive && isGrabbed)
		{
			//Debug.Log("HERE");
			isActive = true;
			//isGrabbed = false;
			//startPosition = transform.position;
			other.renderer.material.color = collisionColor;
		}
	}
	
	public bool getIsThrown()
	{
		return isThrown;
	}
}
