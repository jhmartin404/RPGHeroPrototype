using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
	private Object enemyPrefab;
	private GameObject levelOverText;
	private GameObject levelOverButton;

	//Register the EnemySpawner methods with the LevelStateManager
	void Awake()
	{
		LevelStateManager.OnLevelStartEvent += OnLevelStart;
		LevelStateManager.OnLevelRunningEvent += OnLevelRunning;
		LevelStateManager.OnLevelWonEvent += OnLevelWon;
		LevelStateManager.OnLevelLostEvent += OnLevelLost;
	}

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
		LevelStateManager.PushState (LevelState.Won);//Switch the level to won state
	}
	
	public void NotifyPlayerDied()
	{
		levelOverText.SetActive (true);
		levelOverButton.SetActive (true);
		LevelStateManager.PushState (LevelState.Lost);//Switch the level to lost state
	}

	public void OnLevelStart()
	{
		Debug.Log ("OnLevelStart EnemySpawner");
	}

	public void OnLevelRunning()
	{
		Debug.Log ("OnLevelRunning EnemySpawner");
	}

	public void OnLevelWon()
	{
		Debug.Log ("OnLevelWon EnemySpawner");
	}

	public void OnLevelLost()
	{
		Debug.Log ("OnLevelLost EnemySpawner");
	}
}
