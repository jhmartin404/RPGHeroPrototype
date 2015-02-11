using UnityEngine;
using System.Collections;

public class ShieldControl : MonoBehaviour 
{
	private GameObject shield;
	private Shield equippedShield;
	private ControlState controlState;
	private Vector2 controlPosition;//Original position of the control
	private Vector3 actionAreaCenter;
	private float actionAreaRadius;
	private float fingerRadius = 0.5f;

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
		shield = GameObject.Find ("Shield");
		equippedShield = Player.Instance.GetPlayerInventory ().EquippedShield;
		shield.GetComponent<SpriteRenderer> ().sprite = equippedShield.GetItemImage ();
		controlPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0)
		{
			if((Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved)) && controlState == ControlState.Stationary && equippedShield.Defence>0
			   && !GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected)
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					controlState = ControlState.Active;
					//transform.localScale = newSize;
					shield.renderer.enabled = true;//render the shield
					GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected = true;
					Player.Instance.IsDefending = true;
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && controlState == ControlState.Active) 
			{
				Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				transform.position = pos;
				//actionArea.renderer.enabled = false;
			}
			else if((Input.GetTouch(0).phase == TouchPhase.Ended && controlState == ControlState.Active))
			{
				controlState = ControlState.Stationary;
				shield.renderer.enabled = false;//disable the renderer for the shield
				transform.position = controlPosition;
				GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected = false;
				Player.Instance.IsDefending = false;
				//transform.localScale = new Vector2(1.0f,1.0f);
			}
		}
		
		Vector3 newPosition = shield.transform.position;
		newPosition.x = transform.position.x;
		shield.transform.position = newPosition;
	}
}
