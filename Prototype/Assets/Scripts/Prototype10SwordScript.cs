using UnityEngine;
using System.Collections;

public class Prototype10SwordScript : MonoBehaviour 
{
	//public Transform center;//transform icon rotates around
	//public float degreesPerSecond = 85.0f;//speed of rotation
	public Sprite swordFullAttacks;
	public Sprite swordThreeAttacks;
	public Sprite swordTwoAttacks;
	public Sprite swordOneAttack;
	public Sprite swordNoAttacks;
	private GameObject sword;//reference to the sword, used for rendering the sword and rotating it
	private float attackTime = 2.0f;
	//private GameObject[] enemies;//reference to the enemy, used for determining the distance the enemy is at
	//private bool isStationary = false;//has the user held the icon in spot, to initiate attack
	private Vector3 actionAreaCenter;
	private float actionAreaRadius;
	private bool isActive = false;//is the icon in the action area
	private bool isGrabbed = false;//is the icon grabbed by the user
	//private bool leftSwipped = false;//did the user swipe left
	//private bool rightSwipped = false;//did the user swipe right
	//private bool successfulAttack = false;//was the attack successful
	//private bool isUsed = false;//was the icon used
	//private float minSwipeDistance = 0.5f;//minimum distance the user must swipe from the starting position to be considered a swipe
	private float fingerRadius = 0.5f;
	private Vector3 startPosition;//position the icon is held at
	//private float[] enemyStartPosition;//the enemy's starting position
	//private float barDisplay = 0.0f;
	//private float maxAttackDamage = 15;//maximum damage the player can cause
	private Vector2 newSize = new Vector2 (1.2f, 1.2f);
	private int attacksAvailable;//Available uses left for the sword
	private Vector2 position;//Original position of the icon
	private float cooldownTime;//Time it takes to regain an attack
	
	//private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		sword = GameObject.Find ("Sword");
		//enemies = GameObject.FindGameObjectsWithTag("Enemy");
		attacksAvailable = 4;
		cooldownTime = 5.0f;
		position = transform.position;
		//enemyStartPosition = new float[enemies.Length];
		//Debug.Log (enemies.Length);
		//for(int i = 0;i<enemies.Length;i++)
		//{
		//	if(enemies[i] != null)
		//		enemyStartPosition[i] = enemies[i].transform.position.y; 
			//enemyCount++;
		//}
		//v = transform.position - center.position;

		actionAreaCenter = GameObject.Find ("ActionArea").renderer.bounds.center;
		actionAreaCenter.y = actionAreaCenter.y + 1;//Move center up a bit
		actionAreaRadius = GameObject.Find ("ActionArea").GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.touchCount > 0)
		{
			if((Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved && !Prototype10Layout.getIconSelected()))  && attacksAvailable>0)
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					isGrabbed = true;		
					rigidbody2D.isKinematic = true;
					Prototype10Layout.setIconSelected(true);
					transform.localScale = newSize;
					sword.renderer.enabled = true;//render the sword
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed && !isActive) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && isGrabbed && isActive)
			{
				float degrees = 10;
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
				float angle = (Mathf.Atan2(transform.position.y, transform.position.x) - Mathf.Atan2(actionAreaCenter.y, actionAreaCenter.x)) * Mathf.Rad2Deg;
				Vector2 delta = Input.GetTouch(0).deltaPosition;
				if(delta.x >0)
				{
					//Quaternion rotate = Quaternion.AngleAxis(delta.x*degrees,Vector3.forward);
					sword.transform.rotation = Quaternion.Euler(0,0,Mathf.Clamp(degrees+sword.transform.eulerAngles.z,0,90));
				}
				else if(delta.x<0)
				{
					sword.transform.rotation = Quaternion.Euler(0,0,Mathf.Clamp(sword.transform.eulerAngles.z - degrees,0,90));
				}
			}			
