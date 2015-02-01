using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectedItem : MonoBehaviour 
{
	private InventoryItem selectedItem;
	
	public InventoryItem SlctdItem
	{
		get
		{
			return selectedItem;
		}
		set
		{
			selectedItem = value;
			GameObject.Find("SelectedItemImage").GetComponent<SpriteRenderer> ().sprite = selectedItem.GetItemImage ();
			Text selectedText = GameObject.Find("SelectedItemText").GetComponent<Text>();
			selectedText.text = selectedItem.ToString();
			GameObject.Find("ComparedItem").GetComponent<ComparedItem>().SetCompared(selectedItem.GetType());
		}
	}

	public void EquipItem()
	{
		if(selectedItem.GetType().ToString()=="MeleeWeapon")
		{
			Player.Instance.GetPlayerInventory ().AddUnequippedItem (Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon);
			Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon = (MeleeWeapon)selectedItem;
			GameObject.Find("ComparedItem").GetComponent<ComparedItem>().SetCompared(selectedItem.GetType());
		}
		if(selectedItem.GetType().ToString()=="RangedWeapon")
		{
			Player.Instance.GetPlayerInventory ().AddUnequippedItem (Player.Instance.GetPlayerInventory ().EquippedRangedWeapon);
			Player.Instance.GetPlayerInventory ().EquippedRangedWeapon = (RangedWeapon)selectedItem;
			GameObject.Find("ComparedItem").GetComponent<ComparedItem>().SetCompared(selectedItem.GetType());
		}
		if(selectedItem.GetType().ToString()=="Shield")
		{
			Player.Instance.GetPlayerInventory ().AddUnequippedItem (Player.Instance.GetPlayerInventory ().EquippedShield);
			Player.Instance.GetPlayerInventory ().EquippedShield = (Shield)selectedItem;
			GameObject.Find("ComparedItem").GetComponent<ComparedItem>().SetCompared(selectedItem.GetType());
		}
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
