using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour 
{
	void Start()
	{
		SoundManager.Instance.PlayBackgroundMusic ("Start_Scene_BackgroundMusic");
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
		SoundManager.Instance.PlayUISound ("StartGame");
		Application.LoadLevel("LevelSelectScene");
	}
}
