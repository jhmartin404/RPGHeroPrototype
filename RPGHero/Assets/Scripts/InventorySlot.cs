using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : MonoBehaviour 
{
	private InventoryItem item;
	private float fingerRadius = 0.3f;
	private Object inventoryItemDetailsPrefab;
	private Object equipButtonPrefab;
	private float saleRatio = 0.8f;

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
		inventoryItemDetailsPrefab = Resources.Load ("Prefabs/InventoryItemDetails");
		equipButtonPrefab = Resources.Load ("Prefabs/EquipButton");
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
					{
						SoundManager.Instance.PlayUISound("Item_Select");
						Vector3 position = new Vector3(0,0,0);
						GameObject selected = GameObject.Find("SelectedItem");
						GameObject compared = GameObject.Find("ComparedItem");
						GameObject compared2 = GameObject.Find("ComparedItem2");
						GameObject itemButton = GameObject.Find("ItemButton");
						GameObject itemButton2 = GameObject.Find("ItemButton2");
						if(selected == null)
						{
							selected = Instantiate(inventoryItemDetailsPrefab,position,Quaternion.identity) as GameObject;
							selected.name = "SelectedItem";
							selected.transform.SetParent(GameObject.Find ("SelectedItemPanel").transform, false);
							itemButton = Instantiate(equipButtonPrefab,position,Quaternion.identity) as GameObject;
							itemButton.transform.SetParent(GameObject.Find ("SelectedItemPanel").transform, false);
							itemButton.name = "ItemButton";
							SetButtonListener(itemButton.GetComponentInChildren<Button>());
						}
						else if(selected != null)
						{
							if(itemButton == null)
							{
								itemButton = Instantiate(equipButtonPrefab,position,Quaternion.identity) as GameObject;
								itemButton.transform.SetParent(GameObject.Find ("SelectedItemPanel").transform, false);
								itemButton.name = "ItemButton";
								SetButtonListener(itemButton.GetComponentInChildren<Button>());
							}
						}
						if(item.GetItemType() == ItemType.Magic)
						{
							if(itemButton2 == null)
							{
								itemButton2 = Instantiate(equipButtonPrefab,position,Quaternion.identity) as GameObject;
								itemButton2.transform.SetParent(GameObject.Find ("SelectedItemPanel").transform, false);
								itemButton2.name = "ItemButton2";
								SetButtonListener(itemButton2.GetComponentInChildren<Button>());
							}
						}
						else if(itemButton2 != null)
						{
							Destroy(itemButton2);
						}
						
						selected.GetComponent<InventoryItemDetails>().SetItem(item);

						if(compared == null)
						{
							compared = Instantiate(inventoryItemDetailsPrefab,position,Quaternion.identity) as GameObject;
							compared.name = "ComparedItem";
							compared.transform.SetParent(GameObject.Find ("ComparedItemPanel").transform, false);
						}



						InventoryItem comparedItem = null;
						InventoryItem comparedItem2 = null;
						switch(item.GetItemType())
						{
						case ItemType.Weapon:
							Weapon weapon = item as Weapon;
							if(weapon.WpnType == WeaponType.Melee)
							{
								comparedItem = Player.Instance.GetPlayerInventory().EquippedMeleeWeapon;
								if(compared2 != null)
								{
									Destroy(compared2);
								}
							}
							if(weapon.WpnType == WeaponType.Ranged)
							{
								comparedItem = Player.Instance.GetPlayerInventory().EquippedRangedWeapon;
								if(compared2 != null)
								{
									Destroy(compared2);
								}
							}
							break;
						case ItemType.Shield:
							comparedItem = Player.Instance.GetPlayerInventory().EquippedShield;
							if(compared2 != null)
							{
								Destroy(compared2);
							}
							break;
						case ItemType.Magic:
							comparedItem = Player.Instance.GetPlayerInventory().EquippedMagic1;
							comparedItem2 = Player.Instance.GetPlayerInventory().EquippedMagic2;
							if(compared2 == null)
							{
								compared2 = Instantiate(inventoryItemDetailsPrefab,position,Quaternion.identity) as GameObject;
								compared2.name = "ComparedItem2";
								compared2.transform.SetParent(GameObject.Find ("ComparedItemPanel").transform, false);
							}
							compared2.GetComponent<InventoryItemDetails>().SetItem(comparedItem2);
							break;
						}
						compared.GetComponent<InventoryItemDetails>().SetItem(comparedItem);
					}
				}				
			}
		}
	}

	private void BuyItem()
	{
		InventoryItem selectedItem = GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().Item;
		if(selectedItem.GetItemCost()<= Player.Instance.GetPlayerInventory().Coins)
		{
			Player.Instance.GetPlayerInventory().Coins -= selectedItem.GetItemCost();
			SoundManager.Instance.PlayUISound("Purchase_Sell_Item");
			switch(selectedItem.GetItemType())
			{
			case ItemType.Weapon:
				Weapon item = selectedItem as Weapon;
				if(item.WpnType == WeaponType.Melee)
				{
					EquipMelee(selectedItem);
					SetCompared(selectedItem);
				}
				else if(item.WpnType == WeaponType.Ranged)
				{
					EquipRanged(selectedItem);
					SetCompared(selectedItem);
				}
				break;
			case ItemType.Shield:
				EquipShield(selectedItem);
				SetCompared(selectedItem);
				break;
			case ItemType.Misc:
				if(selectedItem.GetType() == typeof(HealthPotion))
				{
					//HealthPotion hp = selectedItem as HealthPotion;
					Player.Instance.GetPlayerInventory().HealthPotions++;
				}
				else if(selectedItem.GetType() == typeof(ManaPotion))
				{
					//ManaPotion hp = selectedItem as ManaPotion;
					Player.Instance.GetPlayerInventory().ManaPotions++;
				}
				else if(selectedItem.GetType() == typeof(RepairHammer))
				{
					RepairHammer hammer = selectedItem as RepairHammer;
					Player.Instance.GetPlayerInventory().EquippedShield.RepairShield(hammer.GetRepairAmount());
				}
				break;
			}
		}
		else
		{
			SoundManager.Instance.PlayUISound("Locked_Sound");
		}
		GameObject.Find("Main Camera").GetComponent<StoreScript>().ResetBoard ();
	}


	private void SellItem()
	{
		InventoryItem selectedItem = GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().Item;
		bool removed = Player.Instance.GetPlayerInventory ().RemoveUnequippedItem (selectedItem);
		if(removed)
		{
			SoundManager.Instance.PlayUISound("Purchase_Sell_Item");
			GameObject.Find("Main Camera").GetComponent<StoreScript>().StoreClerk.AddItem(selectedItem);
			Player.Instance.GetPlayerInventory().Coins += (int)((float)selectedItem.GetItemCost()*saleRatio);
			SetComparedAndSelected(null,null);
		}
		GameObject.Find("Main Camera").GetComponent<StoreScript>().ResetBoard ();
	}

	private void EquipItem(string button)
	{
		InventoryItem selectedItem = GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().Item;
		switch(selectedItem.GetItemType())
		{
		case ItemType.Weapon:
			Weapon item = selectedItem as Weapon;
			if(item.WpnType == WeaponType.Melee)
			{
				InventoryItem prevEquipped = Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon;
				EquipMelee(selectedItem);
				SetComparedAndSelected(selectedItem,prevEquipped);
			}
			else if(item.WpnType == WeaponType.Ranged)
			{
				InventoryItem prevEquipped = Player.Instance.GetPlayerInventory ().EquippedRangedWeapon;
				EquipRanged(selectedItem);
				SetComparedAndSelected(selectedItem,prevEquipped);
			}
			break;
		case ItemType.Shield:
			InventoryItem prevShield = Player.Instance.GetPlayerInventory ().EquippedShield;
			EquipShield(selectedItem);
			SetComparedAndSelected(selectedItem,prevShield);
			break;
		case ItemType.Magic:
			if(button == "ItemButton")
			{
				InventoryItem prevMagic1 = Player.Instance.GetPlayerInventory ().EquippedMagic1;
				EquipMagic1(selectedItem);
				SetComparedAndSelected(selectedItem,prevMagic1);
			}
			else if(button == "ItemButton2")
			{
				InventoryItem prevMagic2 = Player.Instance.GetPlayerInventory ().EquippedMagic2;
				EquipMagic2(selectedItem);
				GameObject.Find("ComparedItem2").GetComponent<InventoryItemDetails>().SetItem(selectedItem);
				GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(prevMagic2);
				//SetComparedAndSelected(selectedItem,prevMagic2);
			}
			break;
		}
		GameObject.Find("Main Camera").GetComponent<InventoryScript>().ResetBoard ();
	}

	private void EquipMelee(InventoryItem item)
	{
		SoundManager.Instance.PlayUISound ("Equip_Shield");
		InventoryItem prevEquipped = Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevEquipped);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon = (MeleeWeapon)item;
	}
	
	private void EquipRanged(InventoryItem item)
	{
		SoundManager.Instance.PlayUISound ("Equip_Shield");
		InventoryItem prevEquipped = Player.Instance.GetPlayerInventory ().EquippedRangedWeapon;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevEquipped);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedRangedWeapon = (RangedWeapon)item;
	}
	
	private void EquipShield(InventoryItem item)
	{
		SoundManager.Instance.PlayUISound ("Equip_Shield");
		InventoryItem prevShield = Player.Instance.GetPlayerInventory ().EquippedShield;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevShield);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedShield = (Shield)item;
	}
	
	private void EquipMagic1(InventoryItem item)
	{
		SoundManager.Instance.PlayUISound ("Equip_Magic");
		InventoryItem prevMagic1 = Player.Instance.GetPlayerInventory ().EquippedMagic1;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevMagic1);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedMagic1 = (Magic)item;
	}
	
	private void EquipMagic2(InventoryItem item)
	{
		SoundManager.Instance.PlayUISound ("Equip_Magic");
		InventoryItem prevMagic2 = Player.Instance.GetPlayerInventory ().EquippedMagic2;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevMagic2);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedMagic2 = (Magic)item;
	}

	private void SetButtonListener (Button button)
	{
		StoreScript store = GameObject.Find ("Main Camera").GetComponent<StoreScript> ();
		if(GameObject.Find("Main Camera").GetComponent<InventoryScript>() != null)
		{
			button.onClick.AddListener(() => {EquipItem(button.name);});
			Text buttonText = button.GetComponentInChildren<Text>();
			buttonText.text = "Equip";
		}
		else if(store != null)
		{
			if(store.Mode == StoreMode.Buy)
			{
				button.onClick.AddListener(() => {BuyItem();});
				Text buttonText = button.GetComponentInChildren<Text>();
				buttonText.text = "Buy";
			}
			else if(store.Mode == StoreMode.Sell)
			{
				button.onClick.AddListener(() => {SellItem();});
				Text buttonText = button.GetComponentInChildren<Text>();
				buttonText.text = "Sell";
			}
		}
	}

	private void SetCompared (InventoryItem compared)
	{
		GameObject.Find("ComparedItem").GetComponent<InventoryItemDetails>().SetItem(compared);
	}

	private void SetComparedAndSelected (InventoryItem compared, InventoryItem selected)
	{
		GameObject.Find("ComparedItem").GetComponent<InventoryItemDetails>().SetItem(compared);
		GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(selected);
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
