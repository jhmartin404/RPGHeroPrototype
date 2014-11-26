using UnityEngine;
using System.Collections;

public class Prototype10EnemySpawner : MonoBehaviour 
{
	public GameObject enemyPrefab;
	private float spawnTime = 2.0f;//Time between spawns 
	private float timeElapsed = 0.0f;
	private bool spawnLeft = false;
	private int leftIconChooser,rightIconChooser;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		timeElapsed += Time.deltaTime;
		
		//Repeat till 7 icons are on each side
		if(timeElapsed >= spawnTime)
		{
			Vector3 rightPosition = new Vector3(3.0f,2,4.1f);//where to spawn the right spider
			Vector3 leftPosition = new Vector3(-3,2,4.1f);//where to spawn the left spider

			if(spawnLeft)
			{
				//left side
				GameObject leftArea = Instantiate(enemyPrefab,rightPosition,Quaternion.identity) as GameObject;
				Prototype10EnemyScript leftScript = leftArea.GetComponent<Prototype10EnemyScript>();
				spawnLeft = !spawnLeft;
			}
			else
			{
				//right side
				GameObject rightArea = Instantiate(enemyPrefab,leftPosition,Quaternion.identity) as GameObject;
				Prototype10EnemyScript rightScript = rightArea.GetComponent<Prototype10EnemyScript>();
				rightScript.xDirection = 1;
				spawnLeft = !spawnLeft;
			}
			timeElapsed = 0.0f;
		}
	}
}
