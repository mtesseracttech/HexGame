using UnityEngine;
using UnityEngine.EventSystems;

enum OptionalToggle {
	Ignore, Yes, No
}

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
	private bool _isDrag;
	private HexDirection _dragDirection;
	private HexCell _previousCell;

	private OptionalToggle _riverMode, _roadMode;

	void Awake () {
		SelectColor (0);
	}

	void Update () {
		if (Input.GetMouseButton (0) && !EventSystem.current.IsPointerOverGameObject ()) {
			HandleInput ();
		} else {
			_previousCell = null;
		}
	}

	void HandleInput () {
		Ray inputRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit)) {
			HexCell currentCell = _hexGrid.GetCell(hit.point);
			if (_previousCell && _previousCell != currentCell) {
				ValidateDrag (currentCell);
			} else {
				_isDrag = false;
			}
			EditCells(currentCell);
			_previousCell = currentCell;
		} else {
			_previousCell = null;
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

	void EditCell (HexCell cell)
	{
		if (cell) {
			if (_applyColor)
			{
				cell.Color = _activeColor;
			}
			if (_applyElevation)
			{
				cell.Elevation = _activeElevation;
			}
			if (_riverMode == OptionalToggle.No)
			{
				cell.RemoveRiver();
			}
			if (_roadMode == OptionalToggle.No)
			{
				cell.RemoveRoads();
			}
			if (_isDrag)
			{
				HexCell otherCell = cell.GetNeighbor(_dragDirection.Opposite());
				if (otherCell)
				{
					if (_riverMode == OptionalToggle.Yes)
					{
						otherCell.SetOutgoingRiver(_dragDirection);
					}
					if (_roadMode == OptionalToggle.Yes)
					{
						otherCell.AddRoad(_dragDirection);
					}
				}
			}


		    /*
			else if (_isDrag && _riverMode == OptionalToggle.Yes)
			{
				HexCell otherCell = cell.GetNeighbor(_dragDirection.Opposite());
				if (otherCell)
				{
					otherCell.SetOutgoingRiver(_dragDirection);
				}
			}
            */
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

	public void SetRiverMode (int mode) {
		_riverMode = (OptionalToggle)mode;
		//Debug.Log (_riverMode);
	}

    public void SetRoadMode (int mode) {
        _roadMode = (OptionalToggle)mode;
    }

	void ValidateDrag (HexCell currentCell) {
		for (_dragDirection = HexDirection.NE; _dragDirection <= HexDirection.NW; _dragDirection++) {
			if (_previousCell.GetNeighbor(_dragDirection) == currentCell) {
				_isDrag = true;
				return;
			}
		}
		_isDrag = false;
	}
}
