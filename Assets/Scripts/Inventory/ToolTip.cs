using Assets.Scripts.Inventory.Stats.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class ToolTip : MonoBehaviour {

        private Item _item;
        private string _data;
        private GameObject _tooltip;

        void Start () {
            _tooltip = GameObject.Find ("Tooltip");
            _tooltip.SetActive (false);

        }

        void Update () {

            if (_tooltip.activeSelf)
            {
                _tooltip.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 100);
                // _tooltip.transform.position = new Vector2 (850, 180);
            }
        }

        public void Activate (Item item) {
            this._item = item;
            ConstructDataString ();
            _tooltip.SetActive (true);
        }

        public void Deactivate () {
            _tooltip.SetActive (false);
        }

        public void ConstructDataString () {
            _data = "<color=#000000><b>" +_item.ItemName + "</b></color>\n\n" + _item.Description;
            _tooltip.transform.GetChild (0).GetComponent <Text> ().text = _data;
        }
    }
}
