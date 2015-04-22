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
		Object[] objs = FindObjectsOfType (typeof(Enemy));

		//HACKY FIX FOR NOW SHOULD IMPROVE LATER!!!!!!!!!!!!!!!!!!!
		if(objs.Length <= 1)
		{
			LevelStateManager.PushState (LevelState.Won);//Switch the level to won state
		}
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
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			GameObject enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,2);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,2);
			enemyPrefab = Resources.Load("Prefabs/BanditPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,4);
			break;
		case 2:
			enemyPrefab = Resources.Load("Prefabs/BanditPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,3);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,3);
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,4);
			break;
		case 3:
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,3);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,3);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,3);
			break;
		case 4:
			enemyPrefab = Resources.Load("Prefabs/BanditPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,3);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.0f,2);
			enemyPrefab = Resources.Load("Prefabs/ArcherPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (2.5f,5);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-2.5f,6);
			break;
		case 5:
			enemyPrefab = Resources.Load("Prefabs/Section1SpiderBossPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,5);
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,3);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,3);
			break;
		case 6:
			enemyPrefab = Resources.Load("Prefabs/CougarPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,5);
			break;
		case 7:
			enemyPrefab = Resources.Load("Prefabs/ArcherPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,5);
			enemyPrefab = Resources.Load("Prefabs/CougarPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,4);
			break;
		case 8:
			enemyPrefab = Resources.Load("Prefabs/EaglePrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,3);
			enemyPrefab = Resources.Load("Prefabs/ArcherPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,4);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,2);
			break;
		case 9:
			enemyPrefab = Resources.Load("Prefabs/CougarPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,6);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,5);
			break;
		case 10:
			enemyPrefab = Resources.Load("Prefabs/Section2SpiderBossPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,5);
			enemyPrefab = Resources.Load("Prefabs/CougarPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,2);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,2);
			break;
		case 11:
			enemyPrefab = Resources.Load("Prefabs/OrcPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,6);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,5);
			break;
		case 12:
			enemyPrefab = Resources.Load("Prefabs/OrcPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,4);
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,2);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,2);
			break;
		case 13:
			enemyPrefab = Resources.Load("Prefabs/CougarPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,4);
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,2);
			break;
		case 14:
			enemyPrefab = Resources.Load("Prefabs/ArcherPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (2.5f,3);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-2.5f,4);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,5);
			break;
		case 15:
			enemyPrefab = Resources.Load("Prefabs/Section3SpiderBossPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,5);
			enemyPrefab = Resources.Load("Prefabs/OrcPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,2);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,2);
			break;
		case 16:
			enemyPrefab = Resources.Load("Prefabs/SkeletonPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,5);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,4);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,3);
			break;
		case 17:
			enemyPrefab = Resources.Load("Prefabs/SkeletonPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,4);
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,3);
			enemyPrefab = Resources.Load("Prefabs/OrcPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,3);
			break;
		case 18:
			enemyPrefab = Resources.Load("Prefabs/OrcPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,2);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,2);
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,4);
			break;
		case 19:
			enemyPrefab = Resources.Load("Prefabs/SkeletonPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,5);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,4);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,3);
			enemyPrefab = Resources.Load("Prefabs/ArcherPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,6);
			break;
		case 20:
			enemyPrefab = Resources.Load("Prefabs/Section4SpiderBossPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,5);
			enemyPrefab = Resources.Load("Prefabs/SkeletonPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,2);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,2);
			break;
		default:
			enemyPrefab = Resources.Load("Prefabs/WolfPrefab");
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (0,3);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (1.5f,2);
			enemy = Instantiate(enemyPrefab) as GameObject;
			enemy.GetComponent<Enemy> ().SetLayer (-1.5f,2);
			break;
		}
		LoadingScreen.Hide ();
	}

	public void OnLevelRunning()
	{

	}

	public void OnLevelWon()
	{
		Debug.Log ("OnLevelWon EnemySpawner");
		GameObject.Find ("Main Camera").GetComponent<LevelScript> ().PlayerWon = true;
		SoundManager.Instance.PlayUISound("Player_Win");
		CreateEndGameMenu (true);
		RemoveMethods ();
	}

	public void OnLevelLost()
	{
		Debug.Log ("OnLevelLost EnemySpawner");
		CreateEndGameMenu (false);
		RemoveMethods ();
	}

	public void RemoveMethods()
	{
		LevelStateManager.OnLevelStartEvent -= OnLevelStart;
		LevelStateManager.OnLevelRunningEvent -= OnLevelRunning;
		LevelStateManager.OnLevelWonEvent -= OnLevelWon;
		LevelStateManager.OnLevelLostEvent -= OnLevelLost;
	}

	private void CreateEndGameMenu(bool won)
	{
		Object endGameMenu = Resources.Load ("Prefabs/EndGameMenu");
		if(this.gameObject != null)
		{
			GameObject endGame = Instantiate (endGameMenu, transform.position, transform.rotation) as GameObject;
			endGame.transform.SetParent (GameObject.Find ("Canvas").transform, false);
			endGame.transform.position = transform.position;
			endGame.GetComponentInChildren<Button> ().onClick.AddListener (() => {GameObject.Find("Main Camera").GetComponent<LevelScript>().GoBack ();});
			if(won)
			{
				Text endGameText = endGame.GetComponentInChildren<Text>();
				endGameText.text = "Level Won\nCoins Collected\n" + Player.Instance.TemporaryCoins + "\nExperience Earned\n" + Player.Instance.ExperienceCollected;
			}
			else if(!won)
			{
				Text endGameText = endGame.GetComponentInChildren<Text>();
				endGameText.text = "Level Lost\nCoins Collected\n0\nExperience Earned\n" + Player.Instance.ExperienceCollected;
			}
		}
		else
		{
			Debug.Log ("GameObject is Dead");
		}
	}
}
