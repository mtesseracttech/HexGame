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

	HexCell[] _cells;
	Canvas _gridCanvas;

	void Awake () {
		_cells = new HexCell[_height * _width];
		_gridCanvas = GetComponentInChildren<Canvas> ();

		for (int z = 0, i = 0; z < _height; z++) {
			for (int x = 0; x < _width; x++) {
				CreateCell (x, z, i++);
			}			
		}
	}

	private void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = x * 10f;
		position.y = 0f;
		position.z = z * 10f;

		HexCell cell = _cells [i] = Instantiate<HexCell> (_cellPrefab);
		cell.transform.SetParent (transform, false);
		cell.transform.localPosition = position;

		Text label = Instantiate<Text> (_cellLabelPrefab);
		label.rectTransform.SetParent (_gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2 (position.x, position.z);
		label.text = x.ToString () + "\n" + z.ToString ();
	}
}
