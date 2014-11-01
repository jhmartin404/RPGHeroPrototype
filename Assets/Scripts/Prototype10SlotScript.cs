using UnityEngine;
using System.Collections;

public class Prototype10SlotScript : MonoBehaviour 
{
	public GameObject healthPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;
	public GameObject objectInSlot = null;//the item that is currently in the slot

	public Transform center;//center that the slot rotates around
	public float degreesPerSecond = 85.0f;//speed the slot rotates at
	private int iconChooser;//chooses an icon type
	private bool isEmpty = false;//bool to determine if the slot is empty
	
	private Vector3 v;
	
	// Use this for initialization
	void Start () 
	{
		v = transform.position - center.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
		transform.position = center.position + v;
		//if the object in the slot is null or if the object in the slot is away from the slot then the slot is empty
		if ((objectInSlot==null || Vector3.Distance(transform.position,objectInSlot.transform.position) > 0.5f) && !isEmpty)
		{
			isEmpty = true;
			//spawn a new icon into the slot in 9 seconds
			Invoke("SpawnIcon",9);
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
			GameObject icon = Instantiate(healthPrefab, iconTransform, Quaternion.identity) as GameObject;
			Prototype10HealthScript script = icon.GetComponent<Prototype10HealthScript>();
			script.center = center;
			script.degreesPerSecond = degreesPerSecond;
			objectInSlot = icon;
			break;
		case 1:
			GameObject icon2 = Instantiate(coinPrefab, iconTransform, Quaternion.identity) as GameObject;
			Prototype10FireBallScript script2 = icon2.GetComponent<Prototype10FireBallScript>();
			script2.center = center;
			script2.degreesPerSecond = degreesPerSecond;
			objectInSlot = icon2;
			break;
		case 2:
			GameObject icon3 = Instantiate(fireBallPrefab, iconTransform, Quaternion.identity) as GameObject;
			Prototype10FireBallScript script3 = icon3.GetComponent<Prototype10FireBallScript>();
			script3.center = center;
			script3.degreesPerSecond = degreesPerSecond;
			objectInSlot = icon3;
			break;
		case 3:
			GameObject icon4 = Instantiate(arrowPrefab, iconTransform, Quaternion.identity) as GameObject;
			Prototype10ArrowScript script4 = icon4.GetComponent<Prototype10ArrowScript>();
			script4.center = center;
			script4.degreesPerSecond = degreesPerSecond;
			objectInSlot = icon4;
			break;
//		case 4:
//			GameObject icon5 = Instantiate(shieldPrefab, iconTransform, Quaternion.identity) as GameObject;
//			Prototype10ShieldScript script5 = icon5.GetComponent<Prototype10ShieldScript>();
//			script5.center = center;
//			script5.degreesPerSecond = degreesPerSecond;
//			objectInSlot = icon5;
//			break;
		}
		isEmpty = false;
	}
}
