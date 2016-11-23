using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolTip : MonoBehaviour {

	private Item item;
	string data;
	GameObject tooltip;

	void Start () {
		tooltip = GameObject.Find ("Tooltip");
		tooltip.SetActive (false);

	}

	void Update () {
		if (tooltip.activeSelf) {
			//tooltip.transform.position = Input.mousePosition;
			tooltip.transform.position = new Vector2 (850, 180);
		}
	}

	public void Activate (Item item) {
		this.item = item;
		ConstructDataString ();
		tooltip.SetActive (true);
	}

	public void Deactivate () {
		tooltip.SetActive (false);
	}

	public void ConstructDataString () {
		data = "<color=#000000><b>" +item.ItemName + "</b></color>\n\n" + item.Description;
		tooltip.transform.GetChild (0).GetComponent <Text> ().text = data;
	}
}
