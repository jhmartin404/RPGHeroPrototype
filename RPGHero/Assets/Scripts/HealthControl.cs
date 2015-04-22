using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthControl : MonoBehaviour 
{
	public LevelScript levelScript;
	private Text healthPotions;
	private float disableTime;
	private int healAmount;
	private bool potionUsed;
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
		if((Player.Instance.GetPlayerInventory ().HealthPotions <= 0 && GetComponent<Button>().interactable) || levelScript.PlayerLost || levelScript.PlayerWon)
		{
			GetComponent<Button>().interactable = false;
		}
		else if(Player.Instance.GetPlayerInventory ().HealthPotions > 0 && !GetComponent<Button>().interactable && !potionUsed)
		{
			GetComponent<Button>().interactable = true;
		}
		healthPotions.text = "" + Player.Instance.GetPlayerInventory ().HealthPotions;
	}

	public void UseHealthPotion()
	{
		if(Player.Instance.GetPlayerInventory ().HealthPotions > 0)
		{
			potionUsed = true;
			AudioSource.PlayClipAtPoint(potionDrink,transform.position);
			Player.Instance.Health += healAmount;
			Player.Instance.GetPlayerInventory().HealthPotions -= 1;
			GetComponent<Button>().interactable = false;
			Invoke("ReactivateControl",disableTime);
		}
	}

	private void ReactivateControl()
	{
		potionUsed = false;
		gameObject.GetComponent<Button> ().interactable = true;
	}
}
