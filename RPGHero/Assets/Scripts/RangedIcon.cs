using UnityEngine;
using System.Collections;

public class RangedIcon : Icon 
{
	private LineRenderer lineRenderer;
	private GameObject actionArea;
	private float actionAreaRadius;
	private Vector3 actionAreaCenter;
	private RangedWeapon equippedRanged;
	private Vector3 leftSide = new Vector3(-0.9f,-1.8f,0);
	private Vector3 rightSide = new Vector3 (0.9f,-1.8f,0);
	private bool startThrow;
	private float rangedStaminaCost;

	public RangedWeapon EquippedRanged
	{
		get
		{
			return equippedRanged;
		}
		set
		{
			equippedRanged = value;
		}
	}

	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		iconType = IconType.Ranged;
		lineRenderer = GameObject.Find ("LineRenderer").GetComponent<LineRenderer> ();
		equippedRanged = Player.Instance.GetPlayerInventory ().EquippedRangedWeapon;
		startThrow = false;
		actionArea = GameObject.Find ("ActionArea");
		actionAreaCenter = actionArea.GetComponent<Renderer>().bounds.center;
		actionAreaRadius = actionArea.GetComponent<CircleCollider2D>().radius;
		rangedStaminaCost = equippedRanged.GetRangedCost();
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update ();
		if (Input.touchCount > 0 && iconState != IconState.Thrown)
		{
			if( Vector2.Distance(actionAreaCenter,transform.position) < actionAreaRadius && iconState == IconState.Grabbed)
			{
				startThrow = true;
				startPosition = actionAreaCenter;
			}
		}

	}

	protected override bool OnCheckSelected()
	{
		return Player.Instance.Stamina >= rangedStaminaCost;/*equippedRanged.RangedCost;*/
	}
	
	protected override void OnIconTouched()
	{
		base.OnIconTouched ();
		actionArea.GetComponent<Renderer>().enabled = true;
	}
	
	protected override void OnIconLetGo()
	{
		base.OnIconLetGo ();
		lineRenderer.enabled = false;
		actionArea.GetComponent<Renderer>().enabled = false;
		endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		Player.Instance.Stamina -= rangedStaminaCost;//equippedRanged.RangedCost;
	}

	protected override void OnGrabbedState()
	{
		if(startThrow)
		{
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			if(Vector2.Distance(startPosition,pos)<2)
				transform.position = pos;
			if(!lineRenderer.enabled)
				lineRenderer.enabled = true;
			lineRenderer.SetPosition(0,leftSide);
			lineRenderer.SetPosition(1,transform.position);
			lineRenderer.SetPosition(2,rightSide);
			lineRenderer.SetPosition(3,transform.position);
		}
		else if(!startThrow)
		{
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			transform.position = pos;
		}
	}

	protected override void OnThrownState()
	{
		Debug.Log ("NOW THROWN");
	}

	public override void LateUpdate()
	{
		if(iconState == IconState.Thrown && startThrow)
		{
			GetComponent<Rigidbody2D>().velocity = (startPosition - endPosition).normalized*iconSpeed;
		}
		else if(iconState == IconState.Thrown && !startThrow)
		{
			OnDestroy();
		}
	}
}
