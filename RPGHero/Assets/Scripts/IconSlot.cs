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
	private GameObject icon;

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
		v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
		transform.position = center.position + v;

		if(icon == null && !notified)
		{
			iconSpawner.GetComponent<IconSpawner>().NotifyEmpty(gameObject);
			notified = true;
		}
	}
	public void SetIcon(GameObject icn)
	{
		icon = icn;
		iconSpawner.GetComponent<IconSpawner> ().NotifyFull (gameObject);
		isEmpty = false;
		notified = false;
	}
}
