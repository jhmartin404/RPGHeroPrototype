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
	bool startThrow;
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		lineRenderer = GameObject.Find ("LineRenderer").GetComponent<LineRenderer> ();
		equippedRanged = Player.Instance.GetPlayerInventory ().EquippedRangedWeapon;
		startThrow = false;
		actionArea = GameObject.Find ("ActionArea");
		actionAreaCenter = actionArea.renderer.bounds.center;
		actionAreaRadius = actionArea.GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		if (Input.touchCount > 0 && iconState != IconState.Thrown)
		{
			if((Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved) && iconState == IconState.Rotating
			   && !mainCamera.GetComponent<LevelScript>().IconSelected && Player.Instance.Stamina >= equippedRanged.RangedCost)
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					iconState = IconState.Grabbed;
					mainCamera.GetComponent<LevelScript>().IconSelected = true;
					//startPosition = transform.position;
					rigidbody2D.isKinematic = true;
					actionArea.renderer.enabled = true;
				}
				
				
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended && iconState == IconState.Grabbed)
			{
				iconState = IconState.Thrown;
				mainCamera.GetComponent<LevelScript>().IconSelected = false;
				lineRenderer.enabled = false;
				rigidbody2D.isKinematic = false;
				actionArea.renderer.enabled = false;
				endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Player.Instance.Stamina -= equippedRanged.RangedCost;
			}
		}
		
		if(iconState == IconState.Rotating)
		{
			OnRotatingState();
		}
		else if(iconState == IconState.Grabbed)
		{
			OnGrabbedState();
		}
		else if(iconState == IconState.Thrown)
		{
			OnThrownState();
		}
		if( Vector2.Distance(actionAreaCenter,transform.position) < actionAreaRadius && iconState == IconState.Grabbed)
		{
			Debug.Log("StartTHrow");
			startThrow = true;
			startPosition = actionAreaCenter;
		}
	}

	protected override void OnGrabbedState()
	{
		Debug.Log ("InGrabstate");
		//if(!actionArea.renderer.enabled)
		//{
		//	actionArea.renderer.enabled = true;
		//}
		if(startThrow)
		{
			Debug.Log("Throw Started");
			//isGrabbed=false;
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
			Debug.Log("Waiting To Start Throw");
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			transform.position = pos;
		}
	}

	protected override void OnThrownState()
	{
		Debug.Log ("NOW THROWN");
		//if(actionArea.renderer.enabled)
		//{
		//	actionArea.renderer.enabled = false;
		//	endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		//}

	}

	public override void LateUpdate()
	{
		if(iconState == IconState.Thrown)
		{
			rigidbody2D.velocity = (startPosition - endPosition).normalized*iconSpeed;
		}
	}
}
