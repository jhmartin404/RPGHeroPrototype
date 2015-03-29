using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum IconSpawnerState
{
	Running,
	Paused,
	Stopped
};

public class IconSpawner : MonoBehaviour 
{
	private List<GameObject> emptySlots;
	public List<GameObject> slots;
	private int randomNum;
	private IconSpawnerState spawnerState;

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

	public void SetState(IconSpawnerState state)
	{
		spawnerState = state;
		if(spawnerState != IconSpawnerState.Running)
		{
			for(int i=0;i<slots.Count;++i)
			{
				IconSlot slot = slots[i].GetComponent<IconSlot>();
				slot.IsStopped = true;
				if(slot.SlotIcon != null)
					slot.SlotIcon.StopIcon();
			}
		}
		else if(spawnerState == IconSpawnerState.Running)
		{
			for(int i=0;i<slots.Count;++i)
			{
				IconSlot slot = slots[i].GetComponent<IconSlot>();
				slot.IsStopped = false;
				if(slot.SlotIcon != null)
					slot.SlotIcon.RestartIcon();
			}
		}
	}

	public IconSpawnerState GetState()
	{
		return spawnerState;
	}

	public void NotifyEmpty(GameObject slot)
	{
		emptySlots.Add (slot);
	}

	public void NotifyFull(GameObject slot)
	{
		emptySlots.Remove (slot);
	}

	public void RemoveMethods()
	{
		LevelStateManager.OnLevelStartEvent -= OnLevelStart;
		LevelStateManager.OnLevelRunningEvent -= OnLevelRunning;
		LevelStateManager.OnLevelWonEvent -= OnLevelWon;
		LevelStateManager.OnLevelLostEvent -= OnLevelLost;
	}

	public void OnLevelStart()
	{
		Debug.Log ("OnLevelStart IconSpawner");
		Debug.Log ("Creating Icon Spawner");
		spawnerState = IconSpawnerState.Running;
		iconArray.Add(Resources.Load ("Prefabs/CoinIconPrefab"));
		iconArray.Add(Resources.Load ("Prefabs/RangedIconPrefab"));
		iconArray.Add(Resources.Load ("Prefabs/MagicIconPrefab"));
	}

	public void OnLevelRunning()
	{
		//Debug.Log ("OnLevelRunning IconSpawner");
		if(spawnerState == IconSpawnerState.Running)
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
				spawnedIcon.GetComponent<Icon>().Slot = emptySlots[i];
				emptySlots[i].GetComponent<IconSlot>().SlotIcon = spawnedIcon.GetComponent<Icon>();
			}
		}
	}
	
	public void OnLevelWon()
	{
		Debug.Log ("OnLevelWon IconSpawner");
		//spawnerState = IconSpawnerState.Stopped;
		SetState (IconSpawnerState.Stopped);
		RemoveMethods ();
	}
	
	public void OnLevelLost()
	{
		Debug.Log ("OnLevelLost IconSpawner");
		//spawnerState = IconSpawnerState.Stopped;
		SetState (IconSpawnerState.Stopped);
		RemoveMethods ();
	}
}