//			else if(((Input.GetTouch(0).phase == TouchPhase.Moved && isStationary) || (Input.GetTouch(0).phase == TouchPhase.Stationary && isStationary)) && !successfulAttack)
//			{
//				Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//				if(touchPos.x < startPosition.x - minSwipeDistance)
//				{
//					sword.transform.rotation = Quaternion.Euler(0,0,90);//rotate the sword
//					leftSwipped = true;
////					if(rightSwipped)
////						barDisplay = 1.0f;
////					else
////						barDisplay = 0.5f;
//				}
//				else if(touchPos.x > startPosition.x + minSwipeDistance)
//				{
//					sword.transform.rotation = Quaternion.Euler(0,0,-30);//rotate the sword
//					rightSwipped = true;
////					if(leftSwipped)
////						barDisplay = 1.0f;
////					else
////						barDisplay = 0.5f;
//				}
//
//				if(leftSwipped && rightSwipped)
//					successfulAttack = true;
//			}
//			else if(Input.GetTouch(0).phase == TouchPhase.Stationary && isActive && isGrabbed && !isStationary && !successfulAttack)
//			{
//				isStationary = true;
//				startPosition = transform.position;
//				
//			}
			else if((Input.GetTouch(0).phase == TouchPhase.Ended && isGrabbed) || attackTime<=0)
			{
				if(isActive)
				{
					isActive = false;
				}
				isGrabbed = false;
				attacksAvailable--;
				sword.transform.rotation = Quaternion.Euler(0,0,30);//rotate sword back to initial angle
				transform.position = position;
				transform.localScale = new Vector2(1.0f,1.0f);//set sword icon back to normal size
				sword.renderer.enabled = false;//disable the renderer for the sword
				rigidbody2D.isKinematic = false;
				Prototype10Layout.setIconSelected(false);
				attackTime = 2.0f;
				cooldownTime = 5.0f;
				//if attack was successful then calculate the damage
//				if(enemies.Length>0)
//				{
//					for(int i = 0;i<enemies.Length;i++)
//					{
//						if(enemies[i] != null)
//						{
//							//determine how close the enemy is
//							float damage = maxAttackDamage*Mathf.Abs(enemies[i].transform.position.y-enemyStartPosition[i])/4.5f;
//							enemies[i].GetComponent<Prototype10EnemyScript>().TakeDamage(damage);
//						}
//					}
//				}
				sword.GetComponent<PolygonCollider2D>().isTrigger = false;//Remove trigger to disable collisions
				//Destroy(gameObject);//destroy the sword icon
			}
		}
		
//		if (!isUsed && !isGrabbed && !isStationary) 
//		{
//			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
//			transform.position = center.position + v;
//		}
		if(isActive)
		{
			attackTime-=Time.deltaTime;
		}
		if(attacksAvailable<4 && !isGrabbed && !isActive)
		{
			cooldownTime-=Time.deltaTime;
		}
		if(cooldownTime<=0)
		{
			attacksAvailable++;
			cooldownTime=5.0f;
		}

		switch(attacksAvailable)
		{
		case 4:
			GetComponent<SpriteRenderer>().sprite = swordFullAttacks;
			break;
		case 3:
			GetComponent<SpriteRenderer>().sprite = swordThreeAttacks;
			break;
		case 2:
			GetComponent<SpriteRenderer>().sprite = swordTwoAttacks;
			break;
		case 1:
			GetComponent<SpriteRenderer>().sprite = swordOneAttack;
			break;
		case 0:
			GetComponent<SpriteRenderer>().sprite = swordNoAttacks;
			break;
		}

		if(Vector3.Distance(actionAreaCenter,transform.position) > actionAreaRadius && isGrabbed && !isActive)
		{
			isActive = true;
			sword.transform.position = actionAreaCenter;
			startPosition = actionAreaCenter;	
			attackTime = 2.0f;
			sword.GetComponent<PolygonCollider2D>().isTrigger = true;
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
		if(other.gameObject.tag == "Repair")
		{
			attacksAvailable = 4;
			cooldownTime = 5.0f;
			other.GetComponent<Prototype10RepairSlotScript>().setRepairUsed(true);
			//Destroy(other.gameObject);
		}
	}
}
