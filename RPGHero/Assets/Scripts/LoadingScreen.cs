using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
	//We make a static variable to our LoadingScreen instance
	static LoadingScreen instance;
	//reference to gameobject with the static image 
	GameObject loadingScreenImage;
	
	//function which executes on scene awake before the start function
	void Awake()
	{
		//find the ImageLS gameobject from the Hierarchy
		loadingScreenImage = GameObject.Find("LSImage");
		//destroy the already existing instance, if any
		if (instance)
		{
			Destroy(gameObject);
			Hide();     //call hide function to hide the 'loading Screen Sprite'
			return;
		}
		instance = this;    
		instance.loadingScreenImage.SetActive(false);
		DontDestroyOnLoad(this.gameObject);  //make this object persistent between scenes
	}

	void Update()
	{
		if(instance.loadingScreenImage.activeSelf)
		{

		}
	}

	//function to enable the loading screen
	public static void Show()
	{
		//if instance does not exists return from this function
		if (!InstanceExists()) 
		{
			return;
		}
		//enable the loading image object 
		instance.loadingScreenImage.SetActive(true);
	}
	//function to hide the loading screen
	public static void Hide()
	{
		if (!InstanceExists()) 
		{
			return;
		}
		instance.loadingScreenImage.SetActive(false);
	}
	//function to check if the persistent instance exists
	static bool InstanceExists()
	{
		if (!instance)
		{
			return false;
		}
		return true;
		
	}
	
}