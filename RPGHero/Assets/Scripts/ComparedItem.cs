using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComparedItem : MonoBehaviour 
{
	private InventoryItem comparedItem;

	public InventoryItem CmprdItem
	{
		get
		{
			return comparedItem;
		}
		set
		{
			comparedItem = value;
		}
	}

	public void SetCompared(InventoryItem selectedItem)
	{
		switch(selectedItem.GetItemType())
		{
		case ItemType.Weapon:
			Weapon item = selectedItem as Weapon;
			if(item.WpnType == WeaponType.Melee)
			{
				comparedItem = Player.Instance.GetPlayerInventory().EquippedMeleeWeapon;
			}
			if(item.WpnType == WeaponType.Ranged)
			{
				comparedItem = Player.Instance.GetPlayerInventory().EquippedRangedWeapon;
			}
			break;
		case ItemType.Shield:
			comparedItem = Player.Instance.GetPlayerInventory().EquippedShield;
			break;
		}

		GameObject.Find("ComparedItemImage").GetComponent<SpriteRenderer> ().sprite = comparedItem.GetItemImage ();
		Text selectedText = GameObject.Find("ComparedItemText").GetComponent<Text>();
		selectedText.text = comparedItem.ToString();
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
