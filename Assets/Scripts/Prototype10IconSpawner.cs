using UnityEngine;
using System.Collections;

public class Prototype10IconSpawner : MonoBehaviour 
{
	//public GameObject healthPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;
	public GameObject slotPrefab;

//	private GameObject[] leftSlots;
//	private GameObject[] rightSlots;
	private float spawnTime = 0.7f;//Time between spawns 
	private Transform leftPivot,rightPivot;
	private float timeElapsed = 0.0f;
	private int slots = 0;
	private int leftIconChooser,rightIconChooser;

	// Use this for initialization
	void Start () 
	{
		leftPivot = GameObject.Find ("LeftPivot").transform;//pivot for the left circle
		rightPivot = GameObject.Find ("RightPivot").transform;//pivot for the right circle
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;

		//Repeat till 7 icons are on each side
		if(timeElapsed >= spawnTime && slots < 7)
		{
			leftIconChooser = Random.Range(1,4);//Choose a random icon type for left side
			rightIconChooser = Random.Range(1,4);//choose a random icon type for right side
			Vector3 rightPosition = new Vector3(3.5f,-3,0.1f);//where to spawn the right icon
			Vector3 leftPosition = new Vector3(-3.5f,-3,0.1f);//where to spawn the left icon
			Vector3 rightSlotPosition = new Vector3(3.5f,-3,0.2f);//where to spawn the right slot
			Vector3 leftSlotPosition = new Vector3(-3.5f,-3,0.2f);//where to spawn the left slot

			//left slot
			GameObject leftArea = Instantiate(slotPrefab,leftSlotPosition,Quaternion.identity) as GameObject;
			Prototype10SlotScript leftScript = leftArea.GetComponent<Prototype10SlotScript>();
			leftScript.center = leftPivot;
			leftScript.degreesPerSecond *= -1;

			//left icon
			switch(leftIconChooser)
			{
//			case 0:
//				GameObject icon = Instantiate(healthPrefab, leftPosition, Quaternion.identity) as GameObject;
//				Prototype10HealthScript script = icon.GetComponent<Prototype10HealthScript>();
//				script.center = leftPivot;
//				script.degreesPerSecond *= -1;
//				leftArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon;
//				break;
			case 1:
				GameObject icon2 = Instantiate(coinPrefab, leftPosition, Quaternion.identity) as GameObject;
				Prototype10FireBallScript script2 = icon2.GetComponent<Prototype10FireBallScript>();
				script2.center = leftPivot;
				script2.degreesPerSecond *= -1;
				leftArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon2;
				break;
			case 2:
				GameObject icon3 = Instantiate(fireBallPrefab, leftPosition, Quaternion.identity) as GameObject;
				Prototype10FireBallScript script3 = icon3.GetComponent<Prototype10FireBallScript>();
				script3.center = leftPivot;
				script3.degreesPerSecond *= -1;
				leftArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon3;
				break;
			case 3:
				GameObject icon4 = Instantiate(arrowPrefab, leftPosition, Quaternion.identity) as GameObject;
				Prototype10ArrowScript script4 = icon4.GetComponent<Prototype10ArrowScript>();
				script4.center = leftPivot;
				script4.degreesPerSecond *= -1;
				leftArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon4;
				break;
//			case 4:
//				GameObject icon5 = Instantiate(shieldPrefab, leftPosition, Quaternion.identity) as GameObject;
//				Prototype10ShieldScript script5 = icon5.GetComponent<Prototype10ShieldScript>();
//				script5.center = leftPivot;
//				script5.degreesPerSecond *= -1;
//				leftArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon5;
//				break;
			}

			//right slot
			GameObject rightArea = Instantiate(slotPrefab,rightSlotPosition,Quaternion.identity) as GameObject;
			Prototype10SlotScript rightScript = rightArea.GetComponent<Prototype10SlotScript>();
			rightScript.center = rightPivot;

			//right icon
			switch(rightIconChooser)
			{
//				case 0:
//					GameObject icon = Instantiate(healthPrefab, rightPosition, Quaternion.identity) as GameObject;
//					Prototype10HealthScript script = icon.GetComponent<Prototype10HealthScript>();
//					script.center = rightPivot;
//					rightArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon;
//					break;
				case 1:
					GameObject icon2 = Instantiate(coinPrefab, rightPosition, Quaternion.identity) as GameObject;
					Prototype10FireBallScript script2 = icon2.GetComponent<Prototype10FireBallScript>();
					script2.center = rightPivot;
					rightArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon2;
					break;
				case 2:
					GameObject icon3 = Instantiate(fireBallPrefab, rightPosition, Quaternion.identity) as GameObject;
					Prototype10FireBallScript script3 = icon3.GetComponent<Prototype10FireBallScript>();
					script3.center = rightPivot;
					rightArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon3;
					break;
				case 3:
					GameObject icon4 = Instantiate(arrowPrefab, rightPosition, Quaternion.identity) as GameObject;
					Prototype10ArrowScript script4 = icon4.GetComponent<Prototype10ArrowScript>();
					script4.center = rightPivot;
					rightArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon4;
					break;
//				case 4:
//					GameObject icon5 = Instantiate(shieldPrefab, rightPosition, Quaternion.identity) as GameObject;
//					Prototype10ShieldScript script5 = icon5.GetComponent<Prototype10ShieldScript>();
//					script5.center = rightPivot;
//					rightArea.GetComponent<Prototype10SlotScript>().objectInSlot = icon5;
//					break;
			}
			slots ++;
			timeElapsed = 0.0f;
		}
	}
}
