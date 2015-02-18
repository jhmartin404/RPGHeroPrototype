using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IconSpawner : MonoBehaviour 
{
	private List<GameObject> emptySlots;
	private Object coinPrefab;
	private Object rangedPrefab;
	private Object magic1Prefab;
	private int randomNum;

	private List<Object> iconArray;

	void Awake()
	{
		emptySlots = new List<GameObject> ();
		iconArray = new List<Object> ();

		LevelScript.OnLevelStartEvent += OnLevelStart;
		LevelScript.OnLevelRunningEvent += OnLevelRunning;
		LevelScript.OnLevelWonEvent += OnLevelWon;
		LevelScript.OnLevelLostEvent += OnLevelLost;
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
			GameObject child = Instantiate(iconArray[randomNum],emptySlots[i].transform.position,Quaternion.identity) as GameObject;
			child.GetComponent<Icon>().Center = emptySlots[i].GetComponent<IconSlot>().Center;
			child.GetComponent<Icon>().DegreesPerSecond = emptySlots[i].GetComponent<IconSlot>().DegreesPerSecond;
			emptySlots[i].GetComponent<IconSlot>().SetIcon(child.GetComponent<Icon>());
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
	}
	
	public void OnLevelLost()
	{
		Debug.Log ("OnLevelLost IconSpawner");
	}
}
