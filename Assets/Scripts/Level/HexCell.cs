using UnityEngine;

public class HexCell : MonoBehaviour {

	[SerializeField]
	HexCell[] _neighbors;

	private int _elevation = int.MinValue;

	public HexCoordinates coordinates;
	public Color color;
	public RectTransform uiRect;
	public HexGridChunk chunk;

	public Vector3 Position {
		get {
			return transform.localPosition;
		}
	}

	public Color Color {
		get {
			return color;
		}
		set {
			if (color == value) {
				return;
			}
			color = value;
			Refresh();
		}
	}

	public int Elevation {
		get {
			return _elevation;
		}
		set {
			if (_elevation == value) {
				return;
			}

			_elevation = value;
			Vector3 position = transform.localPosition;
			position.y = value * HexMetrics.elevationStep;
			position.y += (HexMetrics.SampleNoise (position).y * 2f - 1f) * HexMetrics.elevationPerturbStrength;
			transform.localPosition = position;

			Vector3 uiPosition = uiRect.localPosition;
			uiPosition.z = -position.y;
			uiRect.localPosition = uiPosition;

			Refresh ();
		}
	}

	public HexCell GetNeighbor (HexDirection direction) {
		return _neighbors [(int)direction];
	}

	public void SetNeighbor (HexDirection direction, HexCell cell) {
		_neighbors [(int)direction] = cell;
		cell._neighbors [(int)direction.Opposite ()] = this;
	}

	public HexEdgeType GetEdgeType (HexDirection direction) {
		return HexMetrics.GetEdgeType (_elevation, _neighbors[(int)direction]._elevation);
	}

	public HexEdgeType GetEdgeType (HexCell otherCell) {
		return HexMetrics.GetEdgeType (_elevation, otherCell._elevation);
	}

	void Refresh () {
		if (chunk) {
			chunk.Refresh ();
			for (int i = 0; i < _neighbors.Length; i++) {
				HexCell neighbor = _neighbors[i];
				if (neighbor != null && neighbor.chunk != chunk) {
					neighbor.chunk.Refresh();
				}
			}
		}
	}
}
