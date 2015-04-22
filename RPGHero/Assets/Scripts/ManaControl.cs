using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaControl : MonoBehaviour 
{
	public LevelScript levelScript;
	private Text manaPotions;
	private float disableTime;
	private int manaReplenishAmount;
	private AudioClip potionDrink;
	private bool potionUsed;
	// Use this for initialization
	void Start () 
	{
		potionUsed = false;
		manaPotions = gameObject.GetComponentInChildren<Text> ();
		disableTime = 2.0f;
		manaReplenishAmount = 20;
		potionDrink = Resources.Load<AudioClip> ("PotionDrinkSound");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if((Player.Instance.GetPlayerInventory ().ManaPotions <= 0 && GetComponent<Button>().interactable) || levelScript.PlayerLost || levelScript.PlayerWon)
		{
			GetComponent<Button>().interactable = false;
		}
		else if(Player.Instance.GetPlayerInventory ().ManaPotions > 0 && !GetComponent<Button>().interactable && !potionUsed)
		{
			GetComponent<Button>().interactable = true;
		}
		manaPotions.text ="" + Player.Instance.GetPlayerInventory ().ManaPotions;
	}

	public void UseManaPotion()
	{
		if(Player.Instance.GetPlayerInventory ().ManaPotions > 0)
		{
			potionUsed = true;
			AudioSource.PlayClipAtPoint(potionDrink,transform.position);
			Player.Instance.Mana += manaReplenishAmount;
			Player.Instance.GetPlayerInventory().ManaPotions -= 1;
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
