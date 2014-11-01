using UnityEngine;
using System.Collections;

public class Prototype10ArrowScript : MonoBehaviour {

	public Transform center; //icon rotates around this
	public float degreesPerSecond = 85.0f; //how fast icon rotates
	private Vector3 actionAreaCenter;
	private float actionAreaRadius;
	private LineRenderer render;//Renderer used to render the bow strings
	private bool isGrabbed = false;//has the user selected the icon
	private bool startThrow = false;//has the user started to pull back the arrow
	private bool isActive = false;//is the icon in the action area
	private bool isThrown = false;//did the user let go of the arrow
	private float speed = 10.0f;//speed of the arrow
	//Used by LineRenderer to render the bow strings
	private Vector3 leftSide = new Vector3(-0.9f,-1.8f,0);
	private Vector3 rightSide = new Vector3 (0.9f,-1.8f,0);
	private Vector2 startPosition;//start position of the arrow pull
	private Vector2 endPosition;//ending position
//	private float lifeTime = 5;
//	private float timeElapsed;
//	private bool halfLife = false;
	private float fingerRadius = 0.5f;//fingerRadius to determine a touch
	private Vector2 newSize = new Vector2 (1.2f, 1.2f);//new size to expand icon to when user selects the icon
	
	private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		render = GameObject.Find ("LineRender").GetComponent<LineRenderer> ();
		v = transform.position - center.position;
		actionAreaCenter = GameObject.Find ("ActionArea").renderer.bounds.center;
		actionAreaCenter.y = actionAreaCenter.y + 1;//Move center up a bit
		actionAreaRadius = GameObject.Find ("ActionArea").GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0 && !isThrown)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !isGrabbed && !isActive && !Prototype10Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					//isTouched = true;
					//startPosition = transform.position;
					isGrabbed=true;
					rigidbody2D.isKinematic = true;
					Prototype10Layout.setIconSelected(true);
					transform.localScale = newSize;
				}
				
				
			}
//			else if(Input.GetTouch(0).phase == TouchPhase.Stationary && isActive && !startThrow)
//			{
//				startThrow = true;
//				startPosition = transform.position;
//			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isActive && startThrow) 
			{
				//isGrabbed=false;
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if(Vector2.Distance(startPosition,pos)<2)
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
				Prototype10Layout.setIconSelected(false);
			}
		}
		
		if (!isActive && !isGrabbed && !isThrown) 
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
//			if(timeElapsed >= lifeTime)
//			{
//				Destroy(gameObject);
//			}
		}

		//Color collisionColor = new Color (0, 0, 0, 255);
		if( Vector3.Distance(actionAreaCenter,transform.position) > actionAreaRadius && !isActive && isGrabbed)
		{
			isActive = true;
			startThrow = true;
			startPosition = actionAreaCenter;
		}
	}

//	void OnTriggerStay2D(Collider2D other)
//	{
//		//Color collisionColor = new Color (0, 0, 0, 255);
//		if(other.gameObject.tag == "ActionArea" && !isActive && isGrabbed)
//		{
//			isActive = true;
//			//Debug.Log("HERE NOW!!!!");
//			//other.renderer.material.color = collisionColor;
//		}
//	}
	
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
