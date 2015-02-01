using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaControl : MonoBehaviour 
{
	private Text manaPotions;
	private float disableTime;
	// Use this for initialization
	void Start () 
	{
		manaPotions = gameObject.GetComponentInChildren<Text> ();
		disableTime = 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		manaPotions.text ="" + Player.Instance.GetPlayerInventory ().ManaPotions;
	}

	public void UseManaPotion()
	{
		if(Player.Instance.GetPlayerInventory ().ManaPotions > 0)
		{
			Player.Instance.Mana += 20;
			Player.Instance.GetPlayerInventory().ManaPotions -= 1;
			gameObject.GetComponent<Button>().interactable = false;
			Invoke("ReactivateControl",disableTime);
		}
	}

	private void ReactivateControl()
	{
		gameObject.GetComponent<Button> ().interactable = true;
	}
}
