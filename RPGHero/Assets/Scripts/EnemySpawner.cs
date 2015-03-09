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
		//enemyPrefab = Resources.Load ("Prefabs/SpiderBossPrefab");

		switch(Player.Instance.CurrentLevel)
		{
		case 1:
			enemyPrefab = Resources.Load("Prefabs/BanditPrefab");
			break;
		case 2:
			enemyPrefab = Resources.Load("Prefabs/EaglePrefab");
			break;
		case 3:
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			break;
		case 4:
			enemyPrefab = Resources.Load("Prefabs/OrcPrefab");
			break;
		case 5:
			enemyPrefab = Resources.Load("Prefabs/SpiderBossPrefab");
			break;
		case 6:
			enemyPrefab = Resources.Load("Prefabs/SkeletonPrefab");
			break;
		case 7:
			enemyPrefab = Resources.Load("Prefabs/CougarPrefab");
			break;
		case 8:
			enemyPrefab = Resources.Load("Prefabs/ArcherPrefab");
			break;
		default:
			enemyPrefab = Resources.Load("Prefabs/BanditPrefab");
			break;
		}

		GameObject enemy = Instantiate(enemyPrefab,transform.position,Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	public void NotifyEnemyDied()
	{
		//levelOverText.SetActive (true);
		//levelOverButton.SetActive (true);
		LevelStateManager.PushState (LevelState.Won);//Switch the level to won state
	}
	
	public void NotifyPlayerDied()
	{
		//levelOverText.SetActive (true);
		//levelOverButton.SetActive (true);
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
		levelOverText.SetActive (true);
		levelOverButton.SetActive (true);
	}

	public void OnLevelLost()
	{
		Debug.Log ("OnLevelLost EnemySpawner");
		levelOverText.SetActive (true);
		levelOverButton.SetActive (true);
	}
}
