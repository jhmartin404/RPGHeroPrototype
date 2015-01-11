using UnityEngine;
using System.Collections;

public class Prototype11FireBallScript : MonoBehaviour 
{
	public float xDirection = 1.0f;

	private Vector2 movement;
	private bool isGrabbed = false;//has the user selected the icon
	private bool isActive = false;//is the icon in the action area
	private bool isThrown = false;//did the user throw the icon
	private float speed = 4.0f;//speed of icon
	private float fireBallSpeed = 5.0f;//Speed of fireball
	private Vector2 startPosition;//start position of the fireball
	private Vector2 endPosition;//end position of the fireball
	private float fingerRadius = 0.5f;//Radius of finger
	private Vector2 newSize = new Vector2 (1.2f, 1.2f);//new size to expand icon to when user selects the icon

	private Vector3 v;

	// Use this for initialization
	void Start () 
	{
		//v = transform.position - center.position;
		rigidbody2D.isKinematic = true;
		movement.y = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.touchCount > 0 && !isThrown)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !isActive && !Prototype11Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					//startPosition = transform.position;
					rigidbody2D.isKinematic = true;
					Prototype11Layout.setIconSelected(true);
					transform.localScale = newSize;
				}


			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed) 
			{
				startPosition = transform.position;
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed)
			{
				if(!isActive)
					Destroy(gameObject);
				if(isActive)
				{
					isThrown = true;
					isActive = false;
				}
				endPosition = transform.position;
				rigidbody2D.isKinematic = false;
				Prototype11Layout.setIconSelected(false);
			}
		}

		if (!isActive && !isThrown && !isGrabbed) 
		{
			movement.x = xDirection * speed * Time.deltaTime;
			transform.Translate(movement);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		//Color collisionColor = new Color (0, 0, 0, 255);
		if(other.gameObject.tag == "ActionArea" && isGrabbed && !isActive)
		{
			isActive=true;
		}
	}

	void LateUpdate()
	{
		if(isThrown)
		{
			rigidbody2D.velocity = (endPosition - startPosition).normalized*fireBallSpeed;
		}
	}

	public bool getIsThrown()
	{
		return isThrown;
	}

	public bool getIsActive()
	{
		return isActive;
	}
}
