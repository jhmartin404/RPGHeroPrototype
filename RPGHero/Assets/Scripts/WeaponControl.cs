using UnityEngine;
using System.Collections;

public enum ControlState
{
	Active,
	Grabbed,
	Stationary
};

public class WeaponControl : MonoBehaviour 
{
	private GameObject weapon;
	private GameObject actionArea;
	private MeleeWeapon meleeWeapon;
	private ControlState controlState;
	private Vector2 controlPosition;//Original position of the control
	private Vector3 actionAreaCenter;
	private float actionAreaRadius;
	private float fingerRadius = 0.5f;
	private float staminaRegen = 2.0f;

	public ControlState CntrlState
	{
		get
		{
			return controlState;
		}
		set
		{
			controlState = value;
		}
	}

	// Use this for initialization
	void Start () 
	{
		controlState = ControlState.Stationary;
		actionArea = GameObject.Find ("ActionArea");
		weapon = GameObject.Find ("Weapon");
		meleeWeapon = Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon;
		weapon.GetComponent<SpriteRenderer> ().sprite = meleeWeapon.GetItemImage ();
		controlPosition = transform.position;
		actionAreaCenter = actionArea.renderer.bounds.center;
		//actionAreaCenter.y = actionAreaCenter.y + 1;//Move center up a bit
		actionAreaRadius = actionArea.GetComponent<CircleCollider2D>().radius;//Collider is used to get the radius the collider is not actually used though

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0)
		{
			if((Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved)) && controlState == ControlState.Stationary 
			   && Player.Instance.Stamina>meleeWeapon.MeleeCost && !GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected)
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					controlState = ControlState.Grabbed;		
					//rigidbody2D.isKinematic = true;
					//transform.localScale = newSize;
					//weapon.renderer.enabled = true;//render the weapon
					actionArea.renderer.enabled = true;
					GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected = true;
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && controlState == ControlState.Grabbed) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved &&controlState == ControlState.Active)
			{
				float degrees = 10;
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
				float angle = (Mathf.Atan2(transform.position.y, transform.position.x) - Mathf.Atan2(actionAreaCenter.y, actionAreaCenter.x)) * Mathf.Rad2Deg;
				Vector2 delta = Input.GetTouch(0).deltaPosition;
				if(delta.x >0)
				{
					weapon.transform.rotation = Quaternion.Euler(0,0,Mathf.Clamp(degrees+weapon.transform.eulerAngles.z,0,90));
				}
				else if(delta.x<0)
				{
					weapon.transform.rotation = Quaternion.Euler(0,0,Mathf.Clamp(weapon.transform.eulerAngles.z - degrees,0,90));
				}
			}			
			else if((Input.GetTouch(0).phase == TouchPhase.Ended && controlState != ControlState.Stationary) || Player.Instance.Stamina<=0)
			{
				controlState = ControlState.Stationary;
				//weapon.transform.rotation = Quaternion.Euler(0,0,30);//rotate sword back to initial angle
				transform.position = controlPosition;
				//transform.localScale = new Vector2(1.0f,1.0f);//set weapon icon back to normal size
				weapon.renderer.enabled = false;//disable the renderer for the weapon
				actionArea.renderer.enabled = false;
				GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected = false;
				//rigidbody2D.isKinematic = false;
				//weapon.GetComponent<PolygonCollider2D>().isTrigger = false;//Remove trigger to disable collisions
			}
		}
		
		if(controlState == ControlState.Active)
		{
			Player.Instance.Stamina -= meleeWeapon.MeleeCost*Time.deltaTime;
		}

		if(controlState == ControlState.Stationary && Player.Instance.Stamina<Player.Instance.GetPlayerStats().MaxStamina)
		{
			Player.Instance.Stamina += staminaRegen*Time.deltaTime;
		}
		
		if(Vector2.Distance(actionAreaCenter,transform.position) < actionAreaRadius && controlState == ControlState.Grabbed)
		{
			controlState = ControlState.Active;
			weapon.renderer.enabled = true;//render the weapon
			weapon.transform.position = actionAreaCenter;
		}
	}
}
