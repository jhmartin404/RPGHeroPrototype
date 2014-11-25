using UnityEngine;
using System.Collections;

public class Prototype10RepairSlotScript : MonoBehaviour 
{
	private Color coolDownColor = new Color(0,255,0,255);
	private Color normalColor;
	private bool isGrabbed = false;//has the user selected the icon
//	private bool isActive = false;//is the icon in the action area
	private bool repairUsed;
//	private bool isThrown = false;//did the user throw the icon
	private float speed = 5.0f;//speed of health icon
	private Vector2 slotPosition;//where the repair icon is located
//	private Vector2 endPosition;//end position of the health icon
	private float fingerRadius = 0.5f;//Radius of finger
	private Vector2 newSize = new Vector2 (1.2f, 1.2f);//new size to expand icon to when user selects the icon
	
	private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		repairUsed = false;
		slotPosition = transform.position;
		normalColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0 && !repairUsed)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !isGrabbed && !Prototype10Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;	
//					isActive = true;
					//startPosition = transform.position;
//					rigidbody2D.isKinematic = true;
					Prototype10Layout.setIconSelected(true);
					transform.localScale = newSize;
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed) 
			{
//				startPosition = transform.position;
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed)
			{
//				if(!isActive)
//					Destroy(gameObject);
//				if(isActive)
//				{
////					isThrown = true;
//					isActive = false;
//				}
//				endPosition = transform.position;
//				rigidbody2D.isKinematic = false;
				isGrabbed=false;
				transform.localScale = new Vector2(1.0f,1.0f);
				Prototype10Layout.setIconSelected(false);
				if(!repairUsed)
				{
					repairUsed = true;
					transform.position = slotPosition;
					Invoke("CoolDown",3);
					renderer.material.color = coolDownColor;
				}
			}
		}
	}

	public void setRepairUsed(bool rep)
	{
		isGrabbed=false;
		transform.localScale = new Vector2(1.0f,1.0f);
		//Prototype10Layout.setIconSelected(false);
		repairUsed = rep;
		transform.position = slotPosition;
		Invoke ("CoolDown", 3);
		renderer.material.color = coolDownColor;
	}

	void CoolDown ()
	{
		repairUsed = false;
		renderer.material.color = normalColor;
	}

//	void OnTriggerEnter2D(Collider2D other)
//	{		
//		if(other.gameObject.tag == "Sword")
//		{
//			repairUsed = true;
//			transform.position = slotPosition;
//			transform.localScale = new Vector2(1.0f,1.0f);
//			isGrabbed=false;
//			Prototype10Layout.setIconSelected(false);
//			Invoke("CoolDown",3);
//		}
//
//		if(other.gameObject.tag == "Shield")
//		{
//			repairUsed = true;
//			transform.position = slotPosition;
//			transform.localScale = new Vector2(1.0f,1.0f);
//			isGrabbed=false;
//			Prototype10Layout.setIconSelected(false);
//			Invoke("CoolDown",3);
//		}
//	}

//	void LateUpdate()
//	{
//		if(isThrown)
//		{
//			rigidbody2D.velocity = (endPosition - startPosition).normalized*speed;
//		}
//	}
	
//	public bool getIsThrown()
//	{
//		return isThrown;
//	}
	
//	public bool getIsActive()
//	{
//		return isActive;
//	}
}
