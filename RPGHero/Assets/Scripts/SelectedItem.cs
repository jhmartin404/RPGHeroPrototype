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
			GameObject.Find("ComparedItem").GetComponent<ComparedItem>().SetCompared(selectedItem);
		}
	}

	public void EquipItem()
	{
		switch(selectedItem.GetItemType())
		{
		case ItemType.Weapon:
			Weapon item = selectedItem as Weapon;
			if(item.WpnType == WeaponType.Melee)
			{
				InventoryItem prevEquipped = Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon;
				Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevEquipped);
				Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(selectedItem);
				Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon = (MeleeWeapon)selectedItem;
				SlctdItem = prevEquipped;
				GameObject.Find("ComparedItem").GetComponent<ComparedItem>().SetCompared(selectedItem);
			}
			else if(item.WpnType == WeaponType.Ranged)
			{
				InventoryItem prevEquipped = Player.Instance.GetPlayerInventory ().EquippedRangedWeapon;
				Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevEquipped);
				Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(selectedItem);
				Player.Instance.GetPlayerInventory ().EquippedRangedWeapon = (RangedWeapon)selectedItem;
				SlctdItem = prevEquipped;
				GameObject.Find("ComparedItem").GetComponent<ComparedItem>().SetCompared(selectedItem);
			}
			break;
		case ItemType.Shield:
			InventoryItem prevShield = Player.Instance.GetPlayerInventory ().EquippedShield;
			Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevShield);
			Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(selectedItem);
			Player.Instance.GetPlayerInventory ().EquippedShield = (Shield)selectedItem;
			SlctdItem = prevShield;
			GameObject.Find("ComparedItem").GetComponent<ComparedItem>().SetCompared(selectedItem);
			break;
		}
		GameObject.Find ("Main Camera").GetComponent<InventoryScript> ().ResetBoard ();
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
