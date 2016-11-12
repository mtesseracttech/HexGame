using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

	[SerializeField]
	Color[] _colors;
	[SerializeField]
	HexGrid _hexGrid;

	private Color _activeColor;
	private int _activeElevation;

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
			EditCell (_hexGrid.GetCell (hit.point));
		}
	}

	public void SelectColor (int index) {
		_activeColor = _colors[index];
	}

	void EditCell (HexCell cell) {
		cell.color = _activeColor;
		cell.Elevation = _activeElevation;
		_hexGrid.Refresh ();
	}

	public void SetElevation (float elevation) {
		_activeElevation = (int)elevation;
	}
}
