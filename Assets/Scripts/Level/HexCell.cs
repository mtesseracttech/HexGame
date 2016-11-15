using UnityEngine;

public class HexCell : MonoBehaviour {

	[SerializeField]
	HexCell[] _neighbors;

	private int _elevation = int.MinValue;
	private bool _hasIncomingRiver, _hasOutgoingRiver;
	private HexDirection _incomingRiver, _outgoingRiver;

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

			if (_hasOutgoingRiver && _elevation < GetNeighbor(_outgoingRiver)._elevation) {
				RemoveOutgoingRiver();
			}

			if (_hasIncomingRiver && _elevation > GetNeighbor(_incomingRiver)._elevation) {
				RemoveIncomingRiver();
			}

			Refresh ();
		}
	}

	public float RiverSurfaceY {
		get {
			return
				(_elevation + HexMetrics.riverSurfaceElevationOffset) * HexMetrics.elevationStep;
		}
	}

	public float StreamBedY {
		get {
			return (_elevation + HexMetrics.streamBedElevationOffset) *	HexMetrics.elevationStep;
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

	public bool HasIncomingRiver {
		get {
			return _hasIncomingRiver;
		}
	}

	public bool HasOutgoingRiver {
		get {
			return _hasOutgoingRiver;
		}
	}

	public HexDirection IncomingRiver {
		get {
			return _incomingRiver;
		}
	}

	public HexDirection OutgoingRiver {
		get {
			return _outgoingRiver;
		}
	}

	public bool HasRiver {
		get {
			return _hasIncomingRiver || _hasOutgoingRiver;
		}
	}

	public bool HasRiverBeginOrEnd {
		get {
			return _hasIncomingRiver != _hasOutgoingRiver;
		}
	}

	public bool HasRiverThroughEdge (HexDirection direction) {
		return
			_hasIncomingRiver && _incomingRiver == direction ||
			_hasOutgoingRiver && _outgoingRiver == direction;
	}

	public void RemoveRiver () {
		RemoveOutgoingRiver();
		RemoveIncomingRiver();
	}

	public void RemoveOutgoingRiver () {
		if (!_hasOutgoingRiver) {
			return;
		}
		_hasOutgoingRiver = false;
		RefreshSelfOnly();

		HexCell neighbor = GetNeighbor(_outgoingRiver);
		neighbor._hasIncomingRiver = false;
		neighbor.RefreshSelfOnly();
	}

	public void RemoveIncomingRiver () {
		if (!_hasIncomingRiver) {
			return;
		}
		_hasIncomingRiver = false;
		RefreshSelfOnly();

		HexCell neighbor = GetNeighbor(_incomingRiver);
		neighbor._hasOutgoingRiver = false;
		neighbor.RefreshSelfOnly();
	}

	void RefreshSelfOnly () {
		chunk.Refresh();
	}

	public void SetOutgoingRiver (HexDirection direction) {
		if (_hasOutgoingRiver && _outgoingRiver == direction) {
			return;
		}

		HexCell neighbor = GetNeighbor(direction);
		if (!neighbor || _elevation < neighbor._elevation) {
			return;
		}

		RemoveOutgoingRiver();
		if (_hasIncomingRiver && _incomingRiver == direction) {
			RemoveIncomingRiver();
		}

		_hasOutgoingRiver = true;
		_outgoingRiver = direction;
		RefreshSelfOnly();

		neighbor.RemoveIncomingRiver();
		neighbor._hasIncomingRiver = true;
		neighbor._incomingRiver = direction.Opposite();
		neighbor.RefreshSelfOnly();
	}
}
