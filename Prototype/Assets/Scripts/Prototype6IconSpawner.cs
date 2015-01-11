using UnityEngine;
using System.Collections;

public class Prototype6IconSpawner : MonoBehaviour {

	public GameObject swordPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;
	
	private float spawnTime = 1.0f;
	private float timeElapsed = 0.0f;
	private int iconChooser;
	private Vector3 leftArea;
	private Vector3 bottomArea;
	private Vector3 rightArea;
	private int iteration = 0;
	
	// Use this for initialization
	void Start () 
	{
		leftArea = new Vector3 (-2, -2, 0);
		bottomArea = new Vector3 (0, -4, 0);
		rightArea = new Vector3 (2, -2, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= spawnTime)
		{
			iconChooser = Random.Range(0,4);
			Vector3 position = (iteration==0) ? leftArea : (iteration==1) ? bottomArea : rightArea;
			switch(iconChooser)
			{
			case 0:
				GameObject icon = Instantiate(swordPrefab, position, Quaternion.identity) as GameObject;
				break;
			case 1:
				GameObject icon2 = Instantiate(coinPrefab, position, Quaternion.identity) as GameObject;
				break;
			case 2:
				GameObject icon3 = Instantiate(fireBallPrefab, position, Quaternion.identity) as GameObject;
				break;
			case 3:
				GameObject icon4 = Instantiate(arrowPrefab, position, Quaternion.identity) as GameObject;
				break;
			}
			timeElapsed = 0.0f;
			iteration++;
			if(iteration>2)
				iteration = 0;
		}
	}
}
