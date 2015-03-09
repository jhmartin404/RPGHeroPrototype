using UnityEngine;
using System.Collections;

public class InventorySlot : MonoBehaviour 
{
	private InventoryItem item;
	private float fingerRadius = 0.5f;

	public InventoryItem Item
	{
		get
		{
			return item;
		}
		set
		{
			item = value;
		}
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || (Input.GetTouch(0).phase == TouchPhase.Moved))
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				if (GetComponent<Collider2D>() == Physics2D.OverlapCircle(touchPos, fingerRadius))
				{
					if(item != null)
						GameObject.Find("SelectedItem").GetComponent<SelectedItem>().SlctdItem = item;
				}
				
				
			}
		}
	}

	public void SetItem(InventoryItem itm)
	{
		item = itm;
		if(item != null)
		{
			gameObject.GetComponentsInChildren<SpriteRenderer> ()[1].sprite = item.GetItemImage ();
		}
		else
		{
			gameObject.GetComponentsInChildren<SpriteRenderer> ()[1].sprite = null;
		}
	}
}
