using UnityEngine;
using UnityEngine.UI;
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

	public void NotifyEnemyDied()
	{
		Enemy[] remainingEnemies = GetComponents<Enemy> ();
		if(remainingEnemies.Length<=0)
			LevelStateManager.PushState (LevelState.Won);//Switch the level to won state
	}
	
	public void NotifyPlayerDied()
	{
		LevelStateManager.PushState (LevelState.Lost);//Switch the level to lost state
	}

	public void OnLevelStart()
	{
		Debug.Log ("OnLevelStart EnemySpawner");
		Debug.Log ("Creating Enemy Spawner");
		
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
		
		Instantiate(enemyPrefab,transform.position,Quaternion.identity);
	}

	public void OnLevelRunning()
	{
		//Debug.Log ("OnLevelRunning EnemySpawner");
	}

	public void OnLevelWon()
	{
		Debug.Log ("OnLevelWon EnemySpawner");
		CreateEndGameMenu ();
		RemoveMethods ();
	}

	public void OnLevelLost()
	{
		Debug.Log ("OnLevelLost EnemySpawner");
		CreateEndGameMenu ();
		RemoveMethods ();
	}

	public void RemoveMethods()
	{
		LevelStateManager.OnLevelStartEvent -= OnLevelStart;
		LevelStateManager.OnLevelRunningEvent -= OnLevelRunning;
		LevelStateManager.OnLevelWonEvent -= OnLevelWon;
		LevelStateManager.OnLevelLostEvent -= OnLevelLost;
	}

	private void CreateEndGameMenu()
	{
		Object endGameMenu = Resources.Load ("Prefabs/EndGameMenu");
		if(this.gameObject != null)
		{
			GameObject endGame = Instantiate (endGameMenu, transform.position, transform.rotation) as GameObject;
			endGame.transform.SetParent (GameObject.Find ("Canvas").transform, false);
			endGame.transform.position = transform.position;
			endGame.GetComponentInChildren<Button> ().onClick.AddListener (() => {GameObject.Find("Main Camera").GetComponent<LevelScript>().GoBack ();});
		}
		else
		{
			Debug.Log ("GameObject is Dead");
		}
	}
}
