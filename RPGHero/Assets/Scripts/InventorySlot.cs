using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : MonoBehaviour 
{
	private InventoryItem item;
	private float fingerRadius = 0.5f;
	private Object inventoryItemDetailsPrefab;
	private Object equipButtonPrefab;

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
						Vector3 position = new Vector3(0,0,0);
						GameObject selected = GameObject.Find("SelectedItem");
						GameObject compared = GameObject.Find("ComparedItem");
						GameObject compared2 = GameObject.Find("ComparedItem2");
						GameObject equipButton = GameObject.Find("EquipButton");
						GameObject equipButton2 = GameObject.Find("EquipButton2");
						if(selected == null)
						{
							selected = Instantiate(inventoryItemDetailsPrefab,position,Quaternion.identity) as GameObject;
							selected.name = "SelectedItem";
							selected.transform.SetParent(GameObject.Find ("SelectedItemPanel").transform, false);
							equipButton = Instantiate(equipButtonPrefab,position,Quaternion.identity) as GameObject;
							equipButton.transform.SetParent(GameObject.Find ("SelectedItemPanel").transform, false);
							equipButton.name = "EquipButton";
							//equipButton.GetComponentInChildren<Button> ().onClick.AddListener (() => {EquipItem(equipButton.name);});
							equipButton.GetComponentInChildren<Button>().onClick.AddListener(() => {BuyItem();});
						}
						if(item.GetItemType() == ItemType.Magic)
						{
							if(equipButton2 == null)
							{
								//equipButton.GetComponentInChildren<Text>().text = "Equip Slot 1";
								equipButton2 = Instantiate(equipButtonPrefab,position,Quaternion.identity) as GameObject;
								equipButton2.transform.SetParent(GameObject.Find ("SelectedItemPanel").transform, false);
								equipButton2.name = "EquipButton2";
								equipButton2.GetComponentInChildren<Button> ().onClick.AddListener (() => {EquipItem(equipButton2.name);});
								//equipButton2.GetComponentInChildren<Text>().text = "Equip Slot 2";
							}
						}
						else if(equipButton2 != null)
						{
							Destroy(equipButton2);
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

	private void EquipMelee(InventoryItem item)
	{
		InventoryItem prevEquipped = Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevEquipped);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedMeleeWeapon = (MeleeWeapon)item;
	}

	private void EquipRanged(InventoryItem item)
	{
		InventoryItem prevEquipped = Player.Instance.GetPlayerInventory ().EquippedRangedWeapon;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevEquipped);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedRangedWeapon = (RangedWeapon)item;
	}

	private void EquipShield(InventoryItem item)
	{
		InventoryItem prevShield = Player.Instance.GetPlayerInventory ().EquippedShield;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevShield);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedShield = (Shield)item;
	}

	private void EquipMagic1(InventoryItem item)
	{
		InventoryItem prevMagic1 = Player.Instance.GetPlayerInventory ().EquippedMagic1;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevMagic1);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedMagic1 = (Magic)item;
	}

	private void EquipMagic2(InventoryItem item)
	{
		InventoryItem prevMagic2 = Player.Instance.GetPlayerInventory ().EquippedMagic2;
		Player.Instance.GetPlayerInventory ().AddUnequippedItem (prevMagic2);
		Player.Instance.GetPlayerInventory ().RemoveUnequippedItem(item);
		Player.Instance.GetPlayerInventory ().EquippedMagic2 = (Magic)item;
	}

	public void BuyItem()
	{
		InventoryItem selectedItem = GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().Item;
		if(selectedItem.GetItemCost()<= Player.Instance.GetPlayerInventory().Coins)
		{
			Player.Instance.GetPlayerInventory().Coins -= selectedItem.GetItemCost();
			switch(selectedItem.GetItemType())
			{
			case ItemType.Weapon:
				Weapon item = selectedItem as Weapon;
				if(item.WpnType == WeaponType.Melee)
				{
					EquipMelee(selectedItem);
					GameObject.Find("ComparedItem").GetComponent<InventoryItemDetails>().SetItem(selectedItem);
					//GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(prevEquipped);
				}
				else if(item.WpnType == WeaponType.Ranged)
				{
					EquipRanged(selectedItem);
					GameObject.Find("ComparedItem").GetComponent<InventoryItemDetails>().SetItem(selectedItem);
					//GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(prevEquipped);
				}
				break;
			case ItemType.Shield:
				EquipShield(selectedItem);
				GameObject.Find("ComparedItem").GetComponent<InventoryItemDetails>().SetItem(selectedItem);
				//GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(prevShield);
				break;
			case ItemType.Misc:
				if(selectedItem.GetType() == typeof(HealthPotion))
				{
					HealthPotion hp = selectedItem as HealthPotion;
					Player.Instance.GetPlayerInventory().HealthPotions++;
				}
				else if(selectedItem.GetType() == typeof(ManaPotion))
				{
					ManaPotion hp = selectedItem as ManaPotion;
					Player.Instance.GetPlayerInventory().ManaPotions++;
				}
				else if(selectedItem.GetType() == typeof(RepairHammer))
				{
					RepairHammer hammer = selectedItem as RepairHammer;
					Player.Instance.GetPlayerInventory().EquippedShield.HealShield();
				}
				break;
			}
		}
		GameObject.Find("Main Camera").GetComponent<StoreScript>().ResetBoard ();
	}

	public void EquipItem(string button)
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
				GameObject.Find("ComparedItem").GetComponent<InventoryItemDetails>().SetItem(selectedItem);
				GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(prevEquipped);
				//selectedItem = prevEquipped;
			}
			else if(item.WpnType == WeaponType.Ranged)
			{
				InventoryItem prevEquipped = Player.Instance.GetPlayerInventory ().EquippedRangedWeapon;
				EquipRanged(selectedItem);
				GameObject.Find("ComparedItem").GetComponent<InventoryItemDetails>().SetItem(selectedItem);
				GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(prevEquipped);
			}
			break;
		case ItemType.Shield:
			InventoryItem prevShield = Player.Instance.GetPlayerInventory ().EquippedShield;
			EquipShield(selectedItem);
			GameObject.Find("ComparedItem").GetComponent<InventoryItemDetails>().SetItem(selectedItem);
			GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(prevShield);
			break;
		case ItemType.Magic:
			if(button == "EquipButton")
			{
				InventoryItem prevMagic1 = Player.Instance.GetPlayerInventory ().EquippedMagic1;
				EquipMagic1(selectedItem);
				GameObject.Find("ComparedItem").GetComponent<InventoryItemDetails>().SetItem(selectedItem);
				GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(prevMagic1);
			}
			else if(button == "EquipButton2")
			{
				InventoryItem prevMagic2 = Player.Instance.GetPlayerInventory ().EquippedMagic2;
				EquipMagic2(selectedItem);
				GameObject.Find("ComparedItem2").GetComponent<InventoryItemDetails>().SetItem(selectedItem);
				GameObject.Find("SelectedItem").GetComponent<InventoryItemDetails>().SetItem(prevMagic2);
			}
			break;
		}
		GameObject.Find("Main Camera").GetComponent<InventoryScript>().ResetBoard ();
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
