using UnityEngine;
using System.Collections;

public class IconSlot : MonoBehaviour 
{
	public float degreesPerSecond;
	public Transform center;
	private Vector3 v;
	private bool isEmpty;
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
		//degressPerSecond = -85.0f;
		//center = GameObject.Find ("LeftCircle").transform;
		v = transform.position - center.position;
		isEmpty = true;
		Debug.Log (gameObject.ToString ());
		GameObject.Find ("IconSpawner").GetComponent<IconSpawner> ().NotifyEmpty (gameObject);
		icon = null;
	}
	
	// Update is called once per frame
	void Update () 
	{
		v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
		transform.position = center.position + v;
	}

	public void SetIcon(GameObject icn)
	{
		icon = icn;
		GameObject.Find ("IconSpawner").GetComponent<IconSpawner> ().NotifyFull (gameObject);
		isEmpty = false;
	}
}
