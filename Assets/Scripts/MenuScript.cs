using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

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
	}

	void OnGUI()
	{
		int buttonWidth = (int)(Screen.width * 0.3);
		int buttonHeight = (int)(Screen.height * 0.1);
		
		GUI.skin.button.fontSize = Screen.width / 25;
		
		
//		Rect buttonRect = new Rect(
//			Screen.width / 2 - (buttonWidth / 2),
//			Screen.height/6-buttonHeight,
//			buttonWidth,
//			buttonHeight
//			);
//		
//		if(GUI.Button(buttonRect,"Prototype 9"))
//		{
//			// On Click, load the first level.
//			// "Stage1" is the name of the first scene we created.
//			Application.LoadLevel("PrototypeScene9");
//		}
//		
//		Rect button2Rect = new Rect (
//			Screen.width / 2 - (buttonWidth / 2),
//			2*Screen.height/6-buttonHeight,
//			buttonWidth,
//			buttonHeight
//			);
//		
//		if(GUI.Button(button2Rect,"Prototype 10"))
//		{
//			// On Click, load the first level.
//			// "Stage1" is the name of the first scene we created.
//			Application.LoadLevel("PrototypeScene10");
//		}
//		
//		Rect button3Rect = new Rect (
//			Screen.width / 2 - (buttonWidth / 2),
//			3*Screen.height/6-buttonHeight,
//			buttonWidth,
//			buttonHeight
//			);
//		
//		if(GUI.Button(button3Rect,"Prototype 11"))
//		{
//			// On Click, load the first level.
//			// "Stage1" is the name of the first scene we created.
//			Application.LoadLevel("PrototypeScene11");
//		}
//
//		Rect button4Rect = new Rect (
//			Screen.width / 2 - (buttonWidth / 2),
//			4*Screen.height/6 - buttonHeight,
//			buttonWidth,
//			buttonHeight
//			);
//		
//		if(GUI.Button(button4Rect,"Prototype 12"))
//		{
//			// On Click, load the first level.
//			// "Stage1" is the name of the first scene we created.
//			Application.LoadLevel("PrototypeScene12");
//		}

		Rect button5Rect = new Rect (
			Screen.width / 2 - (buttonWidth / 2),
			5*Screen.height/6 - buttonHeight,
			buttonWidth,
			buttonHeight
			);
		
		if(GUI.Button(button5Rect,"Prototype 13"))
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("PrototypeScene13");
		}

//		Rect button6Rect = new Rect (
//			Screen.width / 2 - (buttonWidth / 2),
//			6*Screen.height/6 - buttonHeight,
//			buttonWidth,
//			buttonHeight
//			);
//		
//		if(GUI.Button(button6Rect,"Prototype 14"))
//		{
//			// On Click, load the first level.
//			// "Stage1" is the name of the first scene we created.
//			Application.LoadLevel("PrototypeScene14");
//		}
	}
}
