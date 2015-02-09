using UnityEngine;
using System.Collections;

public class MagicIcon : Icon 
{
	private GameObject actionArea;
	private Magic equippedMagic1;
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		equippedMagic1 = Player.Instance.GetPlayerInventory ().EquippedMagic1;
		actionArea = GameObject.Find ("ActionArea");
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		if (Input.touchCount > 0 && iconState != IconState.Thrown)
		{
			if((Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved) && iconState == IconState.Rotating
			   && !mainCamera.GetComponent<LevelScript>().IconSelected && Player.Instance.Mana >= equippedMagic1.ManaCost)
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
				//endPosition = transform.position;
				rigidbody2D.isKinematic = false;
				actionArea.renderer.enabled = false;
				endPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Player.Instance.Mana -= equippedMagic1.ManaCost;
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
	}
}
