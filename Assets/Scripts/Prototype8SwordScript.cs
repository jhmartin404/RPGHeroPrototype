using UnityEngine;
using System.Collections;

public class Prototype8SwordScript : MonoBehaviour 
{
	public GameObject sword;
	private bool isStationary = false;
	private bool isActive = false;
	private bool isGrabbed = false;
	private bool leftSwipped = false;
	private bool rightSwipped = false;
	private bool successfulAttack = false;
	private bool isUsed = false;
	private float speed = 5.0f;
	private Vector2 movement;
	private float xDirection = 1.0f;
	private float fingerRadius = 0.5f;
	private Vector3 startPostion;
	private float barDisplay = 0.0f;
	//private Vector3 rotation = new Vector3 (0, 0, Time.deltaTime);
	
	private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		rigidbody2D.isKinematic = true;
		movement.y = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0 && !isUsed)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					rigidbody2D.isKinematic = true;
					Layout.setIconSelected(true);
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed && !isStationary) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if((Input.GetTouch(0).phase == TouchPhase.Moved && isStationary) || (Input.GetTouch(0).phase == TouchPhase.Stationary && isStationary))
			{
				Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if(touchPos.x < startPostion.x)
				{
					sword.transform.rotation = Quaternion.Euler(0,0,90);
					leftSwipped = true;
					if(rightSwipped)
						barDisplay = 1.0f;
					else
						barDisplay = 0.5f;
				}
				else if(touchPos.x > startPostion.x)
				{
					sword.transform.rotation = Quaternion.Euler(0,0,-30);
					rightSwipped = true;
					if(leftSwipped)
						barDisplay = 1.0f;
					else
						barDisplay = 0.5f;
				}
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Stationary && isActive && isGrabbed)
			{
				isStationary = true;
				startPostion = transform.position;

			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed)
			{
				if(isActive)
				{
					isUsed = true;
					if(leftSwipped && rightSwipped)
						successfulAttack = true;
				}
				sword.transform.rotation = Quaternion.Euler(0,0,30);
				rigidbody2D.isKinematic = false;
				Layout.setIconSelected(false);
				if(successfulAttack)
				{
					Prototype8EnemyScript.TakeDamage(15);
				}
				Destroy(gameObject);
			}
		}
		
		if (!isUsed && !isGrabbed && !isStationary) 
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

	void OnGUI()
	{
		Vector3 pos = new Vector3(350,750,0);
		Vector3 size = new Vector3 (350, 100, 0);
		if(isStationary)
		{
			// draw the background:
			GUI.BeginGroup (new Rect (pos.x, Screen.height - pos.y, size.x, size.y));
				GUI.Box (new Rect (0,0, size.x, size.y),"EMPTY");
			
				// draw the filled-in part:
				GUI.BeginGroup (new Rect (0, 0, size.x * barDisplay, size.y));
					GUI.Box (new Rect (0,0, size.x, size.y),"FULL");
				GUI.EndGroup ();
			
			GUI.EndGroup ();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Color collisionColor = new Color (0, 0, 0, 255);
		if(other.gameObject.tag == "ActionArea")
		{
			isActive = true;
			other.renderer.material.color = collisionColor;
		}
	}

	public bool SuccessfulAttack()
	{
		return successfulAttack;
	}
	
	public bool getIsUsed()
	{
		return isUsed;
	}
}
