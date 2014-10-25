using UnityEngine;
using System.Collections;

public class Prototype11IconSpawner : MonoBehaviour 
{
	public GameObject swordPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;
	public GameObject slotPrefab;

	private float spawnTime = 0.5f;//Time between spawns 
	private Vector3 firstRowIconPosition,secondRowIconPosition,firstRowSlotPosition,secondRowSlotPosition;
	private float timeElapsed = 0.0f;
	private int slots = 0;
	private int firstRowIconChooser,secondRowIconChooser;

	// Use this for initialization
	void Start () 
	{

		firstRowIconPosition = new Vector3 (8, -3.5f, 0.1f);
		secondRowIconPosition = new Vector3 (-8, -4.5f, 0.1f);
		firstRowSlotPosition = new Vector3 (8, -3.5f, 0.2f);
		secondRowSlotPosition = new Vector3 (-8, -4.5f, 0.2f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;

		//Repeat till 7 icons are on each row
		if(timeElapsed >= spawnTime && slots < 7)
		{
			firstRowIconChooser = Random.Range(0,4);//Choose a random icon type for first row
			secondRowIconChooser = Random.Range(0,4);//choose a random icon type for second row

			//first row
			GameObject firstRowArea = Instantiate(slotPrefab,firstRowSlotPosition,Quaternion.identity) as GameObject;
			Prototype11SlotScript firstRowScript = firstRowArea.GetComponent<Prototype11SlotScript>();
			firstRowScript.xDirection = -1;

			switch(firstRowIconChooser)
			{
			case 0:
				GameObject icon = Instantiate(swordPrefab, firstRowIconPosition, Quaternion.identity) as GameObject;
				Prototype11SwordScript script = icon.GetComponent<Prototype11SwordScript>();
				script.xDirection = -1;
				firstRowArea.GetComponent<Prototype11SlotScript>().objectInSlot = icon;
				break;
			case 1:
				GameObject icon2 = Instantiate(coinPrefab, firstRowIconPosition, Quaternion.identity) as GameObject;
				Prototype11FireBallScript script2 = icon2.GetComponent<Prototype11FireBallScript>();
				script2.xDirection = -1;
				firstRowArea.GetComponent<Prototype11SlotScript>().objectInSlot = icon2;
				break;
			case 2:
				GameObject icon3 = Instantiate(fireBallPrefab, firstRowIconPosition, Quaternion.identity) as GameObject;
				Prototype11FireBallScript script3 = icon3.GetComponent<Prototype11FireBallScript>();
				script3.xDirection = -1;
				firstRowArea.GetComponent<Prototype11SlotScript>().objectInSlot = icon3;
				break;
			case 3:
				GameObject icon4 = Instantiate(arrowPrefab, firstRowIconPosition, Quaternion.identity) as GameObject;
				Prototype11ArrowScript script4 = icon4.GetComponent<Prototype11ArrowScript>();
				script4.xDirection = -1;
				firstRowArea.GetComponent<Prototype11SlotScript>().objectInSlot = icon4;
				break;
			}

			//second row
			GameObject secondRowArea = Instantiate(slotPrefab,secondRowSlotPosition,Quaternion.identity) as GameObject;

			switch(secondRowIconChooser)
			{
				case 0:
					GameObject icon = Instantiate(swordPrefab, secondRowIconPosition, Quaternion.identity) as GameObject;
					//Prototype11SwordScript script = icon.GetComponent<Prototype11SwordScript>();
					secondRowArea.GetComponent<Prototype11SlotScript>().objectInSlot = icon;
					break;
				case 1:
					GameObject icon2 = Instantiate(coinPrefab, secondRowIconPosition, Quaternion.identity) as GameObject;
					//Prototype11FireBallScript script2 = icon2.GetComponent<Prototype11FireBallScript>();
					secondRowArea.GetComponent<Prototype11SlotScript>().objectInSlot = icon2;
					break;
				case 2:
					GameObject icon3 = Instantiate(fireBallPrefab, secondRowIconPosition, Quaternion.identity) as GameObject;
					//Prototype11FireBallScript script3 = icon3.GetComponent<Prototype11FireBallScript>();
					secondRowArea.GetComponent<Prototype11SlotScript>().objectInSlot = icon3;
					break;
				case 3:
					GameObject icon4 = Instantiate(arrowPrefab, secondRowIconPosition, Quaternion.identity) as GameObject;
					//Prototype11ArrowScript script4 = icon4.GetComponent<Prototype11ArrowScript>();
					secondRowArea.GetComponent<Prototype11SlotScript>().objectInSlot = icon4;
					break;
			}
			slots ++;
			timeElapsed = 0.0f;
		}
	}
}
