using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IconSpawner : MonoBehaviour 
{
	private List<GameObject> emptySlots;
	private Object iconPrefab;

	void Awake()
	{
		emptySlots = new List<GameObject> ();
	}

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Creating Icon Spawner");
		//emptySlots = new List<GameObject> ();
		iconPrefab = Resources.Load ("Prefabs/IconPrefab");
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i=0;i<emptySlots.Count;++i)
		{
			GameObject child = Instantiate(iconPrefab,emptySlots[i].transform.position,Quaternion.identity) as GameObject;
			child.GetComponent<Icon>().Center = emptySlots[i].GetComponent<IconSlot>().Center;
			child.GetComponent<Icon>().DegreesPerSecond = emptySlots[i].GetComponent<IconSlot>().DegreesPerSecond;
			emptySlots[i].GetComponent<IconSlot>().SetIcon(child);
		}
	}

	public void NotifyEmpty(GameObject slot)
	{
	//	if(emptySlots != null)
		emptySlots.Add (slot);
	}

	public void NotifyFull(GameObject slot)
	{
		emptySlots.Remove (slot);
	}
}
