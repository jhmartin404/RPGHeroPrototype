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
		
		
		Rect buttonRect = new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			Screen.height/5-buttonHeight,
			buttonWidth,
			buttonHeight
			);
		
		if(GUI.Button(buttonRect,"Prototype 1"))
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("PrototypeScene");
		}
		
		Rect button2Rect = new Rect (
			Screen.width / 2 - (buttonWidth / 2),
			2*Screen.height/5-buttonHeight,
			buttonWidth,
			buttonHeight
			);
		
		if(GUI.Button(button2Rect,"Prototype 2"))
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("PrototypeScene2");
		}
		
		Rect button3Rect = new Rect (
			Screen.width / 2 - (buttonWidth / 2),
			3*Screen.height/5-buttonHeight,
			buttonWidth,
			buttonHeight
			);
		
		if(GUI.Button(button3Rect,"Prototype 7"))
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("PrototypeScene7");
		}

		Rect button4Rect = new Rect (
			Screen.width / 2 - (buttonWidth / 2),
			4*Screen.height/5 - buttonHeight,
			buttonWidth,
			buttonHeight
			);
		
		if(GUI.Button(button4Rect,"Prototype 8"))
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("PrototypeScene8");
		}

		Rect button5Rect = new Rect (
			Screen.width / 2 - (buttonWidth / 2),
			5*Screen.height/5 - buttonHeight,
			buttonWidth,
			buttonHeight
			);
		
		if(GUI.Button(button5Rect,"Prototype 9"))
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("PrototypeScene9");
		}

//		Rect button6Rect = new Rect (
//			Screen.width / 2 - (buttonWidth / 2),
//			6*Screen.height/6 - buttonHeight,
//			buttonWidth,
//			buttonHeight
//			);
//		
//		if(GUI.Button(button6Rect,"Prototype 6"))
//		{
//			// On Click, load the first level.
//			// "Stage1" is the name of the first scene we created.
//			Application.LoadLevel("PrototypeScene6");
//		}
	}
}
