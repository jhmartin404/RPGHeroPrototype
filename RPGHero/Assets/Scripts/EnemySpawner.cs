using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
	private Object enemyPrefab;
	private GameObject levelOverText;
	private GameObject levelOverButton;

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Creating Enemy Spawner");
		levelOverText = GameObject.Find ("LevelOverText");
		levelOverText.SetActive (false);
		levelOverButton = GameObject.Find ("LevelOverButton");
		levelOverButton.SetActive (false);
		enemyPrefab = Resources.Load ("Prefabs/EnemyPrefab");
		GameObject child = Instantiate(enemyPrefab,transform.position,Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	public void NotifyEnemyDied()
	{
		levelOverText.SetActive (true);
		levelOverButton.SetActive (true);
		//Application.LoadLevel ("LevelSelectScene");
	}
	
	public void NotifyPlayerDied()
	{
		levelOverText.SetActive (true);
		levelOverButton.SetActive (true);
		//Application.LoadLevel ("StartScene");
	}
}
