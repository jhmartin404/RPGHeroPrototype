using UnityEngine;
using System.Collections;

public class IconSpawner : MonoBehaviour 
{
	public GameObject swordPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;

	private float spawnTime = 1.0f;
	private Transform center;
	private float timeElapsed = 0.0f;
	private int iconChooser;

	// Use this for initialization
	void Start () 
	{
		center = GameObject.Find ("Pivot").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= spawnTime)
		{
			iconChooser = Random.Range(0,4);
			Vector3 position = new Vector3(4,-6,0);
			switch(iconChooser)
			{
				case 0:
					GameObject icon = Instantiate(swordPrefab, position, Quaternion.identity) as GameObject;
					SwordScript script = icon.GetComponent<SwordScript>();
					script.center = center;
					break;
				case 1:
					GameObject icon2 = Instantiate(coinPrefab, position, Quaternion.identity) as GameObject;
					FireBallScript script2 = icon2.GetComponent<FireBallScript>();
					script2.center = center;
					break;
				case 2:
					GameObject icon3 = Instantiate(fireBallPrefab, position, Quaternion.identity) as GameObject;
					FireBallScript script3 = icon3.GetComponent<FireBallScript>();
					script3.center = center;
					break;
				case 3:
					GameObject icon4 = Instantiate(arrowPrefab, position, Quaternion.identity) as GameObject;
					ArrowScript script4 = icon4.GetComponent<ArrowScript>();
					script4.center = center;
					break;
			}
			timeElapsed = 0.0f;
		}
	}
}
