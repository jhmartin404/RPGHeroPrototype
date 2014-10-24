using UnityEngine;
using System.Collections;

public class Prototype10ActionArea : MonoBehaviour 
{
	private GameObject actionArea;
	// Use this for initialization
	void Start () 
	{
		actionArea = GameObject.Find ("ActionArea");
		actionArea.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Render the action area when an icon is selected and disable it when an icon is not selected
		if(Prototype10Layout.getIconSelected())
		{
			actionArea.renderer.enabled = true;
		}
		else if (!Prototype10Layout.getIconSelected() && actionArea.renderer.enabled)
		{
			actionArea.renderer.enabled = false;
		}
	}
}
