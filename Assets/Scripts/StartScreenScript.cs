using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {

	private float fingerRadius = 0.5f;
	// Use this for initialization
	void Start () {
		
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
		
//		if(Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved)
//		{
//			Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//			if (collider2D == Physics2D.OverlapCircle(touchPos, fingerRadius))
//			{
//				Application.LoadLevel("MenuScene");
//			}			
//		}
	}

	public void ChangeScene()
	{
		Application.LoadLevel("MenuScene");
	}
}
