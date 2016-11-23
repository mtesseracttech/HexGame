using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

	public int id;

	private Inventory inv;

	void Start () {
		inv = GameObject.Find ("InventoryController").GetComponent <Inventory> ();
	}

	public void OnDrop (PointerEventData eventData) {
		ItemInfo droppedItem = eventData.pointerDrag.GetComponent <ItemInfo> ();
		if (inv.items [id].ID == -1) {
			inv.items [droppedItem.slot] = new Item ();
			inv.items [id] = droppedItem.item;
			droppedItem.slot = id;
		} else if (droppedItem.slot != id) {
			Transform item = this.transform.GetChild (0);
			item.GetComponent <ItemInfo> ().slot = droppedItem.slot;
			item.transform.SetParent (inv.slots [droppedItem.slot].transform);
			item.transform.position = inv.slots [droppedItem.slot].transform.position;

			droppedItem.slot = id;
			droppedItem.transform.SetParent (this.transform);
			droppedItem.transform.position = this.transform.position;

			inv.items [droppedItem.slot] = item.GetComponent <ItemInfo> ().item;
			inv.items [id] = droppedItem.item;
		}
	}
}