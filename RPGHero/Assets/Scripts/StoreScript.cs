using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum StoreMode
{
	Buy,
	Sell
};

public class StoreScript : MonoBehaviour 
{
	private Inventory playerInventory;
	private List<InventoryItem> unequippedItems;
	private ItemType filter;
	public Text coinsText;
	public GameObject[] storeSlots;
	public Button buyButton, sellButton, weaponButton, shieldButton, miscButton;
	private int slotCount;
	private Clerk clerk;
	private StoreMode mode;

	public Clerk StoreClerk
	{
		get
		{
			return clerk;
		}
	}

	public StoreMode Mode
	{
		get
		{
			return mode;
		}
	}

	// Use this for initialization
	void Start () 
	{
		clerk = new Clerk ();
		mode = StoreMode.Buy;
		buyButton.image.color = Color.gray;
		SoundManager.Instance.PlayBackgroundMusic ("Store_Scene_BackgroundMusic");
		filter = ItemType.Misc;
		miscButton.image.color = Color.gray;
		unequippedItems = Player.Instance.GetPlayerInventory().GetUnequippedItems ();
		coinsText.text = "" + Player.Instance.GetPlayerInventory().Coins;
		ResetBoard ();
		LoadingScreen.Hide ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				GoBack();
			}
		}
	}

	public void GoBack()
	{
		Player.Instance.Save();
		Application.LoadLevel("LevelSelectScene");
	}


	public void SetFilterToWeapon()
	{
		SoundManager.Instance.PlayUISound ("Filter_Mode_Select");
		filter = ItemType.Weapon;
		ResetButtonColors ();
		weaponButton.image.color = Color.gray;
		ResetBoard ();
		ResetComparedAndSelected ();
	}
	
	public void SetFilterToShield()
	{
		SoundManager.Instance.PlayUISound ("Filter_Mode_Select");
		filter = ItemType.Shield;
		ResetButtonColors ();
		shieldButton.image.color = Color.gray;
		ResetBoard ();
		ResetComparedAndSelected ();
	}

	public void SetFilterToMisc()
	{
		SoundManager.Instance.PlayUISound ("Filter_Mode_Select");
		filter = ItemType.Misc;
		ResetButtonColors ();
		miscButton.image.color = Color.gray;
		ResetBoard ();
		ResetComparedAndSelected ();
	}

	public void SetToBuy()
	{
		SoundManager.Instance.PlayUISound ("Filter_Mode_Select");
		mode = StoreMode.Buy;
		sellButton.image.color = sellButton.colors.normalColor;
		buyButton.image.color = Color.gray;
		ResetBoard ();
		ResetComparedAndSelected ();
	}

	public void SetToSell()
	{
		SoundManager.Instance.PlayUISound ("Filter_Mode_Select");
		mode = StoreMode.Sell;
		sellButton.image.color = Color.gray;
		buyButton.image.color = buyButton.colors.normalColor;
		ResetBoard ();
		ResetComparedAndSelected ();
	}

	public void ResetButtonColors()
	{
		weaponButton.image.color = weaponButton.colors.normalColor;
		shieldButton.image.color = shieldButton.colors.normalColor;
		miscButton.image.color = miscButton.colors.normalColor;
	}

	public void ResetComparedAndSelected()
	{
		GameObject selected = GameObject.Find ("SelectedItem");
		if(selected != null)
		{
			selected.GetComponent<InventoryItemDetails> ().SetItem (null,false);
		}

		GameObject compared = GameObject.Find ("ComparedItem");
		if(compared != null)
		{
			compared.GetComponent<InventoryItemDetails> ().SetItem (null,false);
		}
		Destroy(GameObject.Find ("ItemButton"));
		Destroy(GameObject.Find ("ItemButton2"));
	}

	public void ResetBoard()
	{
		foreach(GameObject obj in storeSlots)
		{
			obj.GetComponent<InventorySlot>().SetItem(null);
		}
		coinsText.text = "" + Player.Instance.GetPlayerInventory().Coins;
		slotCount = 0;
		if(mode == StoreMode.Sell)
		{
			unequippedItems = Player.Instance.GetPlayerInventory().GetUnequippedItems ();
			for(int i=0;i<unequippedItems.Count;++i)
			{
				if(unequippedItems[i].GetItemType() == filter)
				{
					storeSlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
					slotCount++;
				}
			}
		}
		else if(mode == StoreMode.Buy)
		{
			List<InventoryItem> items = clerk.GetItems(Player.Instance.GetPlayerStats().ExpLevel);
			for(int i=0;i<items.Count;++i)
			{
				if(items[i].GetItemType() == filter)
				{
					storeSlots[slotCount].GetComponent<InventorySlot>().SetItem(items[i]);
					slotCount++;
				}
			}
		}
	}
}
