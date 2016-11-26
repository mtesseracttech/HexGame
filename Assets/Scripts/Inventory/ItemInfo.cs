using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inventory
{
    public class ItemInfo : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

        public Item item;
        public int amount;
        public int slot;

        private Inventory _inv;
        private Vector2 _offset;

        private ToolTip _tooltip;

        void Start () {
            _inv = GameObject.Find ("InventoryController").GetComponent <Inventory> ();
            _tooltip = _inv.GetComponent <ToolTip> ();
        }

        public void OnBeginDrag (PointerEventData eventData) {
            if (item != null) {
                _offset = eventData.position - new Vector2 (this.transform.position.x, this.transform.position.y);
                this.transform.SetParent (this.transform.parent.parent.parent);
                this.transform.position = eventData.position - _offset;
                GetComponent <CanvasGroup> ().blocksRaycasts = false;
            }
        }

        public void OnPointerDown (PointerEventData eventData) {
            if (item != null) {
                _offset = eventData.position - new Vector2 (this.transform.position.x, this.transform.position.y);
            }
        }

        public void OnDrag (PointerEventData eventData) {
            if (item != null) {
                this.transform.position = eventData.position - _offset;
            }
        }

        public void OnEndDrag (PointerEventData eventData) {
            this.transform.SetParent (_inv.slots [slot].transform);
            this.transform.position = _inv.slots [slot].transform.position;
            GetComponent <CanvasGroup> ().blocksRaycasts = true;
        }

        public void OnPointerEnter (PointerEventData eventData) {
            _tooltip.Activate (item);
        }

        public void OnPointerExit (PointerEventData eventData) {
            _tooltip.Deactivate ();
        }
    }
}
