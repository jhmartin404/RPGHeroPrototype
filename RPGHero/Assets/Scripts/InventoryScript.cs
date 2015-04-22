using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour 
{
	private Inventory playerInventory;
	private ItemType filter;
	public Text coinsText;
	public Text healthPotionText;
	public Text manaPotionText;
	public Button weaponButton, shieldButton, magicButton;
	public GameObject[] inventorySlots;
	private List<InventoryItem> unequippedItems;
	private int slotCount;

	// Use this for initialization
	void Start () 
	{
		playerInventory = Player.Instance.GetPlayerInventory ();
		unequippedItems = playerInventory.GetUnequippedItems ();
		coinsText.text = "" + playerInventory.Coins;
		healthPotionText.text = "" + playerInventory.HealthPotions;
		manaPotionText.text = "" + playerInventory.ManaPotions;
		filter = ItemType.Weapon;
		weaponButton.image.color = Color.gray;
		SoundManager.Instance.PlayBackgroundMusic ("Store_Scene_BackgroundMusic");

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
		ResetButtonColors();
		shieldButton.image.color = Color.gray;
		ResetBoard ();
		ResetComparedAndSelected ();
	}

	public void SetFilterToMagic()
	{
		SoundManager.Instance.PlayUISound ("Filter_Mode_Select");
		filter = ItemType.Magic;
		ResetButtonColors ();
		magicButton.image.color = Color.gray;
		ResetBoard ();
		ResetComparedAndSelected ();
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
		GameObject compared2 = GameObject.Find ("ComparedItem2");
		if(compared2 != null)
		{
			Destroy(compared2);
		}
		Destroy(GameObject.Find ("ItemButton"));
		Destroy(GameObject.Find ("ItemButton2"));
	}

	public void ResetButtonColors()
	{
		weaponButton.image.color = weaponButton.colors.normalColor;
		shieldButton.image.color = shieldButton.colors.normalColor;
		magicButton.image.color = magicButton.colors.normalColor;
	}

	public void ResetBoard()
	{
		foreach(GameObject obj in inventorySlots)
		{
			obj.GetComponent<InventorySlot>().SetItem(null);
		}
		slotCount = 0;
		for(int i=0;i<unequippedItems.Count;++i)
		{
			if(unequippedItems[i].GetItemType() == filter)
			{
				inventorySlots[slotCount].GetComponent<InventorySlot>().SetItem(unequippedItems[i]);
				slotCount++;
			}
		}
	}
}
