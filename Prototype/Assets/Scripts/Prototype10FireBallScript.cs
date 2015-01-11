using UnityEngine;
using System.Collections;

public class Prototype10FireBallScript : MonoBehaviour 
{

	public Transform center;//icon rotates around this
	public float degreesPerSecond = 85.0f;//speed at which the icon rotates
	private bool isGrabbed = false;//has the user selected the icon
	private bool isActive = false;//is the icon in the action area
	private bool isThrown = false;//did the user throw the icon
	private float speed = 5.0f;//speed of fireball
	private Vector2 startPosition;//start position of the fireball
	private Vector2 endPosition;//end position of the fireball
//	private float lifeTime = 5;
//	private float timeElapsed;
//	private bool halfLife = false;
	private float fingerRadius = 0.5f;//Radius of finger
	private Vector2 newSize = new Vector2 (1.2f, 1.2f);//new size to expand icon to when user selects the icon

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
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !isActive && !Prototype10Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					//startPosition = transform.position;
					rigidbody2D.isKinematic = true;
					Prototype10Layout.setIconSelected(true);
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
				Prototype10Layout.setIconSelected(false);
			}
		}

		if (!isActive && !isThrown && !isGrabbed) 
		{
			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
			transform.position = center.position + v;

//			timeElapsed += Time.deltaTime;
//			if(timeElapsed >= lifeTime/2 && !halfLife)
//			{
//				Color originalColor = renderer.material.color;
//				renderer.material.color = new Color (originalColor.r, originalColor.g, originalColor.b, 0.5f);
//				halfLife = true;
//			}
//			
//			if(timeElapsed >= lifeTime && (!isTouched || !isThrown))
//			{
//				Destroy(gameObject);
//			}
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
			rigidbody2D.velocity = (endPosition - startPosition).normalized*speed;
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
