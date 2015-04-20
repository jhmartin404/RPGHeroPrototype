using UnityEngine;
using System.Collections;

public class IconSlot : MonoBehaviour 
{
	public float degreesPerSecond;
	public Transform center;
	private Vector3 v;
	private bool isEmpty;
	private bool notified;
	private GameObject iconSpawner;
	private Icon icon;
	private bool isStopped;
	private bool invisible;

	public Icon SlotIcon
	{
		get
		{
			return icon;
		}
		set
		{
			icon = value;
			iconSpawner.GetComponent<IconSpawner> ().NotifyFull (gameObject);
			isEmpty = false;
			notified = false;
		}
	}

	public bool IsStopped
	{
		get
		{
			return isStopped;
		}
		set
		{
			isStopped = value;
		}
	}

	public Transform Center
	{
		get
		{
			return center;
		}
		set
		{
			center = value;
		}
	}
	
	public float DegreesPerSecond
	{
		get
		{
			return degreesPerSecond;
		}
		set
		{
			degreesPerSecond = value;
		}
	}

	// Use this for initialization
	void Start () 
	{
		v = transform.position - center.position;
		isEmpty = true;
		iconSpawner = GameObject.Find ("IconSpawner");
		iconSpawner.GetComponent<IconSpawner> ().NotifyEmpty (gameObject);
		notified = true;
		icon = null;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isStopped)
		{
			v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
			transform.position = center.position + v;
		
			if(icon == null && !notified && invisible)
			{
				iconSpawner.GetComponent<IconSpawner>().NotifyEmpty(gameObject);
				notified = true;
			}
		}
	}

	void OnBecameVisible()
	{
		if(!isStopped)
		{
			invisible = false;
		}
	}

	void OnBecameInvisible()
	{
		if(!isStopped)
		{
			invisible = true;
		}
	}
}
