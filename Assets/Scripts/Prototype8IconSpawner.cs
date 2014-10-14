using UnityEngine;
using System.Collections;

public class Prototype8IconSpawner : MonoBehaviour 
{
	public GameObject lineRenderer;
	public GameObject swordPrefab;
	public GameObject coinPrefab;
	public GameObject fireBallPrefab;
	public GameObject arrowPrefab;
	
	private float spawnTime = 0.5f;
	private float timeElapsed = 0.0f;
	private int iconChooser;
	private Vector3 row;
	private GameObject sword;
	
	// Use this for initialization
	void Start () 
	{
		sword = GameObject.Find ("Sword");
		row = new Vector3 (-8, -4.4f, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= spawnTime)
		{
			iconChooser = Random.Range(0,4);
			Vector3 position = row;
			switch(iconChooser)
			{
			case 0:
				GameObject icon = Instantiate(swordPrefab, position, Quaternion.identity) as GameObject;
				Prototype8SwordScript script = icon.GetComponent<Prototype8SwordScript>();
				script.sword = sword;
				break;
			case 1:
				GameObject icon2 = Instantiate(coinPrefab, position, Quaternion.identity) as GameObject;
				break;
			case 2:
				GameObject icon3 = Instantiate(fireBallPrefab, position, Quaternion.identity) as GameObject;
				break;
			case 3:
				GameObject icon4 = Instantiate(arrowPrefab, position, Quaternion.identity) as GameObject;
				Prototype8ArrowScript script2 = icon4.GetComponent<Prototype8ArrowScript>();
				script2.lineRenderer = lineRenderer;
				break;
			}
			timeElapsed = 0.0f;
		}
	}
}
