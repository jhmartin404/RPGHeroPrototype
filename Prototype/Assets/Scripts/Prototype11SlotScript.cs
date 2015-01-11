using UnityEngine;
using System.Collections;

public class Prototype11SlotScript : MonoBehaviour 
{
	public GameObject swordPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;
	public GameObject objectInSlot = null;//the item that is currently in the slot
	public float xDirection = 1.0f;

	private float speed = 4.0f;//speed of slot
	private Vector2 movement;

	private int iconChooser;//chooses an icon type
	private bool isEmpty = false;//bool to determine if the slot is empty
	private Vector3 spawnPosition;
	private Vector3 iconPosition;
	
	// Use this for initialization
	void Start () 
	{
		rigidbody2D.isKinematic = true;
		movement.y = 0;
		if(xDirection >0)
			spawnPosition = new Vector3 (8, -3.5f, 0.2f);
		else if(xDirection<0)
			spawnPosition = new Vector3 (-8, -4.5f, 0.2f);
		iconPosition = spawnPosition;
		iconPosition.z = 0.1f;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(Camera.main.WorldToViewportPoint (transform.position).x > 2.0 || 
		   Camera.main.WorldToViewportPoint (transform.position).x < -2.0)
		{
			if(!isEmpty && objectInSlot != null)
			{
				if(xDirection < 0)
				{
					transform.position = new Vector3 (8, -3.5f, 0.2f);
					objectInSlot.transform.position = new Vector3 (8, -3.5f, 0.1f);
				}
				else if(xDirection > 0)
				{
					transform.position = new Vector3 (-8, -4.5f, 0.2f);
					objectInSlot.transform.position = new Vector3 (-8, -4.5f, 0.1f);
				}
				
			}
			else
			{
				if(xDirection < 0)
				{
					//objectInSlot.transform.position = new Vector3 (8, -3.5f, 0.1f);
					transform.position = new Vector3 (8, -3.5f, 0.2f);
				}
				else if(xDirection > 0)
				{
					//objectInSlot.transform.position = new Vector3 (-8, -4.5f, 0.1f);
					transform.position = new Vector3 (-8, -4.5f, 0.2f);
				}
			}
		}
		movement.x = xDirection * speed * Time.deltaTime;
		transform.Translate(movement);

		//if the object in the slot is null or if the object in the slot is away from the slot then the slot is empty
		if ((objectInSlot==null || Vector3.Distance(transform.position,objectInSlot.transform.position) > 0.5f) && !isEmpty)
		{
			isEmpty = true;
			//spawn a new icon into the slot in 9 seconds
			Invoke("SpawnIcon",11);
		}


	}

	//spawns in icon into the slot
	void SpawnIcon()
	{
		Vector3 iconTransform = transform.position;
		iconTransform.z = 0.1f;//set to zero to ensure the icon will appear on top of the slot
		iconChooser = Random.Range (0, 4);
		switch(iconChooser)
		{
		case 0:
			GameObject icon = Instantiate(swordPrefab, iconTransform, Quaternion.identity) as GameObject;
			Prototype11SwordScript script = icon.GetComponent<Prototype11SwordScript>();
			script.xDirection = xDirection;
			objectInSlot = icon;
			break;
		case 1:
			GameObject icon2 = Instantiate(coinPrefab, iconTransform, Quaternion.identity) as GameObject;
			Prototype11FireBallScript script2 = icon2.GetComponent<Prototype11FireBallScript>();
			script2.xDirection = xDirection;
			objectInSlot = icon2;
			break;
		case 2:
			GameObject icon3 = Instantiate(fireBallPrefab, iconTransform, Quaternion.identity) as GameObject;
			Prototype11FireBallScript script3 = icon3.GetComponent<Prototype11FireBallScript>();
			script3.xDirection = xDirection;
			objectInSlot = icon3;
			break;
		case 3:
			GameObject icon4 = Instantiate(arrowPrefab, iconTransform, Quaternion.identity) as GameObject;
			Prototype11ArrowScript script4 = icon4.GetComponent<Prototype11ArrowScript>();
			script4.xDirection = xDirection;
			objectInSlot = icon4;
			break;
		}
		isEmpty = false;
	}
}
