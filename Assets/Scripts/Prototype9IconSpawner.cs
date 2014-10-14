using UnityEngine;
using System.Collections;

public class Prototype9IconSpawner : MonoBehaviour 
{
	public GameObject swordPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;

	private float spawnTime = 0.5f;
	private Transform leftPivot,rightPivot;
	private float timeElapsed = 0.0f;
	private int leftIconChooser,rightIconChooser;

	// Use this for initialization
	void Start () 
	{
		leftPivot = GameObject.Find ("LeftPivot").transform;
		rightPivot = GameObject.Find ("RightPivot").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= spawnTime)
		{
			leftIconChooser = Random.Range(0,4);
			rightIconChooser = Random.Range(0,4);
			Vector3 rightPosition = new Vector3(5,-3,0);
			Vector3 leftPosition = new Vector3(-5,-3,0);
			switch(leftIconChooser)
			{
			case 0:
				GameObject icon = Instantiate(swordPrefab, leftPosition, Quaternion.identity) as GameObject;
				Prototype9SwordScript script = icon.GetComponent<Prototype9SwordScript>();
				script.center = leftPivot;
				script.degreesPerSecond *= -1;
				break;
			case 1:
				GameObject icon2 = Instantiate(coinPrefab, leftPosition, Quaternion.identity) as GameObject;
				Prototype9FireBallScript script2 = icon2.GetComponent<Prototype9FireBallScript>();
				script2.center = leftPivot;
				script2.degreesPerSecond *= -1;
				break;
			case 2:
				GameObject icon3 = Instantiate(fireBallPrefab, leftPosition, Quaternion.identity) as GameObject;
				Prototype9FireBallScript script3 = icon3.GetComponent<Prototype9FireBallScript>();
				script3.center = leftPivot;
				script3.degreesPerSecond *= -1;
				break;
			case 3:
				GameObject icon4 = Instantiate(arrowPrefab, leftPosition, Quaternion.identity) as GameObject;
				Prototype9ArrowScript script4 = icon4.GetComponent<Prototype9ArrowScript>();
				script4.center = leftPivot;
				script4.degreesPerSecond *= -1;
				break;
			}
			switch(rightIconChooser)
			{
				case 0:
					GameObject icon = Instantiate(swordPrefab, rightPosition, Quaternion.identity) as GameObject;
					Prototype9SwordScript script = icon.GetComponent<Prototype9SwordScript>();
					script.center = rightPivot;
					break;
				case 1:
					GameObject icon2 = Instantiate(coinPrefab, rightPosition, Quaternion.identity) as GameObject;
					Prototype9FireBallScript script2 = icon2.GetComponent<Prototype9FireBallScript>();
					script2.center = rightPivot;
					break;
				case 2:
					GameObject icon3 = Instantiate(fireBallPrefab, rightPosition, Quaternion.identity) as GameObject;
					Prototype9FireBallScript script3 = icon3.GetComponent<Prototype9FireBallScript>();
					script3.center = rightPivot;
					break;
				case 3:
					GameObject icon4 = Instantiate(arrowPrefab, rightPosition, Quaternion.identity) as GameObject;
					Prototype9ArrowScript script4 = icon4.GetComponent<Prototype9ArrowScript>();
					script4.center = rightPivot;
					break;
			}
			timeElapsed = 0.0f;
		}
	}
}
