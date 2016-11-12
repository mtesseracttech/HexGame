using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

	[SerializeField]
	Color[] _colors;
	[SerializeField]
	HexGrid _hexGrid;

	private Color _activeColor;

	void Awake () {
		SelectColor (0);
	}

	void Update () {
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ()) {
			HandleInput ();
		}
	}

	void HandleInput () {
		Ray inputRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (inputRay, out hit)) {
			_hexGrid.ColorCell (hit.point, _activeColor);
		}
	}

	public void SelectColor (int index) {
		_activeColor = _colors[index];
	}
}
