using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IconSpawner : MonoBehaviour 
{
	private List<GameObject> emptySlots;
	private Object coinPrefab;
	private Object rangedPrefab;
	private int randomNum;

	private List<Object> iconArray;

	void Awake()
	{
		emptySlots = new List<GameObject> ();
		iconArray = new List<Object> ();

		//Register the IconSpawner methods with the LevelStateManager
		LevelStateManager.OnLevelStartEvent += OnLevelStart;
		LevelStateManager.OnLevelRunningEvent += OnLevelRunning;
		LevelStateManager.OnLevelWonEvent += OnLevelWon;
		LevelStateManager.OnLevelLostEvent += OnLevelLost;
	}

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Creating Icon Spawner");
		//coinPrefab =
		iconArray.Add(Resources.Load ("Prefabs/CoinIconPrefab"));
		//rangedPrefab = 
		iconArray.Add(Resources.Load ("Prefabs/RangedIconPrefab"));
		//magic1Prefab = 
		iconArray.Add(Resources.Load ("Prefabs/MagicIconPrefab"));
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i=0;i<emptySlots.Count;++i)
		{
			randomNum = Random.Range(0,iconArray.Count);
			Vector3 spawnedIconPosition = emptySlots[i].transform.position;
			spawnedIconPosition.z -= 0.2f;
			GameObject spawnedIcon = Instantiate(iconArray[randomNum],spawnedIconPosition,Quaternion.identity) as GameObject;
			MagicIcon mIcon = spawnedIcon.GetComponent<MagicIcon>();
			if(mIcon != null)
			{
				randomNum = Random.Range(1,3);
				if(randomNum==1)
					mIcon.EquippedMagic = Player.Instance.GetPlayerInventory().EquippedMagic1;
				else
					mIcon.EquippedMagic = Player.Instance.GetPlayerInventory().EquippedMagic2;
			}
			spawnedIcon.GetComponent<Icon>().Center = emptySlots[i].GetComponent<IconSlot>().Center;
			spawnedIcon.GetComponent<Icon>().DegreesPerSecond = emptySlots[i].GetComponent<IconSlot>().DegreesPerSecond;
			emptySlots[i].GetComponent<IconSlot>().SetIcon(spawnedIcon.GetComponent<Icon>());
		}
	}

	public void NotifyEmpty(GameObject slot)
	{
		emptySlots.Add (slot);
	}

	public void NotifyFull(GameObject slot)
	{
		emptySlots.Remove (slot);
	}

	private void RemoveMethods()
	{
		LevelStateManager.OnLevelStartEvent -= OnLevelStart;
		LevelStateManager.OnLevelRunningEvent -= OnLevelRunning;
		LevelStateManager.OnLevelWonEvent -= OnLevelWon;
		LevelStateManager.OnLevelLostEvent -= OnLevelLost;
	}

	public void OnLevelStart()
	{
		Debug.Log ("OnLevelStart IconSpawner");
	}

	public void OnLevelRunning()
	{
		Debug.Log ("OnLevelRunning IconSpawner");
	}
	
	public void OnLevelWon()
	{
		Debug.Log ("OnLevelWon IconSpawner");
		RemoveMethods ();
	}
	
	public void OnLevelLost()
	{
		Debug.Log ("OnLevelLost IconSpawner");
		RemoveMethods ();
	}
}
