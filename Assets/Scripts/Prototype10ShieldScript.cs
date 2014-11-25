using UnityEngine;
using System.Collections;

public class Prototype10ShieldScript : MonoBehaviour 
{
	//public Transform center;//transform icon rotates around
	//public float degreesPerSecond = 85.0f;//speed of rotation
	public Sprite shieldFullDefs;
	public Sprite shieldThreeDefs;
	public Sprite shieldTwoDefs;
	public Sprite shieldOneDef;
	public Sprite shieldNoDefs;
	private GameObject shield;//reference to the shield, used for rendering the sword to indicate defending
	private GameObject actionArea;
	private bool isStationary = false;//has the user held the icon in spot
	private bool isActive = false;//is the icon in the action area
	private bool isGrabbed = false;//is the icon grabbed by the user
	private static bool usingShield = false;//boolean to determine if the shield is in use
	//private bool isUsed = false;//was the icon used
	//private Vector3 startPostion;
	private float fingerRadius = 0.5f;
	private Vector2 newSize = new Vector2 (1.2f, 1.2f);
	private int defsAvailable;//Available uses left for the shield
	private Vector2 position;//Original position of the icon
	private float cooldownTime;//Time it takes to regain a shield
	
	//private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		shield = GameObject.Find ("Shield");
		actionArea = GameObject.Find ("ActionArea");
		position = transform.position;
		defsAvailable = 4;
		cooldownTime = 5.0f;
	//	v = transform.position - center.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0)
		{
			if((Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !Prototype10Layout.getIconSelected())) && defsAvailable>0)
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					rigidbody2D.isKinematic = true;
					Prototype10Layout.setIconSelected(true);
					transform.localScale = newSize;
					shield.renderer.enabled = true;//render the shield
					actionArea.renderer.enabled = false;
					usingShield = true;
					//Prototype10Layout.setDefending(true);
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
				actionArea.renderer.enabled = false;
			}
//			else if(Input.GetTouch(0).phase == TouchPhase.Stationary && isActive && isGrabbed && !isStationary)
//			{
//				isStationary = true;
//				Prototype10Layout.setDefending(true);
//				//startPostion = transform.position;
//				
//			}
			else if((Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed))
			{
				rigidbody2D.isKinematic = false;
				Prototype10Layout.setIconSelected(false);
				Prototype10Layout.setDefending(false);
				defsAvailable--;
				cooldownTime = 5.0f;
				shield.renderer.enabled = false;//disable the renderer for the shield
				transform.position = position;
				transform.localScale = new Vector2(1.0f,1.0f);
				isActive = false;
				isStationary = false;
				isGrabbed = false;
				usingShield = false;
			}
		}
		
//		if (!isUsed && !isGrabbed && !isStationary) 
//		{
//			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
//			transform.position = center.position + v;
//		}

		if(defsAvailable<4 && !isGrabbed && !isActive)
		{
			cooldownTime-=Time.deltaTime;
		}
		if(cooldownTime<=0)
		{
			defsAvailable++;
			cooldownTime=5.0f;
		}
		
		switch(defsAvailable)
		{
		case 4:
			GetComponent<SpriteRenderer>().sprite = shieldFullDefs;
			break;
		case 3:
			GetComponent<SpriteRenderer>().sprite = shieldThreeDefs;
			break;
		case 2:
			GetComponent<SpriteRenderer>().sprite = shieldTwoDefs;
			break;
		case 1:
			GetComponent<SpriteRenderer>().sprite = shieldOneDef;
			break;
		case 0:
			GetComponent<SpriteRenderer>().sprite = shieldNoDefs;
			break;
		}

		Vector3 newPosition = shield.transform.position;
		newPosition.x = transform.position.x;
		shield.transform.position = newPosition;
	}

	public static bool getUsingShield()
	{
		return usingShield;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		//Color collisionColor = new Color (0, 0, 0, 255);
		if(other.gameObject.tag == "ActionArea")
		{
			isActive = true;
		}

		if(other.gameObject.tag == "Repair")
		{
			defsAvailable = 4;
			cooldownTime = 5.0f;
			other.GetComponent<Prototype10RepairSlotScript>().setRepairUsed(true);
			//Destroy(other.gameObject);
		}
	}
}
