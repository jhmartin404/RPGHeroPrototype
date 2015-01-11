using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour 
{
	private float fingerRadius = 0.5f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
		}
	}

	public void ChangeScene()
	{
		Application.LoadLevel("LevelSelectScene");
	}
}
