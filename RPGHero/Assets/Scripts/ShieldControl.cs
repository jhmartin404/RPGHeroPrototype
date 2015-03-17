﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShieldControl : MonoBehaviour 
{
	private GameObject shield;
	private Shield equippedShield;
	private ControlState controlState;
	private Vector3 controlPosition;//Original position of the control
	private Vector3 actionAreaCenter;
	private float actionAreaRadius;
	private float fingerRadius = 0.5f;
	private GameObject shieldDefence;
	private Text shieldDefenceText;
	private bool shieldTrigger;

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
		shieldTrigger = shield.GetComponent<PolygonCollider2D> ().isTrigger;
		equippedShield = Player.Instance.GetPlayerInventory ().EquippedShield;
		//shield.GetComponent<SpriteRenderer> ().sprite = equippedShield.GetItemImage ();
		controlPosition = transform.position;
		Object shieldDefencePrefab = Resources.Load ("Prefabs/ShieldDefenceText");
		shieldDefence = Instantiate (shieldDefencePrefab, transform.position, transform.rotation) as GameObject;
		shieldDefence.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		shieldDefence.transform.position = transform.position;
		shieldDefenceText = shieldDefence.GetComponent<Text> ();
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
				if (GetComponent<Collider2D>() == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					controlState = ControlState.Active;
					shield.GetComponent<Renderer>().enabled = true;//render the shield
					//shield.GetComponent<PolygonCollider2D>().isTrigger = true;
					shieldTrigger = true;
					Vector3 pos = transform.position;
					pos.z += 0.5f;
					shield.transform.position = pos;
					GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected = true;
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Moved && controlState == ControlState.Active) 
			{
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				pos.z = -1;
				transform.position = pos;
				Vector3 shieldPos = pos;
				shieldPos.z += 0.5f;
				shield.transform.position = shieldPos;
			}
			else if((Input.GetTouch(0).phase == TouchPhase.Ended && controlState == ControlState.Active))
			{
				//Insure that the Player is not defending when they drop the shield
				if(Player.Instance.IsDefending)
				{
					Player.Instance.IsDefending = false;
				}
				controlState = ControlState.Stationary;
				shield.GetComponent<Renderer>().enabled = false;//disable the renderer for the shield
				//shield.GetComponent<PolygonCollider2D>().isTrigger = false;
				shieldTrigger = false;
				transform.position = controlPosition;
				Vector3 shieldPos = controlPosition;
				shieldPos.z += 0.5f;
				shield.transform.position = shieldPos;
				GameObject.Find("Main Camera").GetComponent<LevelScript>().IconSelected = false;
			}
		}
		
		Vector3 newPosition = shield.transform.position;
		newPosition.x = transform.position.x;
		shield.transform.position = newPosition;
		shieldDefence.transform.position = transform.position;
		shieldDefenceText.text = "" + equippedShield.Defence;
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag ("Enemy") && shieldTrigger)
		{
			Player.Instance.IsDefending = true;
		}
		else if(other.CompareTag("Projectile") && shieldTrigger)
		{
			Player.Instance.IsDefending = true;
			other.GetComponent<Projectile>().OnPlayerBlockedProjectile();
		}
	}

	public virtual void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag ("Enemy"))
		{
			Player.Instance.IsDefending = false;
		}
	}
}
