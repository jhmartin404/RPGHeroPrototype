using UnityEngine;
using System.Collections;

public class RangedIcon : Icon 
{
	private LineRenderer lineRenderer;
	private GameObject actionArea;
	private float actionAreaRadius;
	private Vector3 actionAreaCenter;
	private RangedWeapon equippedRanged;
	private Vector3 leftSide = new Vector3(-1.5f,-1.8f,0);
	private Vector3 rightSide = new Vector3 (1.5f,-1.8f,0);
	private bool startThrow;
	private float rangedStaminaCost;
	private AudioClip rangedIconSound;
	private TrailRenderer trailRenderer;
	private GameObject bowSprite;
	private GameObject bowLeftSide, bowRightSide;

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
		bowSprite = GameObject.Find ("BowSprite");
		bowLeftSide = GameObject.Find ("BowLeftSide");
		bowRightSide = GameObject.Find ("BowRightSide");
		actionAreaCenter = actionArea.GetComponent<Renderer>().bounds.center;
		actionAreaRadius = actionArea.GetComponent<CircleCollider2D>().radius;
		rangedStaminaCost = equippedRanged.GetRangedCost();
		rangedIconSound = Resources.Load<AudioClip> ("RangedIconSound");
		trailRenderer = gameObject.GetComponent<TrailRenderer> ();
		trailRenderer.enabled = false;
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
				bowSprite.GetComponent<Renderer> ().enabled = true;
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
		trailRenderer.enabled = true;
		actionArea.GetComponent<Renderer>().enabled = false;
		bowSprite.GetComponent<Renderer> ().enabled = false;
		endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		Player.Instance.Stamina -= rangedStaminaCost;//equippedRanged.RangedCost;
		AudioSource.PlayClipAtPoint (rangedIconSound, transform.position);
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
			Quaternion rotation = Quaternion.LookRotation(actionAreaCenter - transform.position, transform.TransformDirection(Vector3.back));
			Quaternion result = new Quaternion(0, 0, rotation.z, rotation.w);
			transform.rotation = result;
			bowSprite.transform.rotation = result;

			//Quaternion store = transform.rotation;
			//transform.rotation = Quaternion.identity;
			//Bounds bowBounds = bowSprite.GetComponent<Renderer>().bounds;

			//Vector3 bowLeftPosition = new Vector3(bowBounds.extents.x,0,-2.0f)+transform.position;
			//bowPos.x += bowSprite.GetComponent<Renderer>().bounds.extents.x;
			lineRenderer.SetPosition(0,bowLeftSide.transform.position);
			lineRenderer.SetPosition(1,transform.position);
			//bowPos.x -= 2*bowSprite.GetComponent<Renderer>().bounds.extents.x;
			//Vector3 bowRightPosition = new Vector3(-bowBounds.extents.x,0,-2.0f)+transform.position;
			lineRenderer.SetPosition(2,bowRightSide.transform.position);
			lineRenderer.SetPosition(3,transform.position);
			//transform.rotation = store;
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
