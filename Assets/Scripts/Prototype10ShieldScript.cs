using UnityEngine;
using System.Collections;

public class Prototype10ShieldScript : MonoBehaviour 
{
	public Transform center;//transform icon rotates around
	public float degreesPerSecond = 85.0f;//speed of rotation
	public GameObject shield;//reference to the shield, used for rendering the sword to indicate defending
	private bool isStationary = false;//has the user held the icon in spot
	private bool isActive = false;//is the icon in the action area
	private bool isGrabbed = false;//is the icon grabbed by the user
	private bool isUsed = false;//was the icon used
	private Vector3 startPostion;
	private float fingerRadius = 0.5f;
	private Vector2 newSize = new Vector2 (1.2f, 1.2f);
	
	//private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		shield = GameObject.Find ("Shield"); 
		startPostion = transform.position;
	//	v = transform.position - center.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0 && !isUsed)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !Prototype10Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					rigidbody2D.isKinematic = true;
					Prototype10Layout.setIconSelected(true);
					transform.localScale = newSize;
					shield.renderer.enabled = true;//render the shield
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed && !isStationary) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Stationary && isActive && isGrabbed && !isStationary)
			{
				isStationary = true;
				Prototype10Layout.setDefending(true);
				//startPostion = transform.position;
				
			}
			else if((Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed) || Prototype10Layout.getDefences()>=4)
			{
				if(isActive)
				{
					isUsed = true;
					
				}
				rigidbody2D.isKinematic = false;
				Prototype10Layout.setIconSelected(false);
				Prototype10Layout.setDefending(false);
				shield.renderer.enabled = false;//disable the renderer for the shield
				if(Prototype10Layout.getDefences()>=4)
				{
					Prototype10Layout.setDefences(0);
					Destroy(gameObject);//destroy the shield icon
				}
				else
				{
					transform.position = startPostion;
					isUsed = false;
					isActive = false;
					isStationary = false;
					isGrabbed = false;
				}
			}
		}
		
//		if (!isUsed && !isGrabbed && !isStationary) 
//		{
//			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
//			transform.position = center.position + v;
//		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		//Color collisionColor = new Color (0, 0, 0, 255);
		if(other.gameObject.tag == "ActionArea")
		{
			isActive = true;
		}
	}
	
	public bool getIsUsed()
	{
		return isUsed;
	}
}
