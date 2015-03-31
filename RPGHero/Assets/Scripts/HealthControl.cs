using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthControl : MonoBehaviour 
{
	private Text healthPotions;
	private float disableTime;
	private int healAmount;
	private AudioClip potionDrink;
	// Use this for initialization
	void Start () 
	{
		potionDrink = Resources.Load<AudioClip> ("PotionDrinkSound");
		healAmount = 20;
		healthPotions = gameObject.GetComponentInChildren<Text> ();
		disableTime = 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		healthPotions.text = "" + Player.Instance.GetPlayerInventory ().HealthPotions;
	}

	public void UseHealthPotion()
	{
		if(Player.Instance.GetPlayerInventory ().HealthPotions > 0)
		{
			AudioSource.PlayClipAtPoint(potionDrink,transform.position);
			Player.Instance.Health += healAmount;
			Player.Instance.GetPlayerInventory().HealthPotions -= 1;
			gameObject.GetComponent<Button>().interactable = false;
			Invoke("ReactivateControl",disableTime);
		}
	}

	private void ReactivateControl()
	{
		gameObject.GetComponent<Button> ().interactable = true;
	}
}
