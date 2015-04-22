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
	public LevelScript levelScript;
	private GameObject weapon;
	private GameObject actionArea;
	private MeleeWeapon meleeWeapon;
	private ControlState controlState;
	private Vector3 controlPosition;//Original position of the control
	private Vector3 actionAreaCenter;
	private float actionAreaRadius;
	private float fingerRadius = 0.5f;
	private float staminaRegen = 2.0f;
	private float meleeStaminaCost;
	private GameObject mainCamera;
	private TrailRenderer weaponTrailRenderer;
	private Renderer controlRenderer;

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

	public MeleeWeapon Weapon
	{
		get
		{
			return meleeWeapon;
		}
		set
		{
			meleeWeapon = value;
		}
	}

	// Use this for initialization
	void Start () 
	{
		controlState = ControlState.Stationary;
		controlRenderer = GetComponent<Renderer> ();
		actionArea = GameObject.Find ("ActionArea");
		weapon = GameObject.Find ("Weapon");
		meleeWeapon = Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon;
		controlPosition = transform.position;
		actionAreaCenter = actionArea.GetComponent<Renderer>().bounds.center;
		actionAreaRadius = actionArea.GetComponent<CircleCollider2D>().radius;//Collider is used to get the radius the collider is not actually used though
		meleeStaminaCost = meleeWeapon.GetMeleeCost();
		mainCamera = GameObject.Find ("Main Camera");
		weaponTrailRenderer = weapon.GetComponentInChildren<TrailRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!levelScript.PlayerWon && !levelScript.PlayerLost)
		{
			if(!OnCheckSelected())
			{
				if(controlState != ControlState.Stationary)
				{
					OnControlLetGo();
					controlState = ControlState.Stationary;
				}
				controlRenderer.material.SetColor("_Color",Color.gray);
			}
			else if(OnCheckSelected())
			{
				if(controlRenderer.material.GetColor("_Color") ==  Color.gray)
				{
					controlRenderer.material.SetColor("_Color",Color.white);
				}
			}
			if (Input.touchCount > 0)
			{
				if((Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved)) && controlState == ControlState.Stationary 
			   		&& OnCheckSelected() && !mainCamera.GetComponent<LevelScript>().IconSelected)
				{
					Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
					if (GetComponent<Collider2D>() == Physics2D.OverlapCircle(touchPos, fingerRadius))
					{
						OnControlTouched();
					}				
				}
				else if(Input.GetTouch(0).phase == TouchPhase.Moved && controlState == ControlState.Grabbed) 
				{
					OnControlGrabbed();
				}
				else if(Input.GetTouch(0).phase == TouchPhase.Moved &&controlState == ControlState.Active)
				{
					OnControlActiveGrabbed();
				}			
				else if((Input.GetTouch(0).phase == TouchPhase.Ended && controlState != ControlState.Stationary) || Player.Instance.Stamina<=0)
				{
					OnControlLetGo();
				}
			}
		
			if(controlState == ControlState.Active)
			{
				Player.Instance.Stamina -= meleeStaminaCost * Time.deltaTime;//meleeWeapon.MeleeCost*Time.deltaTime;
			}

			if(controlState == ControlState.Stationary && Player.Instance.Stamina < Player.Instance.GetPlayerStats().MaxStamina)
			{
				Player.Instance.Stamina += staminaRegen*Time.deltaTime;
			}
		
			if(Vector2.Distance(actionAreaCenter,transform.position) < actionAreaRadius && controlState == ControlState.Grabbed)
			{
				controlState = ControlState.Active;
				weapon.GetComponent<Renderer>().enabled = true;//render the weapon
				weapon.GetComponent<PolygonCollider2D>().isTrigger = true;
				Vector3 weaponPos = actionAreaCenter;
				weaponPos.z -= 0.1f;
				weapon.transform.position = weaponPos;
			}
		}
	}

	private bool OnCheckSelected()
	{
		return Player.Instance.Stamina > meleeStaminaCost;
	}

	private void OnControlGrabbed()
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		transform.position = new Vector3(pos.x, pos.y, controlPosition.z);
	}

	private void OnControlActiveGrabbed()
	{
		float degrees = 10;
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		transform.position = new Vector3(pos.x, pos.y, controlPosition.z);
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
	
	private void OnControlTouched()
	{
		controlState = ControlState.Grabbed;
		actionArea.GetComponent<Renderer>().enabled = true;
		mainCamera.GetComponent<LevelScript>().IconSelected = true;
		if(weaponTrailRenderer != null)
			weaponTrailRenderer.enabled = true;
	}
	
	private void OnControlLetGo()
	{
		controlState = ControlState.Stationary;
		transform.position = controlPosition;
		weapon.GetComponent<Renderer>().enabled = false;//disable the renderer for the weapon
		weapon.GetComponent<PolygonCollider2D>().isTrigger = false;
		actionArea.GetComponent<Renderer>().enabled = false;
		mainCamera.GetComponent<LevelScript>().IconSelected = false;
		if(weaponTrailRenderer != null)
			weaponTrailRenderer.enabled = false;
	}
}
