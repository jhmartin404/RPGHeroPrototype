using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItemDetails : MonoBehaviour 
{
	private InventoryItem item;
	private Text itemText;
	//private Sprite itemImage;
	
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
		//itemImage = gameObject.GetComponentsInChildren<Image> () [1].sprite;
	}
	
	public void SetItem(InventoryItem itm)
	{
		item = itm;
		if(item != null)
		{
			itemText = gameObject.GetComponentInChildren<Text>();
			gameObject.GetComponentsInChildren<Image> () [1].sprite = item.GetItemImage ();
			itemText.text = item.ToString();
		}
		else
		{
			itemText = gameObject.GetComponentInChildren<Text>();
			gameObject.GetComponentsInChildren<Image> () [1].sprite = null;
			itemText.text = "No Item";

		}
	}
}
