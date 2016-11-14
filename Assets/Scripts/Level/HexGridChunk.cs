using UnityEngine;
using UnityEngine.UI;

public class HexGridChunk : MonoBehaviour {

	HexCell[] _cells;

	HexMesh _hexMesh;
	Canvas _gridCanvas;

	void Awake () {
		_gridCanvas = GetComponentInChildren<Canvas>();
		_hexMesh = GetComponentInChildren<HexMesh>();

		_cells = new HexCell[HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ];
		ShowUI (false);
	}

	public void ShowUI (bool visible) {
		_gridCanvas.gameObject.SetActive(visible);
	}

	public void AddCell (int index, HexCell cell) {
		_cells[index] = cell;
		cell.chunk = this;
		cell.transform.SetParent(transform, false);
		cell.uiRect.SetParent(_gridCanvas.transform, false);
	}

	public void Refresh () {
		enabled = true;
	}

	void LateUpdate () {
		_hexMesh.Triangulate(_cells);
		enabled = false;
	}
}
