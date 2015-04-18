using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItemDetails : MonoBehaviour 
{
	private InventoryItem item;
	private Text itemText;
	
	public InventoryItem Item
	{
		get
		{
			return item;
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		itemText = gameObject.GetComponentInChildren<Text>();
	}
	
	public void SetItem(InventoryItem itm, bool equipped)
	{
		item = itm;
		if(item != null)
		{
			itemText = gameObject.GetComponentInChildren<Text>();
			gameObject.GetComponentsInChildren<Image> () [1].enabled = true;
			gameObject.GetComponentsInChildren<Image> () [1].sprite = item.GetItemImage ();
			if(equipped)
				itemText.text = "Equipped\n\n"+item.ToString();
			else if(!equipped)
				itemText.text = item.ToString();
		}
		else
		{
			itemText = gameObject.GetComponentInChildren<Text>();
			gameObject.GetComponentsInChildren<Image> () [1].enabled = false;
			itemText.text = "No Item";

		}
	}
}
