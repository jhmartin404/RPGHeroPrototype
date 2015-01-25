using UnityEngine;
using System.Collections;

public class IconSlot : MonoBehaviour 
{
	public float degressPerSecond;
	public Transform center;
	private Vector3 v;
	private bool isEmpty;
	// Use this for initialization
	void Start () 
	{
		//degressPerSecond = -85.0f;
		//center = GameObject.Find ("LeftCircle").transform;
		v = transform.position - center.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		v = Quaternion.AngleAxis (degressPerSecond * Time.deltaTime, Vector3.forward) * v;
		transform.position = center.position + v;
	}
}
