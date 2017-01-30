using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class Inventory : MonoBehaviour
    {

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

        public List<Item> items = new List<Item>();
        public List<GameObject> slots = new List<GameObject>();

        ItemDatabase _database;

        void Start()
        {
            _database = GetComponent<ItemDatabase>();
            for (int i = 0; i < _slotAmount; i++)
            {
                items.Add(new Item());
                slots.Add(Instantiate(_inventorySlot));
                slots[i].GetComponent<Slot>().id = i;
                slots[i].transform.SetParent(_slotPanel.transform, false);
            }
        }

        public void AddItem(int id)
        {
            Debug.Log(items.Count);
            Item itemToAdd = _database.FetchItemById(id);

            if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].ID == id)
                    {
                        ItemInfo data = slots[i].transform.GetChild(0).GetComponent<ItemInfo>();
                        data.amount++;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].ID == -1)
                    {
                        items[i] = itemToAdd;
                        GameObject itemObj = Instantiate(_inventoryItem);
                        itemObj.GetComponent<ItemInfo>().item = itemToAdd;
                        itemObj.GetComponent<ItemInfo>().amount = 1;
                        itemObj.GetComponent<ItemInfo>().slot = i;
                        itemObj.transform.SetParent(slots[i].transform, false);
                        itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                        itemObj.name = itemToAdd.ItemName;
                        break;
                    }
                }
            }
        }

        public bool CheckIfItemIsInInventory(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == item.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveItemFromInventory(int itemId, int amountToRemove)
        {
            ItemInfo data;

            for (int i = 0; i < _slotAmount; i++)
            {
                if (items[i].ID == itemId)
                {
                    data = slots[i].transform.GetChild(0).GetComponent<ItemInfo>();

                    if (data.amount > amountToRemove)
                    {
                        data.amount -= amountToRemove;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                        break;
                    }
                    Destroy(data.gameObject);
                    items[i] = new Item();
                    break;
                }
            }
        }
    }
}