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

	public void SetCompared(System.Type type)
	{
		if(type.ToString()=="MeleeWeapon")
		{
			comparedItem = Player.Instance.GetPlayerInventory().EquippedMeleeWeapon;
		}
		if(type.ToString()=="RangedWeapon")
		{
			comparedItem = Player.Instance.GetPlayerInventory().EquippedRangedWeapon;
		}
		if(type.ToString()=="Shield")
		{
			comparedItem = Player.Instance.GetPlayerInventory().EquippedShield;
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
