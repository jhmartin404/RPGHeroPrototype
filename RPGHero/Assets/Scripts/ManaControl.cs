using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaControl : MonoBehaviour 
{
	private Text manaPotions;
	private float disableTime;
	private int manaReplenishAmount;
	private AudioClip potionDrink;
	// Use this for initialization
	void Start () 
	{
		manaPotions = gameObject.GetComponentInChildren<Text> ();
		disableTime = 2.0f;
		manaReplenishAmount = 20;
		potionDrink = Resources.Load<AudioClip> ("PotionDrinkSound");
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
			AudioSource.PlayClipAtPoint(potionDrink,transform.position);
			Player.Instance.Mana += manaReplenishAmount;
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
