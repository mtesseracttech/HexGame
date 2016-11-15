using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	[SerializeField]
	int _width = 6;
	[SerializeField]
	int _height = 6;
	[SerializeField]
	HexCell _cellPrefab;
	[SerializeField]
	Text _cellLabelPrefab;
	[SerializeField]
	Color _defaultColor = Color.white;

	private HexCell[] _cells;
	private Canvas _gridCanvas;
	private HexMesh _hexMesh;

	void Awake () {
		_gridCanvas = GetComponentInChildren<Canvas> ();
		_hexMesh = GetComponentInChildren<HexMesh> ();

		_cells = new HexCell[_height * _width];
		_gridCanvas = GetComponentInChildren<Canvas> ();

		for (int z = 0, i = 0; z < _height; z++) {
			for (int x = 0; x < _width; x++) {
				CreateCell (x, z, i++);
			}			
		}
	}

	void Start () {
		_hexMesh.Triangulate (_cells);
	}

	private void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = _cells [i] = Instantiate<HexCell> (_cellPrefab);
		cell.transform.SetParent (transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.color = _defaultColor;

		if (x > 0) {
			cell.SetNeighbor (HexDirection.W, _cells [i - 1]);
		}
		if (z > 0) {
			if ((z & 1) == 0) {
				cell.SetNeighbor (HexDirection.SE, _cells [i - _width]);
				if (x > 0) {
					cell.SetNeighbor (HexDirection.SW, _cells [i - _width - 1]);
				}
			} else {
				cell.SetNeighbor (HexDirection.SW, _cells [i - _width]);
				if (x < _width - 1) {
					cell.SetNeighbor (HexDirection.SE, _cells [i - _width + 1]);
				}
			}
		}

		Text label = Instantiate<Text> (_cellLabelPrefab);
		label.rectTransform.SetParent (_gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2 (position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();

		cell.uiRect = label.rectTransform;
	}

	public HexCell GetCell (Vector3 position) {
		position = transform.InverseTransformPoint (position);
		HexCoordinates coordinates = HexCoordinates.FromPosition (position);
		int index = coordinates.X + coordinates.Z * _width + coordinates.Z / 2;
		return _cells [index];
	}

    public HexCell[] GetCells ()
    {
        return _cells;
    }

    public void Refresh () {
		_hexMesh.Triangulate (_cells);
	}
}
