using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	[SerializeField]
	HexCell _cellPrefab;
	[SerializeField]
	Text _cellLabelPrefab;
	[SerializeField]
	Color _defaultColor = Color.white;
	[SerializeField]
	Texture2D noiseSource;
	[SerializeField]
	HexGridChunk chunkPrefab;

	private HexCell[] _cells;
	private HexGridChunk[] _chunks;

	int _cellCountX;
	int _cellCountZ;

	public int chunkCountX = 4, chunkCountZ = 3;

	void Awake () {
		HexMetrics.noiseSource = noiseSource;

		_cellCountX = chunkCountX * HexMetrics.chunkSizeX;
		_cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

		CreateChunks ();
		CreateCells();
	}

	void CreateChunks () {
		_chunks = new HexGridChunk[chunkCountX * chunkCountZ];

		for (int z = 0, i = 0; z < chunkCountZ; z++) {
			for (int x = 0; x < chunkCountX; x++) {
				HexGridChunk chunk = _chunks[i++] = Instantiate(chunkPrefab);
				chunk.transform.SetParent(transform);
			}
		}
	}

	void CreateCells () {
		_cells = new HexCell[_cellCountZ * _cellCountX];

		for (int z = 0, i = 0; z < _cellCountZ; z++) {
			for (int x = 0; x < _cellCountX; x++) {
				CreateCell (x, z, i++);
			}			
		}
	}


	void OnEnable () {
		HexMetrics.noiseSource = noiseSource;
	}

	private void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = _cells [i] = Instantiate<HexCell> (_cellPrefab);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.color = _defaultColor;

		if (x > 0) {
			cell.SetNeighbor (HexDirection.W, _cells [i - 1]);
		}
		if (z > 0) {
			if ((z & 1) == 0) {
				cell.SetNeighbor (HexDirection.SE, _cells [i - _cellCountX]);
				if (x > 0) {
					cell.SetNeighbor (HexDirection.SW, _cells [i - _cellCountX - 1]);
				}
			} else {
				cell.SetNeighbor (HexDirection.SW, _cells [i - _cellCountX]);
				if (x < _cellCountX - 1) {
					cell.SetNeighbor (HexDirection.SE, _cells [i - _cellCountX + 1]);
				}
			}
		}

		Text label = Instantiate<Text> (_cellLabelPrefab);
		label.rectTransform.anchoredPosition = new Vector2 (position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();

		cell.uiRect = label.rectTransform;

		cell.Elevation = 0;

		AddCellToChunk (x, z, cell);
	}

	void AddCellToChunk (int x, int z, HexCell cell) {
		int chunkX = x / HexMetrics.chunkSizeX;
		int chunkZ = z / HexMetrics.chunkSizeZ;
		HexGridChunk chunk = _chunks[chunkX + chunkZ * chunkCountX];

		int localX = x - chunkX * HexMetrics.chunkSizeX;
		int localZ = z - chunkZ * HexMetrics.chunkSizeZ;
		chunk.AddCell(localX + localZ * HexMetrics.chunkSizeX, cell);
	}

	public HexCell GetCell (Vector3 position) {
		position = transform.InverseTransformPoint (position);
		HexCoordinates coordinates = HexCoordinates.FromPosition (position);
		int index = coordinates.X + coordinates.Z * _cellCountX + coordinates.Z / 2;
		return _cells [index];
	}

	public HexCell GetCell (HexCoordinates coordinates) {
		int z = coordinates.Z;
		if (z < 0 || z >= _cellCountZ) {
			return null;
		}
		int x = coordinates.X + z / 2;
		if (x < 0 || x >= _cellCountX) {
			return null;
		}
		return _cells[x + z * _cellCountX];
	}

	public void ShowUI (bool visible) {
		for (int i = 0; i < _chunks.Length; i++) {
			_chunks[i].ShowUI(visible);
		}
	}
}
