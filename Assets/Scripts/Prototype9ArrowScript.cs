using UnityEngine;
using System.Collections;

public class Prototype9ArrowScript : MonoBehaviour {

	public Transform center;
	public float degreesPerSecond = 85.0f;
	private LineRenderer render;
	private bool isGrabbed = false;
	private bool startThrow = false;
	private bool isActive = false;
	private bool isThrown = false;
	private float speed = 10.0f;
	private Vector3 leftSide = new Vector3(-1.0f,-2.0f,0);
	private Vector3 rightSide = new Vector3 (0.4f,-2.0f,0);
	private Vector2 startPosition;
	private Vector2 endPosition;
	private float lifeTime = 4;
	private float timeElapsed;
	private bool halfLife = false;
	private float fingerRadius = 0.5f;
	
	private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		render = GameObject.Find ("LineRender").GetComponent<LineRenderer> ();
		v = transform.position - center.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0 && !isThrown)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !isGrabbed && !isActive && !Prototype9Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					//isTouched = true;
					//startPosition = transform.position;
					isGrabbed=true;
					rigidbody2D.isKinematic = true;
					Prototype9Layout.setIconSelected(true);
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Stationary && isActive && !startThrow)
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
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed && !startThrow) 
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
				Prototype9Layout.setIconSelected(false);
			}
		}
		
		if (!isActive && !isGrabbed && !isThrown) 
		{
			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
			transform.position = center.position + v;

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
//		timeElapsed += Time.deltaTime;
//		if(timeElapsed >= lifeTime/2 && !halfLife)
//		{
//			Color originalColor = renderer.material.color;
//			renderer.material.color = new Color (originalColor.r, originalColor.g, originalColor.b, 0.5f);
//			halfLife = true;
//		}
//
//		if(timeElapsed >= lifeTime)
//		{
//			Destroy(gameObject);
//		}

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
