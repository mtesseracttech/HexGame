﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	[SerializeField]
	GameObject _inventoryPanel;
	[SerializeField]
	GameObject _slotPanel;
	[SerializeField]
	GameObject _inventorySlot;
	[SerializeField]
	GameObject _inventoryItem;
	[SerializeField]
	int _slotAmount;

	public List <Item> items = new List<Item> ();
	public List <GameObject> slots = new List<GameObject> ();

	ItemDatabase _database;

	void Start ()
    {
		_database = GetComponent <ItemDatabase> ();
		for (int i = 0; i < _slotAmount; i++)
        {
			items.Add (new Item ());
			slots.Add (Instantiate (_inventorySlot));
			slots [i].GetComponent <Slot> ().id = i;
			slots [i].transform.SetParent (_slotPanel.transform, false);
		}

		AddItem(4);
        AddItem(5);
        AddItem(6);
        AddItem(7);
    }

	public void AddItem (int id)
    {
		Item itemToAdd = _database.FetchItemByID (id);

		if (itemToAdd.Stackable == true && CheckIfItemIsInInventory (itemToAdd) == true) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == id) {
					ItemInfo data = slots [i].transform.GetChild (0).GetComponent<ItemInfo> ();
					data.amount++;
					data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
					break;
				}
			}
		}
        else
        {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == -1) {
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (_inventoryItem);
					itemObj.GetComponent <ItemInfo> ().item = itemToAdd;
					itemObj.GetComponent <ItemInfo> ().amount = 1;
					itemObj.GetComponent <ItemInfo> ().slot = i;
					itemObj.transform.SetParent (slots [i].transform, false);
					itemObj.GetComponent <Image> ().sprite = itemToAdd.Sprite;
					itemObj.name = itemToAdd.ItemName;
					break;
				}
			}
		}
	}

	bool CheckIfItemIsInInventory (Item item) {
		for (int i = 0; i < items.Count; i++) {
			if (items [i].ID == item.ID) {
				return true;
			}
		}
		return false;
	}
}
