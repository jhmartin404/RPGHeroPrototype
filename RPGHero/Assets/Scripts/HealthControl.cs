using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthControl : MonoBehaviour 
{
	private Text healthPotions;
	private float disableTime;
	// Use this for initialization
	void Start () 
	{
		healthPotions = gameObject.GetComponentInChildren<Text> ();
		disableTime = 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		healthPotions.text = "" + Player.Instance.getPlayerInventory ().healthPotions;
	}

	public void UseHealthPotion()
	{
		if(Player.Instance.getPlayerInventory ().healthPotions > 0)
		{
			Player.Instance.health += 20;
			Player.Instance.getPlayerInventory().healthPotions -= 1;
			gameObject.GetComponent<Button>().interactable = false;
			Invoke("ReactivateControl",disableTime);
		}
	}

	private void ReactivateControl()
	{
		gameObject.GetComponent<Button> ().interactable = true;
	}
}
