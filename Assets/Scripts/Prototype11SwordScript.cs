using UnityEngine;
using System.Collections;

public class Prototype11SwordScript : MonoBehaviour 
{
	public float xDirection = 1.0f;
	public GameObject sword;//reference to the sword, used for rendering the sword and rotating it

	private float speed = 4.0f;//Speed of icon
	private Vector2 movement;
	private GameObject enemy;//reference to the enemy, used for determining the distance the enemy is at
	private bool isStationary = false;//has the user held the icon in spot, to initiate attack
	private bool isActive = false;//is the icon in the action area
	private bool isGrabbed = false;//is the icon grabbed by the user
	private bool leftSwipped = false;//did the user swipe left
	private bool rightSwipped = false;//did the user swipe right
	private bool successfulAttack = false;//was the attack successful
	private bool isUsed = false;//was the icon used
	private float minSwipeDistance = 0.5f;//minimum distance the user must swipe from the starting position to be considered a swipe
	private float fingerRadius = 0.5f;
	private Vector3 startPostion;//position the icon is held at
	private float enemyStartPosition;//the enemy's starting position
	//private float barDisplay = 0.0f;
	private float maxAttackDamage = 15;//maximum damage the player can cause
	private Vector2 newSize = new Vector2 (1.2f, 1.2f);
	
	private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		sword = GameObject.Find ("Sword");
		enemy = GameObject.Find ("Prototype11Orc");
		if(enemy != null)
			enemyStartPosition = enemy.transform.position.y; 
		rigidbody2D.isKinematic = true;
		movement.y = 0;
		//v = transform.position - center.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0 && !isUsed)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !Prototype11Layout.getIconSelected()))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					rigidbody2D.isKinematic = true;
					Prototype11Layout.setIconSelected(true);
					transform.localScale = newSize;
					sword.renderer.enabled = true;//render the sword
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed && !isStationary) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(((Input.GetTouch(0).phase == TouchPhase.Moved && isStationary) || (Input.GetTouch(0).phase == TouchPhase.Stationary && isStationary)) && !successfulAttack)
			{
				Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if(touchPos.x < startPostion.x - minSwipeDistance)
				{
					sword.transform.rotation = Quaternion.Euler(0,0,90);//rotate the sword
					leftSwipped = true;
//					if(rightSwipped)
//						barDisplay = 1.0f;
//					else
//						barDisplay = 0.5f;
				}
				else if(touchPos.x > startPostion.x + minSwipeDistance)
				{
					sword.transform.rotation = Quaternion.Euler(0,0,-30);//rotate the sword
					rightSwipped = true;
//					if(leftSwipped)
//						barDisplay = 1.0f;
//					else
//						barDisplay = 0.5f;
				}

				if(leftSwipped && rightSwipped)
					successfulAttack = true;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Stationary && isActive && isGrabbed && !isStationary && !successfulAttack)
			{
				isStationary = true;
				startPostion = transform.position;
				
			}
			else if((Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed) || successfulAttack)
			{
				if(isActive)
				{
					isUsed = true;

				}
				sword.transform.rotation = Quaternion.Euler(0,0,30);//rotate sword back to initial angle
				rigidbody2D.isKinematic = false;
				Prototype11Layout.setIconSelected(false);
				//if attack was successful then calculate the damage
				if(successfulAttack && enemy!=null)
				{
					//determine how close the enemy is
					float damage = maxAttackDamage*Mathf.Abs(enemy.transform.position.y-enemyStartPosition)/4.5f;
					Prototype11EnemyScript.TakeDamage(damage);
				}
				sword.renderer.enabled = false;//disable the renderer for the sword
				Destroy(gameObject);//destroy the sword icon
			}
		}
		
		if (!isUsed && !isGrabbed && !isStationary) 
		{
			movement.x = xDirection * speed * Time.deltaTime;
			transform.Translate(movement);
		}
	}

//	void OnGUI()
//	{
//		Vector3 pos = new Vector3(350,850,0);
//		Vector3 size = new Vector3 (350, 100, 0);
//		if(isStationary)
//		{
//			// draw the background:
//			GUI.BeginGroup (new Rect (pos.x, Screen.height - pos.y, size.x, size.y));
//			GUI.Box (new Rect (0,0, size.x, size.y),"EMPTY");
//			
//			// draw the filled-in part:
//			GUI.BeginGroup (new Rect (0, 0, size.x * barDisplay, size.y));
//			GUI.Box (new Rect (0,0, size.x, size.y),"FULL");
//			GUI.EndGroup ();
//			
//			GUI.EndGroup ();
//		}
//	}

	void OnTriggerStay2D(Collider2D other)
	{
		//Color collisionColor = new Color (0, 0, 0, 255);
		if(other.gameObject.tag == "ActionArea" && isGrabbed && !isActive)
		{
			isActive = true;
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
