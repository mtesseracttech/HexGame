using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

	[SerializeField]
	Color[] _colors;
	[SerializeField]
	HexGrid _hexGrid;

	private Color _activeColor;
	private int _activeElevation;
	private bool _applyColor;
	private bool _applyElevation = true;
	private int _brushSize;

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
			EditCells (_hexGrid.GetCell (hit.point));
		}
	}

	public void SelectColor (int index) {
		_applyColor = index >= 0;
		if (_applyColor) {
			_activeColor = _colors [index];
		}
	}

	void EditCells (HexCell center) {
		int centerX = center.coordinates.X;
		int centerZ = center.coordinates.Z;

		for (int r = 0, z = centerZ - _brushSize; z <= centerZ; z++, r++) {
			for (int x = centerX - r; x <= centerX + _brushSize; x++) {
				EditCell(_hexGrid.GetCell(new HexCoordinates(x, z)));
			}
		}

		for (int r = 0, z = centerZ + _brushSize; z > centerZ; z--, r++) {
			for (int x = centerX - _brushSize; x <= centerX + r; x++) {
				EditCell(_hexGrid.GetCell(new HexCoordinates(x, z)));
			}
		}
	}

	void EditCell (HexCell cell) {
		if (cell) {
			if (_applyColor) {
				cell.Color = _activeColor;
			}

			if (_applyElevation) {
				cell.Elevation = _activeElevation;
			}
		}
	}

	public void SetElevation (float elevation) {
		_activeElevation = (int)elevation;
	}

	public void SetApplyElevation (bool toggle) {
		_applyElevation = toggle;
	}

	public void SetBrushSize (float size) {
		_brushSize = (int)size;
	}

	public void ShowUI (bool visible) {
		_hexGrid.ShowUI(visible);
	}
}
